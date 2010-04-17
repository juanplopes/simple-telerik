// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Web;

    public class AuthorizeAttributeBuilder : IAuthorizeAttributeBuilder
    {
        private static readonly Type authorizeAttributeType = typeof(IAuthorizeAttribute);
        private static readonly ModuleBuilder module = CreateModuleBuilder();

        public ConstructorInfo Build(Type parentType)
        {
            Guard.IsNotNull(parentType, "parentType");

            string typeName = "$" + parentType.FullName.Replace(".", string.Empty);

            TypeBuilder typeBuilder = module.DefineType(typeName, TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit, parentType, new[] { authorizeAttributeType });
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            typeBuilder.AddInterfaceImplementation(authorizeAttributeType);

            WriteProperty(parentType, typeBuilder, "Order", typeof(int));
            WriteProperty(parentType, typeBuilder, "Roles", typeof(string));
            WriteProperty(parentType, typeBuilder, "Users", typeof(string));
            WriteIsAuthorized(parentType, typeBuilder);

            Type type = typeBuilder.CreateType();

            return type.GetConstructor(Type.EmptyTypes);
        }

        private static void WriteProperty(Type parentType, TypeBuilder builder, string name, Type type)
        {
            const BindingFlags BindingFlag = BindingFlags.Public | BindingFlags.Instance;
            const MethodAttributes MethodAttribute = MethodAttributes.Public | MethodAttributes.Virtual;

            string getName = "get_" + name;
            string setName = "set_" + name;

            MethodInfo parentGetMethod = parentType.GetMethod(getName, BindingFlag);
            MethodBuilder implementedGetMethod = builder.DefineMethod(getName, MethodAttribute, type, Type.EmptyTypes);
            ILGenerator getIl = implementedGetMethod.GetILGenerator();
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Call, parentGetMethod);
            getIl.Emit(OpCodes.Ret);

            MethodInfo interfaceGetMethod = authorizeAttributeType.GetMethod(getName, BindingFlag);
            builder.DefineMethodOverride(implementedGetMethod, interfaceGetMethod);

            MethodInfo parentSetMethod = parentType.GetMethod(setName, BindingFlag);
            MethodBuilder implementedSetMethod = builder.DefineMethod(setName, MethodAttribute, typeof(void), new[] { type });
            ILGenerator setIl = implementedSetMethod.GetILGenerator();
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Call, parentSetMethod);
            setIl.Emit(OpCodes.Ret);

            MethodInfo interfaceSetMethod = authorizeAttributeType.GetMethod(setName, BindingFlag);
            builder.DefineMethodOverride(implementedSetMethod, interfaceSetMethod);
        }

        private static void WriteIsAuthorized(Type parentType, TypeBuilder builder)
        {
            MethodInfo protectedMethod = parentType.GetMethod("AuthorizeCore", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);
            MethodBuilder implementedMethod = builder.DefineMethod("IsAuthorized", MethodAttributes.Public | MethodAttributes.Virtual, typeof(bool), new[] { typeof(HttpContextBase) });
            ILGenerator il = implementedMethod.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Call, protectedMethod);
            il.Emit(OpCodes.Ret);

            MethodInfo interfaceMethod = authorizeAttributeType.GetMethod("IsAuthorized", BindingFlags.Public | BindingFlags.Instance);
            builder.DefineMethodOverride(implementedMethod, interfaceMethod);
        }

        private static ModuleBuilder CreateModuleBuilder()
        {
            const string Name = "InheritedAuthorizeAttributes";

            AssemblyName assemblyName = new AssemblyName(Name + "Assembly")
                                            {
                                                Version = typeof(AuthorizeAttributeBuilder).Assembly.GetName().Version
                                            };

            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(Name + "Module");

            return moduleBuilder;
        }
    }
}
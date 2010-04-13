// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using System.Reflection;
    using System.Web.Mvc;

    using Xunit;

    public class AuthorizeAttributeBuilderTests
    {
        [Fact]
        public void Should_be_able_to_build_derived_attribute_for_the_provided_type()
        {
            ConstructorInfo ctor = new AuthorizeAttributeBuilder().Build(typeof(AuthorizeAttribute));

            IAuthorizeAttribute attribute = ctor.Invoke(new object[0]) as IAuthorizeAttribute;

            Assert.NotNull(attribute);
        }
    }
}
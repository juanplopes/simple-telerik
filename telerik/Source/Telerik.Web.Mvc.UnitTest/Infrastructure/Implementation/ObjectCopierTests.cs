// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Moq;
    using Xunit;

    public class ObjectCopierTests
    {
        private readonly Mock<IFieldCache> fieldCache;
        private readonly Mock<IPropertyCache> propertyCache;

        private readonly ObjectCopier copier;

        public ObjectCopierTests()
        {
            fieldCache = new Mock<IFieldCache>();
            fieldCache.Setup(c => c.GetFields(It.IsAny<Type>())).Returns((Type t) => t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField | BindingFlags.SetField).Where(field => !field.IsInitOnly));

            propertyCache = new Mock<IPropertyCache>();
            propertyCache.Setup(c => c.GetProperties(It.IsAny<Type>())).Returns((Type t) => t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty).Where(property => property.CanRead && property.CanWrite));

            copier = new ObjectCopier(fieldCache.Object, propertyCache.Object);
        }

        [Fact]
        public void Should_be_able_to_copy_objects()
        {
            ObjectToCopy source = new ObjectToCopy
                                      {
                                          Id = 10,
                                          Name = "A dummy name",
                                          Address = "A dummy address"
                                      };

            ObjectToCopy destination = new ObjectToCopy();

            copier.Copy(source, destination);

            Assert.Equal(10, destination.Id);
            Assert.Equal("A dummy name", destination.Name);
            Assert.Equal("A dummy address", destination.Address);
        }

        [Fact]
        public void Should_be_able_to_copy_with_excluded_member()
        {
            ObjectToCopy source = new ObjectToCopy
                                      {
                                          Id = 10,
                                          Name = "A dummy name",
                                          Address = "A dummy address"
                                      };

            ObjectToCopy destination = new ObjectToCopy();

            copier.Copy(source, destination, "Id", "Address");

            Assert.Equal(0, destination.Id);
            Assert.Equal("A dummy name", destination.Name);
            Assert.Null(destination.Address);
        }
    }

    public class ObjectToCopy
    {
        public int Id;

        public string Name
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }
    }
}
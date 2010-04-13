namespace Telerik.Web.Mvc.UnitTest.Effects
{
	using System;
	using System.IO;
	using System.Web.UI;
	using System.Collections.Generic;

	using Xunit;
	using Moq;

	using Telerik.Web.Mvc.UI;

    public class PropertyAnimationSerializationTests
    {
        private PropertyAnimation property;

        public PropertyAnimationSerializationTests()
        {
            property = new PropertyAnimation();
        }

        [Fact]
        public void Opacity_should_serialize_its_name_and_properties()
        {
            property.AnimationType = PropertyAnimationType.Opacity;

            string serialized = property.Serialize();

            Assert.Equal("{name:'property',properties:['opacity']}", serialized);
        }

        [Fact]
        public void Height_should_serialize_its_name_and_properties()
        {
            property.AnimationType = PropertyAnimationType.Height;

            string serialized = property.Serialize();

            Assert.Equal("{name:'property',properties:['height']}", serialized);
        }
    }
}
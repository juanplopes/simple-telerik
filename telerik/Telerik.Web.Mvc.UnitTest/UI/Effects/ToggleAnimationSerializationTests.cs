namespace Telerik.Web.Mvc.UnitTest.Effects
{
	using System;
	using System.IO;
	using System.Web.UI;
	using System.Collections.Generic;

	using Xunit;
	using Moq;

	using Telerik.Web.Mvc.UI;

	public class ToggleAnimationSerializationTests
	{
        private ToggleEffect toggle;

        public ToggleAnimationSerializationTests()
		{
            toggle = new ToggleEffect();
		}

        [Fact]
        public void Toggle_should_serialize_just_its_name() 
        {
            string serialized = toggle.Serialize();

            Assert.Equal("{name:'toggle'}", serialized);
        }
	}
}
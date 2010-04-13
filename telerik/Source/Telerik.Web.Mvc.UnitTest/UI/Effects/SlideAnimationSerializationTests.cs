namespace Telerik.Web.Mvc.UnitTest.Effects
{
	using System;
	using System.IO;
	using System.Web.UI;
	using System.Collections.Generic;

	using Xunit;
	using Moq;

	using Telerik.Web.Mvc.UI;

	public class SlideAnimationSerializationTests
	{
        private SlideAnimation slide;

        public SlideAnimationSerializationTests()
		{
            slide = new SlideAnimation();
		}

        [Fact]
        public void Slide_should_serialize_just_its_name() 
        {
            string serialized = slide.Serialize();

            Assert.Equal("{name:'slide'}", serialized);
        }
	}
}
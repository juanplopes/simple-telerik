// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions.UnitTest
{
	using System.Collections.Generic;

	using Moq;
	using Xunit;
	using Telerik.Web.Mvc.Extensions;
	using Telerik.Web.Mvc.UI;

	public class HtmlAttributesContainerExtensionsTests
	{
		[Fact]
		public void Should_add_a_css_class_to_attributes_collection()
		{
			var dictionary = new Dictionary<string, object>();

			var myAttributesBag = new Mock<IHtmlAttributesContainer>();
			myAttributesBag.SetupGet(a => a.HtmlAttributes).Returns(dictionary);
			
			HtmlAttributesContainerExtensions.AppendCssClass(myAttributesBag.Object, "custom-class");

			Assert.True(dictionary.ContainsKey("class"));
		}

		[Fact]
		public void Should_contain_added_class()
		{
			var dictionary = new Dictionary<string, object>();

			var myAttributesBag = new Mock<IHtmlAttributesContainer>();
			myAttributesBag.SetupGet(a => a.HtmlAttributes).Returns(dictionary);
			
			HtmlAttributesContainerExtensions.AppendCssClass(myAttributesBag.Object, "custom-class");

			Assert.True(((string)dictionary["class"]).Contains("custom-class"));
		}

		[Fact]
		public void Should_contain_added_classes()
		{
			var dictionary = new Dictionary<string, object>();

			var myAttributesBag = new Mock<IHtmlAttributesContainer>();
			myAttributesBag.SetupGet(a => a.HtmlAttributes).Returns(dictionary);

			HtmlAttributesContainerExtensions.AppendCssClasses(myAttributesBag.Object, new string[] { "class1", "class2" });

			Assert.True(((string)dictionary["class"]).Contains("class1"));
			Assert.True(((string)dictionary["class"]).Contains("class2"));
		}
	}
}
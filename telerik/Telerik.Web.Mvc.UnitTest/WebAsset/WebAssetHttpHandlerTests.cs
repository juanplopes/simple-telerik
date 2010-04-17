// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UnitTest
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Web;

    using Infrastructure;

    using Moq;
    using Xunit;

    public class WebAssetHttpHandlerTests
    {
        private readonly Mock<IWebAssetRegistry> _registry;
        private readonly Mock<IHttpResponseCompressor> _compressor;
        private readonly Mock<IHttpResponseCacher> _cacher;

        private readonly WebAssetHttpHandler _handler;

        public WebAssetHttpHandlerTests()
        {
            _registry = new Mock<IWebAssetRegistry>();
            _compressor = new Mock<IHttpResponseCompressor>();
            _cacher = new Mock<IHttpResponseCacher>();

            _handler = new WebAssetHttpHandler(_registry.Object, _compressor.Object, _cacher.Object);
        }

        [Fact]
        public void Default_constructor_should_not_throw_exception_when_not_running_in_web_server()
        {
            Mock<IServiceLocator> locator = new Mock<IServiceLocator>();

            locator.Setup(l => l.Resolve<IWebAssetRegistry>()).Returns(new Mock<IWebAssetRegistry>().Object);
            locator.Setup(l => l.Resolve<IHttpResponseCompressor>()).Returns(new Mock<IHttpResponseCompressor>().Object);
            locator.Setup(l => l.Resolve<IHttpResponseCacher>()).Returns(new Mock<IHttpResponseCacher>().Object);

            ServiceLocator.SetCurrent(() => locator.Object);

            Assert.DoesNotThrow(() => new WebAssetHttpHandler());
        }

        [Fact]
        public void Should_be_able_to_set_default_path()
        {
            WebAssetHttpHandler.DefaultPath = "~/handlers/asset.axd";

            Assert.Equal("~/handlers/asset.axd", WebAssetHttpHandler.DefaultPath);

            WebAssetHttpHandler.DefaultPath = "~/asset.axd";
        }

        [Fact]
        public void Should_be_able_to_set_id_parameter_name()
        {
            WebAssetHttpHandler.IdParameterName = "identifier";

            Assert.Equal("identifier", WebAssetHttpHandler.IdParameterName);

            WebAssetHttpHandler.IdParameterName = "id";
        }

        [Fact]
        public void ProcessRequest_should_write_asset()
        {
            Mock<Stream> outputStream = new Mock<Stream>();

            Process(outputStream, false, 0);

            outputStream.Verify();
        }

        [Fact]
        public void ProcessRequest_should_compress_response()
        {
            Mock<Stream> outputStream = new Mock<Stream>();

            Process(outputStream, true, 0);

            _compressor.Verify();
        }

        [Fact]
        public void ProcessRequest_should_cache_response()
        {
            Mock<Stream> outputStream = new Mock<Stream>();

            Process(outputStream, false, 7);

            _cacher.Verify();
        }

        private void Process(Mock<Stream> outputStream, bool compress, int cacheDuration)
        {
            const string Id = "a-fake-id";

            Mock<HttpRequestBase> httpRequest = new Mock<HttpRequestBase>();
            httpRequest.SetupGet(request => request.QueryString).Returns(new NameValueCollection { { WebAssetHttpHandler.IdParameterName, Id } });

            outputStream.SetupGet(stream => stream.CanRead).Returns(true);
            outputStream.SetupGet(stream => stream.CanWrite).Returns(true);
            outputStream.SetupGet(stream => stream.CanSeek).Returns(true);

            outputStream.Setup(stream => stream.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            Mock<HttpResponseBase> httpResponse = new Mock<HttpResponseBase>();
            httpResponse.SetupGet(response => response.OutputStream).Returns(outputStream.Object);

            Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(context => context.IsDebuggingEnabled).Returns(false);
            httpContext.SetupGet(context => context.Request).Returns(httpRequest.Object);
            httpContext.SetupGet(context => context.Response).Returns(httpResponse.Object);

            WebAsset asset = new WebAsset("application/x-javascript", "2.0", compress, cacheDuration, "function ready(){ test.init(); }");

            _registry.Setup(registry => registry.Retrieve(It.IsAny<string>())).Returns(asset);

            httpResponse.SetupSet(response => response.ContentType).Verifiable();
            _compressor.Setup(compressor => compressor.Compress(httpContext.Object)).Verifiable();
            _cacher.Setup(cacher => cacher.Cache(httpContext.Object, It.IsAny<TimeSpan>())).Verifiable();

            _handler.ProcessRequest(httpContext.Object);
        }
    }
}
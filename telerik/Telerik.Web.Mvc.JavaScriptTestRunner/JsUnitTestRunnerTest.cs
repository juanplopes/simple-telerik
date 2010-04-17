namespace Telerik.Web.Mvc.JavaScriptTestRunner
{
    using System;
    using System.IO;
    using ArtOfTest.WebAii.Core;
    using ArtOfTest.WebAii.ObjectModel;
    using ArtOfTest.WebAii.TestTemplates;
    using Cassini;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Summary description for JsUnitTestRunnerTest
    /// </summary>
    [TestClass]
    public class JsUnitTestRunnerTest : BaseTest
    {
        public const string JsUnitSuitePage = "Home/Suite/";

        #region [Setup / TearDown]

        private TestContext testContextInstance = null;
        /// <summary>
        ///Gets or sets the VS test context which provides
        ///information about and functionality for the
        ///current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }


        // Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            #region WebAii Initialization

            // Initializes WebAii manager to be used by the test case.
            // If a WebAii configuration section exists, settings will be
            // loaded from it. Otherwise, will create a default settings
            // object with system defaults.
            //
            // Note: We are passing in a delegate to the VisualStudio
            // testContext.WriteLine() method in addition to the Visual Studio
            // TestLogs directory as our log location. This way any logging
            // done from WebAii (i.e. Manager.Log.WriteLine()) is
            // automatically logged to the VisualStudio test log and
            // the WebAii log file is placed in the same location as VS logs.
            //
            // If you do not care about unifying the log, then you can simply
            // initialize the test by calling Initialize() with no parameters;
            // that will cause the log location to be picked up from the config
            // file if it exists or will use the default system settings (C:\WebAiiLog\)
            // You can also use Initialize(LogLocation) to set a specific log
            // location for this test.

            // Pass in 'true' to recycle the browser between test methods
            Initialize(false, this.TestContext.TestLogsDir, new TestContextWriteLine(this.TestContext.WriteLine));

            // If you need to override any other settings coming from the
            // config section you can comment the 'Initialize' line above and instead
            // use the following:

            /*

            // This will get a new Settings object. If a configuration
            // section exists, then settings from that section will be
            // loaded

            Settings settings = GetSettings();

            // Override the settings you want. For example:
            settings.DefaultBrowser = BrowserType.FireFox;

            // Now call Initialize again with your updated settings object
            Initialize(settings, new TestContextWriteLine(this.TestContext.WriteLine));

            */

            // Set the current test method. This is needed for WebAii to discover
            // its custom TestAttributes set on methods and classes.
            // This method should always exist in [TestInitialize()] method.
            SetTestMethod(this, (string)TestContext.Properties["TestName"]);

            #endregion

            //
            // Place any additional initialization here
            //

        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {

            //
            // Place any additional cleanup here
            //

            #region WebAii CleanUp

            // Shuts down WebAii manager and closes all browsers currently running
            // after each test. This call is ignored if recycleBrowser is set
            this.CleanUp();

            #endregion
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            // This will shut down all browsers if
            // recycleBrowser is turned on. Else
            // will do nothing.
            ShutDown();
        }

        #endregion

        [Description("Runs JsUnit test suite"), CssProjectStructure("vstfs:///Classification/Node/7ba28c14-9507-4aba-825a-c95d4fc49de0"), TestMethod]
        public void JsUnit_tests_run_without_errors()
        {
            int failuresCount;

            using (var application = new CassiniHostedWebApplication(Directory.GetCurrentDirectory(), 12345))
            {
                string startPageUrl = String.Format("{0}?autorun=true&testPage={0}{1}", application.BaseUrl, JsUnitSuitePage);

                Manager.LaunchNewBrowser(BrowserType.FireFox);

                //ActiveBrowser.ClearCache(BrowserCacheType.Cookies);
                ActiveBrowser.NavigateTo(startPageUrl);

                var frames = ActiveBrowser.Frames;

                Element status = frames["mainStatus"].Find.ById("content");

                Wait.For(s => s.TextContent.StartsWith("Done"), status, 10 * (60 * 1000));

                failuresCount = Int32.Parse(frames["mainCountsFailures"].Find.ById("content").TextContent);
            }

            Assert.IsTrue(failuresCount == 0);
        }

        private class CassiniHostedWebApplication : IDisposable
        {
            private Server webServer;

            public string BaseUrl { get; private set; }

            public CassiniHostedWebApplication(string rootPath, int port)
            {
                BaseUrl = String.Format("http://localhost:{0}/", port);

                webServer = new Server(port, "/", rootPath);
                webServer.Start();
            }

            public void Dispose()
            {
                if (webServer != null)
                {
                    webServer.Stop();
                    webServer = null;
                }
            }
        }
    }
}

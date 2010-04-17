// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using Extensions;
    using Infrastructure;
    using System.Text;

    /// <summary>
    /// Manages ASP.NET MVC javascript files and statements.
    /// </summary>
    public class ScriptRegistrar : IScriptableComponentContainer
    {
        internal const string jQuery = "jquery-1.4.2.js";
        internal const string jQueryValidation = "jquery.validate.js";

        /// <summary>
        /// Used to ensure that the same instance is used for the same HttpContext.
        /// </summary>
        public static readonly string Key = typeof(ScriptRegistrar).AssemblyQualifiedName;

        private static readonly IList<string> frameworkScriptFileNames = new List<string> { jQuery };

        private readonly IList<IScriptableComponent> scriptableComponents;

        private string assetHandlerPath;
        private bool hasRendered;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptRegistrar"/> class.
        /// </summary>
        /// <param name="scripts">The scripts.</param>
        /// <param name="scriptableComponents">The scriptable components.</param>
        /// <param name="viewContext">The view context.</param>
        /// <param name="assetItemMerger">The asset merger.</param>
        /// <param name="scriptWrapper">The script wrapper.</param>
        public ScriptRegistrar(WebAssetItemCollection scripts, IList<IScriptableComponent> scriptableComponents, ViewContext viewContext, IWebAssetItemMerger assetItemMerger, ScriptWrapperBase scriptWrapper)
        {
            Guard.IsNotNull(scripts, "scripts");
            Guard.IsNotNull(scriptableComponents, "scriptableComponents");
            Guard.IsNotNull(viewContext, "viewContext");
            Guard.IsNotNull(assetItemMerger, "assetItemMerger");
            Guard.IsNotNull(scriptWrapper, "scriptWrapper");

            if (viewContext.HttpContext.Items[Key] != null)
            {
                throw new InvalidOperationException(Resources.TextResource.OnlyOneScriptRegistrarIsAllowedInASingleRequest);
            }

            viewContext.HttpContext.Items[Key] = this;

            DefaultGroup = new WebAssetItemGroup("default", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath };
            Scripts = scripts;
            Scripts.Insert(0, DefaultGroup);

            this.scriptableComponents = scriptableComponents;
            ViewContext = viewContext;
            AssetMerger = assetItemMerger;
            ScriptWrapper = scriptWrapper;
            AssetHandlerPath = WebAssetHttpHandler.DefaultPath;

            OnDocumentReadyActions = new List<Action>();
            OnDocumentReadyStatements = new List<string>();
            OnWindowUnloadActions = new List<Action>();
            OnWindowUnloadStatements = new List<string>();
        }

        /// <summary>
        /// Gets the framework script file names.
        /// </summary>
        /// <value>The framework script file names.</value>
        public static IList<string> FrameworkScriptFileNames
        {
            [DebuggerStepThrough]
            get
            {
                return frameworkScriptFileNames;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [exclude framework scripts].
        /// </summary>
        /// <value>
        /// <c>true</c> if [exclude framework scripts]; otherwise, <c>false</c>.
        /// </value>
        public bool ExcludeFrameworkScripts
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the asset handler path. Path must be a virtual path. The default value is set to <see cref="WebAssetHttpHandler.DefaultPath"/>.
        /// </summary>
        /// <value>The asset handler path.</value>
        public string AssetHandlerPath
        {
            [DebuggerStepThrough]
            get
            {
                return assetHandlerPath;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotVirtualPath(value, "value");

                assetHandlerPath = value;
            }
        }

        /// <summary>
        /// Gets the default script group.
        /// </summary>
        /// <value>The default group.</value>
        public WebAssetItemGroup DefaultGroup
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable globalization].
        /// </summary>
        /// <value><c>true</c> if [enable globalization]; otherwise, <c>false</c>.</value>
        public bool EnableGlobalization
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the scripts that will be rendered in the view.
        /// </summary>
        /// <value>The scripts.</value>
        public WebAssetItemCollection Scripts
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the on document ready actions.
        /// </summary>
        /// <value>The on page load actions.</value>
        public IList<Action> OnDocumentReadyActions
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the on document ready statements that is used in <code>RenderAction</code>.
        /// </summary>
        /// <value>The on page load actions.</value>
        public IList<string> OnDocumentReadyStatements
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the on window unload actions.
        /// </summary>
        /// <value>The on page unload actions.</value>
        public IList<Action> OnWindowUnloadActions
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the on window unload statements.that is used in <code>RenderAction</code>.
        /// </summary>
        /// <value>The on page load actions.</value>
        public IList<string> OnWindowUnloadStatements
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the view context.
        /// </summary>
        /// <value>The view context.</value>
        protected ViewContext ViewContext
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the asset merger.
        /// </summary>
        /// <value>The asset merger.</value>
        protected IWebAssetItemMerger AssetMerger
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the script wrapper that is used to write the script statements.
        /// </summary>
        /// <value>The script wrapper.</value>
        protected ScriptWrapperBase ScriptWrapper
        {
            get;
            private set;
        }

        /// <summary>
        /// Registers the scriptable component.
        /// </summary>
        /// <param name="component">The component.</param>
        public virtual void Register(IScriptableComponent component)
        {
            Guard.IsNotNull(component, "component");

            if (!scriptableComponents.Contains(component))
            {
                scriptableComponents.Add(component);
            }
        }

        /// <summary>
        /// Writes the scripts in the response.
        /// </summary>
        public void Render()
        {
            if (hasRendered)
            {
                throw new InvalidOperationException(Resources.TextResource.YouCannotCallRenderMoreThanOnce);
            }

            if (ViewContext.HttpContext.Request.Browser.EcmaScriptVersion.Major >= 1)
            {
                Write(ViewContext.HttpContext.Response.Output);
            }

            hasRendered = true;
        }

        /// <summary>
        /// Writes all script source and script statements.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected virtual void Write(TextWriter writer)
        {
            WriteScriptSources(writer);
            WriteScriptStatements(writer);
        }

        private void WriteScriptSources(TextWriter writer)
        {
            IList<string> mergedList = new List<string>();

            bool isSecured = ViewContext.HttpContext.Request.IsSecureConnection;
            bool canCompress = ViewContext.HttpContext.Request.CanCompress();

            Action<WebAssetItemCollection> append =
                assets =>
                {
                    IList<string> result = AssetMerger.Merge("application/x-javascript", AssetHandlerPath, isSecured, canCompress, assets);

                    if (!result.IsNullOrEmpty())
                    {
                        mergedList.AddRange(result);
                    }
                };

            CopyFrameworkScriptFiles();

            CopyScriptFilesFromComponents();

            if (!Scripts.IsEmpty())
            {
                append(Scripts);
            }

            if (!mergedList.IsEmpty())
            {
                foreach (string script in mergedList)
                {
                    writer.WriteLine("<script type=\"text/javascript\" src=\"{0}\"></script>".FormatWith(script));
                }
            }
        }

        private void WriteScriptStatements(TextWriter writer)
        {
            StringBuilder cleanUpScripts = WriteCleanUpScripts();

            bool shouldWriteOnDocumentReady = !scriptableComponents.IsEmpty() || !OnDocumentReadyActions.IsEmpty() || !OnDocumentReadyStatements.IsEmpty();
            bool shouldWriteOnWindowUnload = !OnWindowUnloadActions.IsEmpty() || !OnWindowUnloadStatements.IsEmpty() || cleanUpScripts.Length > 0;

            if (shouldWriteOnDocumentReady || shouldWriteOnWindowUnload)
            {
                bool isFirst;

                writer.WriteLine("<script type=\"text/javascript\">{0}//<![CDATA[".FormatWith(Environment.NewLine));

                // pageLoad

                if (shouldWriteOnDocumentReady)
                {
                    writer.WriteLine(ScriptWrapper.OnPageLoadStart);

                    // globalization
                    if (EnableGlobalization && CultureInfo.CurrentCulture.Name != "en-US")
                    {
                        writer.WriteLine("if (!jQuery.telerik) jQuery.telerik = {};");
                        GlobalizationInfo globalizationInfo = new GlobalizationInfo(CultureInfo.CurrentCulture);

                        writer.Write("jQuery.telerik.cultureInfo=");
                        writer.Write(new JavaScriptSerializer().Serialize(globalizationInfo.ToDictionary()));
                        writer.WriteLine(";");
                    }

                    isFirst = true;

                    foreach (IScriptableComponent component in scriptableComponents.Where(s => !s.IsSelfInitialized))
                    {
                        if (!isFirst)
                        {
                            writer.WriteLine();
                        }

                        component.WriteInitializationScript(writer);
                        isFirst = false;
                    }

                    isFirst = true;

                    foreach (Action action in OnDocumentReadyActions)
                    {
                        if (!isFirst)
                        {
                            writer.WriteLine();
                        }

                        action();

                        isFirst = false;
                    }

                    isFirst = true;

                    foreach (string statement in OnDocumentReadyStatements)
                    {
                        if (!isFirst)
                        {
                            writer.WriteLine();
                        }

                        writer.Write(statement);

                        isFirst = false;
                    }

                    writer.WriteLine(ScriptWrapper.OnPageLoadEnd);
                }

                // pageUnload
                if (shouldWriteOnWindowUnload)
                {
                    writer.WriteLine(ScriptWrapper.OnPageUnloadStart);

                    isFirst = true;

                    foreach (Action action in OnWindowUnloadActions)
                    {
                        if (!isFirst)
                        {
                            writer.WriteLine();
                        }

                        action();

                        isFirst = false;
                    }

                    isFirst = true;

                    foreach (string statement in OnWindowUnloadStatements)
                    {
                        if (!isFirst)
                        {
                            writer.WriteLine();
                        }

                        writer.Write(statement);

                        isFirst = false;
                    }

                    writer.WriteLine(cleanUpScripts.ToString()); // write clean up scripts

                    writer.WriteLine(ScriptWrapper.OnPageUnloadEnd);
                }

                writer.Write("//]]>{0}</script>".FormatWith(Environment.NewLine));
            }
        }

        private StringBuilder WriteCleanUpScripts()
        {
            bool isFirst = true;

            StringWriter cleanupWriter = new StringWriter();

            foreach (IScriptableComponent component in scriptableComponents)
            {
                if (!isFirst)
                {
                    cleanupWriter.WriteLine();
                }

                component.WriteCleanupScript(cleanupWriter);

                isFirst = false;
            }

            return cleanupWriter.GetStringBuilder();
        }

        private void CopyScriptFilesFromComponents()
        {
            foreach (IScriptableComponent component in scriptableComponents)
            {
                string assetKey = string.IsNullOrEmpty(component.AssetKey) ? "default" : component.AssetKey;
                string filesPath = component.ScriptFilesPath;

                if (assetKey.IsCaseInsensitiveEqual("default") && WebAssetDefaultSettings.ScriptFilesPath.IsCaseInsensitiveEqual(filesPath))
                {
                    if (!DefaultGroup.DefaultPath.IsCaseInsensitiveEqual(WebAssetDefaultSettings.ScriptFilesPath))
                    {
                        filesPath = DefaultGroup.DefaultPath;
                    }
                }

                component.ScriptFileNames.Each(source => Scripts.Add(assetKey, PathHelper.CombinePath(filesPath, source)));
            }
        }

        private void CopyFrameworkScriptFiles()
        {
            if (!ExcludeFrameworkScripts)
            {
                FrameworkScriptFileNames.Reverse().Each(source => DefaultGroup.Items.Insert(0, new WebAssetItem(PathHelper.CombinePath(DefaultGroup.DefaultPath, source))));
            }
        }
    }
}
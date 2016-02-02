//------------------------------------------------------------------------------
// <copyright file="HydraLanguagePackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

namespace HydraLanguagePackage
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#1110", "#1112", "1.0", IconResourceID = 1400)] // Info on this package for Help/About
    [Guid(HydraLanguagePackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideService(typeof(HydraLanguageService), ServiceName = "Hydra language service")]
    [ProvideLanguageService(typeof(HydraLanguageService), "Hydra", 0, EnableCommenting = true, MatchBraces = true, RequestStockColors = true)]
    [ProvideLanguageExtension(typeof(HydraLanguageService), ".hy")]
    public sealed class HydraLanguagePackage : Package
    {
        /// <summary>
        /// HydraLanguagePackage GUID string.
        /// </summary>
        public const string PackageGuidString = "6f3c5ec9-ede1-4212-a846-976e2068ce77";
        private uint m_ComponentId;

        /// <summary>
        /// Initializes a new instance of the <see cref="HydraLanguagePackage"/> class.
        /// </summary>
        public HydraLanguagePackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            IServiceContainer serviceContainer = this;
            var langService = new HydraLanguageService();
            langService.SetSite(this);
            serviceContainer.AddService(typeof(HydraLanguageService), langService, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (m_ComponentId != 0)
            {
                IOleComponentManager mgr = GetService(typeof(SOleComponentManager))
                                           as IOleComponentManager;
                if (mgr != null)
                {
                    int hr = mgr.FRevokeComponent(m_ComponentId);
                }
                m_ComponentId = 0;
            }

            base.Dispose(disposing);
        }
    }
}

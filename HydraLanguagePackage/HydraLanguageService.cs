using System;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;

namespace HydraLanguagePackage
{
    class HydraLanguageService : LanguageService
    {
        private LanguagePreferences m_LanguagePreferences;
        private HydraVsScanner m_HydraVsScanner;

        public override LanguagePreferences GetLanguagePreferences()
        {
            if (m_LanguagePreferences == null)
            {
                m_LanguagePreferences = new LanguagePreferences(Site, typeof (HydraLanguageService).GUID, Name);

                m_LanguagePreferences.Init();
            }

            return m_LanguagePreferences;
        }

        public override IScanner GetScanner(IVsTextLines buffer)
        {
            if (m_HydraVsScanner == null)
            {
                m_HydraVsScanner = new HydraVsScanner(buffer);
            }

            return m_HydraVsScanner;
        }

        public override AuthoringScope ParseSource(ParseRequest req)
        {
            return new HydraAuthoringScope();
        }

        public override string GetFormatFilterList()
        {
            return "*.hy";
        }

        public override string Name => "Hydra";
    }
}

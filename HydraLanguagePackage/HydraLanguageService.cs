using System;
using System.Drawing;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;

namespace HydraLanguagePackage
{
    class HydraLanguageService : LanguageService
    {
        private LanguagePreferences m_LanguagePreferences;
        private HydraVsScanner m_HydraVsScanner;
        private readonly ColorableItem[] m_ColorableItems;

        public HydraLanguageService() : base()
        {
            m_ColorableItems = new ColorableItem[]
            {
                /*1*/ new ColorableItem("Hydra - Keyword", "Hydra - Keyword", COLORINDEX.CI_BLUE, COLORINDEX.CI_USERTEXT_BK, Color.Empty, Color.Empty, FONTFLAGS.FF_DEFAULT),
                /*2*/ new ColorableItem("Hydra - Comment", "Hydra - Comment", COLORINDEX.CI_DARKGREEN, COLORINDEX.CI_USERTEXT_BK, Color.Empty, Color.Empty, FONTFLAGS.FF_DEFAULT),
                /*3*/ new ColorableItem("Hydra - Identifier", "Hydra - Identifier", COLORINDEX.CI_SYSPLAINTEXT_FG, COLORINDEX.CI_USERTEXT_BK, Color.Empty, Color.Empty, FONTFLAGS.FF_DEFAULT),
                /*4*/ new ColorableItem("Hydra - String", "Hydra - String", COLORINDEX.CI_RED, COLORINDEX.CI_USERTEXT_BK, Color.Empty, Color.Empty, FONTFLAGS.FF_DEFAULT),
                /*5*/ new ColorableItem("Hydra - Number", "Hydra - Number", COLORINDEX.CI_DARKBLUE, COLORINDEX.CI_USERTEXT_BK, Color.Empty, Color.Empty, FONTFLAGS.FF_DEFAULT),
            };
        }

        public override int GetColorableItem(int index, out IVsColorableItem item)
        {
            item = m_ColorableItems[index - 1];
            return VSConstants.S_OK;
        }

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

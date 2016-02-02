using System;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;

namespace HydraLanguagePackage
{
    class HydraLanguageService : LanguageService
    {
        private LanguagePreferences m_LanguagePreferences;

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
            throw new NotImplementedException();
        }

        public override AuthoringScope ParseSource(ParseRequest req)
        {
            throw new NotImplementedException();
        }

        public override string GetFormatFilterList()
        {
            throw new NotImplementedException();
        }

        public override string Name { get; }
    }
}

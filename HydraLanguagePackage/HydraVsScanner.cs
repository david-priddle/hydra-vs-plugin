using Hydra.Compiler;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;

namespace HydraLanguagePackage
{
    class HydraVsScanner : IScanner
    {
        private readonly IVsTextBuffer m_Buffer;
        private readonly HydraScanner m_HydraScanner;

        public HydraVsScanner(IVsTextBuffer buffer)
        {
            m_Buffer = buffer;
            m_HydraScanner = new HydraScanner();
        }

        public void SetSource(string source, int offset)
        {
            m_HydraScanner.SetSource(source, offset);
        }

        public bool ScanTokenAndProvideInfoAboutIt(TokenInfo tokenInfo, ref int state)
        {
            var token = m_HydraScanner.NextToken();

            if (token.TokenType == HydraTokenType.ID)
            {
                tokenInfo.Type = TokenType.Identifier;
            }
            else
            {
                tokenInfo.Type = TokenType.Unknown;
            }
        }
    }
}

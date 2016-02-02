using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Hydra.Tools.VisualStudio
{
    class HydraLanguageInfo : IVsLanguageInfo
    {
        public int GetLanguageName(out string bstrName)
        {
            bstrName = HydraConstants.LanguageName;
            return VSConstants.S_OK;
        }

        public int GetFileExtensions(out string pbstrExtensions)
        {
            pbstrExtensions = HydraConstants.FileExtension;
            return VSConstants.S_OK;
        }

        public int GetColorizer(IVsTextLines pBuffer, out IVsColorizer ppColorizer)
        {
            ppColorizer = null;
            return VSConstants.E_FAIL;
        }

        public int GetCodeWindowManager(IVsCodeWindow pCodeWin, out IVsCodeWindowManager ppCodeWinMgr)
        {
            ppCodeWinMgr = null;
            return VSConstants.E_FAIL;
        }
    }
}

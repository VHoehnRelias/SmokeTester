using Microsoft.Win32;

namespace Cmt.Online.Web.TestUi.Selenium
{
    public static class InternetExplorerHelper
    {
        private static int m_PreviousZoomFactor = 0;

        public static void SetZoom100()
        {
            // Get DPI setting.
            RegistryKey dpiRegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop\\WindowMetrics");
            int dpi = (int)dpiRegistryKey.GetValue("AppliedDPI");
            // 96 DPI / Smaller / 100%
            int zoomFactor100Percent = 100000;
            switch (dpi)
            {
                case 120: // Medium / 125%
                    zoomFactor100Percent = 80000;
                    break;
                case 144: // Larger / 150%
                    zoomFactor100Percent = 66667;
                    break;
            }
            // Get IE zoom.
            RegistryKey zoomRegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Zoom", true);
            int currentZoomFactor = (int)zoomRegistryKey.GetValue("ZoomFactor");
            if (currentZoomFactor != zoomFactor100Percent)
            {
                // Set IE zoom and remember the previous value.
                zoomRegistryKey.SetValue("ZoomFactor", zoomFactor100Percent, RegistryValueKind.DWord);
                m_PreviousZoomFactor = currentZoomFactor;
            }
        }

        public static void ResetZoom()
        {
            if (m_PreviousZoomFactor > 0)
            {
                // Reapply the previous value.
                RegistryKey zoomRegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Zoom", true);
                zoomRegistryKey.SetValue("ZoomFactor", m_PreviousZoomFactor, RegistryValueKind.DWord);
            }
        }
    }
}
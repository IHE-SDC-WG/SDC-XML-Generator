using System.Windows;

namespace SDC.Gen.UI.WPF
{
    public partial class Application
    {

        // Application-level events, such as Startup, Exit, and DispatcherUnhandledException
        // can be handled in this file.
        // Sub New()
        // MessageBox.Show("Sub New", "", MessageBoxButton.OK, MessageBoxImage.Information)
        // End Sub
        // Friend XmlGenAPI As New XML_Gen_API.GenXDT

        private void OnAppStartup_UpdateThemeName(object sender, StartupEventArgs e)
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
        }
    }
}
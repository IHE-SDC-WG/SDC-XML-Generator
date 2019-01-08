Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.
    'Sub New()
    '    MessageBox.Show("Sub New", "", MessageBoxButton.OK, MessageBoxImage.Information)
    'End Sub
    'Friend XmlGenAPI As New XML_Gen_API.GenXDT

    Private Sub OnAppStartup_UpdateThemeName(sender As Object, e As StartupEventArgs)

        DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName()
    End Sub
End Class

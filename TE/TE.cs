using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace TE
{
    public class TEinterop : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern
            bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern
            bool IsIconic(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern
            bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// Initializes a new instance of the <see cref="TEinterop"/> class.
        /// </summary>
        public TEinterop(string accessPath)
        {
            _AccessPath = accessPath;

            if (accessPath == "")
            {
                Properties.Settings.Default.Reload(); //probably does nothing
                accessPath = Properties.Settings.Default.AccessPath;
            }
        }
        ~TEinterop()
        { this.Dispose(); }

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };
        private static string _AccessPath;
        private static Microsoft.Office.Interop.Access.Application _oAccess = null;

        private static string AccessPath
        {
            get { return _AccessPath; }
            set { _AccessPath = value; }
        }


        public void LookupItemByCKey(string TemplateCKey, string ItemCKey = "")
        {
            if (_oAccess == null)
            {
                try
                {
                    _oAccess = new Microsoft.Office.Interop.Access.Application();
                    _oAccess.Visible = false;

                    string curPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                    AccessPath = $"{curPath}\\{AccessPath}";

                    //AccessPath = AccessPath.Replace("%USERNAME%", System.Environment.UserName);
                    _oAccess.OpenCurrentDatabase(AccessPath);
                }
                catch
                {
                    if (!(_oAccess is null)) _oAccess.Visible = true;
                    MessageBox.Show($"LookupItemByCKey: There was a problem (0) loading the TE or Access at Path: {AccessPath}.  " +
                        $"You may need to close Access manually.");
                }
            }

            try
            {
                object oMissing = System.Reflection.Missing.Value;
                if (_oAccess.hWndAccessApp() > 0)
                {
                    try
                    {
                        _oAccess.Visible = true;
                        BringToFront();
        RunAccessFunction("InvokeLookup", TemplateCKey, ItemCKey);
        //RunAccessFunction("cboTemplateRequery", TemplateCKey);
    }
                    catch (Exception ex)
                    {
                        //if exception occurrs here most likely Access instance from this application was closed by the user
                        //start a new instance and try again

                        //_oAccess = new Microsoft.Office.Interop.Access.Application();
                        //_oAccess.Visible = false;
                        //_oAccess.OpenCurrentDatabase(AccessPath);
                        //_oAccess.Visible = true;
                        //RunAccessFunction("InvokeLookup", TemplateCKey, ItemCKey); //need an option to to ignore the ItemCKey
                        //BringToFront();
                        MessageBox.Show("LookupItemByCKey: There was a problem (1) loading TE in LookupItemByCKey: " + ex);
                        _oAccess.Application.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveNone);
                        _oAccess = null;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem (2) loading TE in LookupItemByCKey");
                _oAccess.Application.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveNone);
                _oAccess = null;
            }
        }

        private static void RunAccessFunction(string functionname, string Parameter1, string Parameter2 = "")
{
    object oMissing = System.Reflection.Missing.Value;
    try
    {
        _oAccess.Run(functionname, Parameter1, Parameter2, ref oMissing, ref oMissing,
                   ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                   ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                   , ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                   , ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                   , ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                   , ref oMissing, ref oMissing);
    }
    catch (Exception ex)
    {
        //throw new Exception("Error running function in Access in RunAccessFunction: " + ex.Message);
        MessageBox.Show("RunAccessFunction: There was a problem quitting the TE: " + ex);
        _oAccess.Application.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveNone);
        _oAccess = null;
    }
}

public static void Close()
{
    if (_oAccess != null)
    {
        _oAccess.CloseCurrentDatabase();
        _oAccess.Quit();
    }
}

public static void Hide()
{
    if (_oAccess != null)
        _oAccess.Visible = false;
}

public static void Show()
{
    if (_oAccess != null)
        _oAccess.Visible = true;
}

public static void BringToFront()
{
    System.Diagnostics.Process[] bProcess = System.Diagnostics.Process.GetProcessesByName("MSACCESS");
    int handle = _oAccess.hWndAccessApp();
    foreach (System.Diagnostics.Process p in bProcess)
    {
        IntPtr hWnd = p.MainWindowHandle;
        if (hWnd.ToInt32() == handle)
        {
            if (IsIconic(hWnd))
            {
                ShowWindowAsync(hWnd, (int)ShowWindowEnum.Restore);
            }
            SetForegroundWindow(hWnd);
        }
    }
}

public void Dispose()
{
    if (_oAccess != null)
    {
        try
        {
            _oAccess.Application.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveNone);
            _oAccess.Quit();// TODO: does this force unload from RAM?
        }
        catch (Exception ex)
        {
            MessageBox.Show("Dispose: There was a problem quitting the TE: " + ex.InnerException);
        }
    }
}
    }
}

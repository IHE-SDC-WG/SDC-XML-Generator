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


            if (accessPath == "")
            {
                Properties.Settings.Default.Reload(); //probably does nothing
                AccessPath = Properties.Settings.Default.AccessPath;
            }
            else
            { AccessPath = accessPath; }
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
            string curPath = "";
            if (_oAccess == null)
            {
                try
                {
                    _oAccess = new Microsoft.Office.Interop.Access.Application();
                    _oAccess.Visible = false;

                    curPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                    curPath = $"{curPath}\\{AccessPath}";

                    Application.UseWaitCursor = true;
                    Application.DoEvents();
                    _oAccess.OpenCurrentDatabase(curPath);
                    Application.UseWaitCursor = false;
                }
                catch (Exception ex)
                { //Probably, user has closed Access, the TE is closed, but we are still holding on to a handle to the Access process
                    Application.UseWaitCursor = false;
                    //if (!(_oAccess is null)) _oAccess.Visible = true;
                    MessageBox.Show("LookupItemByCKey: There was a problem (0) loading TE in LookupItemByCKey.  Details:\r\n\r\n" + ex);
                    CloseAccess();
                    return;
                }
            }

            if (_oAccess != null)
                try
                {
                    object oMissing = System.Reflection.Missing.Value;
                    if (_oAccess.hWndAccessApp() > 0)
                    {
                        try
                        {
                            BringToFront();
                            RunAccessFunction("InvokeLookup", TemplateCKey, ItemCKey);
                            if (_oAccess != null) _oAccess.Visible = true;
                            //RunAccessFunction("cboTemplateRequery", TemplateCKey);
                        }
                        catch (Exception ex)
                        { //Probably, user has closed Access, the TE is closed, but we are still holding on to a handle to the Access process
                            MessageBox.Show("LookupItemByCKey: There was a problem (1) loading TE in LookupItemByCKey.  Details:\r\n\r\n" + ex);
                            CloseAccess();
                        }
                    }

                }
                catch (Exception ex)
                { //Probably, user has closed Access, the TE is closed, but we are still holding on to a handle to the Access process
                    MessageBox.Show("There was a problem (2) loading TE in LookupItemByCKey");
                    CloseAccess();
                }
        }

        private static void RunAccessFunction(string functionname, string Parameter1, string Parameter2 = "")
        {
            object oMissing = System.Reflection.Missing.Value;
            try
            {
                if (_oAccess != null) _oAccess.Run(functionname, Parameter1, Parameter2, ref oMissing, ref oMissing,
                           ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                           ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                           , ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                           , ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                           , ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                           , ref oMissing, ref oMissing);
            }
            catch (Exception ex)
            { //Probably, user has closed Access, the TE is closed, but we are still holding on to a handle to the Access process
                MessageBox.Show("RunAccessFunction: There was a problem activating the TE. Details: \r\n\r\n" + ex);
                CloseAccess();
            }
        }

        private static void CloseAccess()
        {if (_oAccess != null) 
            try
            {
                _oAccess.Application.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveNone);
                _oAccess.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveNone);  //in case Access is still alive
                _oAccess = null;
            }
            catch
            {
                try //in case Access is still alive} }
                { _oAccess.Quit(Microsoft.Office.Interop.Access.AcQuitOption.acQuitSaveNone); }
                catch { }
            }
            finally
            { _oAccess = null; }
        }

        public static void Close()
        {
            CloseAccess();
        }

        public static void Hide()
        {
            if (_oAccess != null) _oAccess.Visible = false;
        }

        public static void Show()
        {
            if (_oAccess != null) _oAccess.Visible = true;
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
            CloseAccess();
        }
    }
}

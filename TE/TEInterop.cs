using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace TE
{
public static class TEInterop
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
        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };
        static Microsoft.Office.Interop.Access.Application _oAccess = null;

        private static readonly string AccessPath
        {
            get { return Properties.Settings.Default.TEpath; }
        }

        public static void LookupItemByCKey(string TemplateCKey, string ItemCKey)
        {
            if (_oAccess == null)
            {
                _oAccess = new Microsoft.Office.Interop.Access.Application();
                _oAccess.Visible = false;
                string path = System.Windows.Forms.Application.ExecutablePath;
                path = System.IO.Path.GetDirectoryName(path);
                _oAccess.OpenCurrentDatabase(path + "\\TE\\eCC_TE.adp");
                
            }
            _oAccess.Visible = true;
            try
            {
                object oMissing = System.Reflection.Missing.Value;
                if (_oAccess.hWndAccessApp() > 0)
                {
                    try
                    {
                        RunAccessFunction("InvokeLookup", TemplateCKey, ItemCKey);
                       
                    }
                    catch
                    {
                        //if exception occurrs here most likely Access instance from this application was closed by the user
                        //start a new instance and try again
                        _oAccess = new Microsoft.Office.Interop.Access.Application();
                        _oAccess.Visible = false;
                        string path = System.Windows.Forms.Application.ExecutablePath;
                        path = System.IO.Path.GetDirectoryName(path);
                        _oAccess.OpenCurrentDatabase(path + "\\TE\\eCC_TE.adp");
                        _oAccess.Visible = true;
                        RunAccessFunction("InvokeLookup", TemplateCKey, ItemCKey);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error in TEInterop: " + ex.Message);
            }
        }

        private static void RunAccessFunction(string functionname, string Parameter1, string Parameter2)
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
            catch(Exception ex)
           {
               throw new Exception("Error running function in Access: " + ex.Message);
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
            if(_oAccess!=null)
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
    }
}
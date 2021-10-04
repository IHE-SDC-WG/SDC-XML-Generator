using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace SDC.Gen.UI.WPF
{
    // Imports TE


    public partial class MainWindow : DXWindow
    {
        // Implements IDisposable

        private string BrowserPath { get; set; }

        private TE.TEinterop TEinterop;

        private string FilePath { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();
            DataContext = new DataSource();
            TEinterop = new TE.TEinterop(My.MySettingsProperty.Settings.TEpath);


            // If filePath = "" Then
            // filePath = My.Settings.FilePath  'filePath, when set here,  will override settings files from other components
            // If Not My.Computer.FileSystem.DirectoryExists(filePath) Then

            // Try
            // My.Computer.FileSystem.CreateDirectory(filePath)
            // Catch ex As Exception
            // MsgBox("Unable to create the file path: " + FilePath & vbCrLf & ex.Message + vbCrLf + ex.Data.ToString(), MsgBoxStyle.Exclamation, "SDC Folder was not created")
            // End Try
            // End If
            // End If
        }

        private void btnGenAll_Click(object sender, RoutedEventArgs e)
        {
            BrowserPath = this.txtBrowserPath.Text.Trim();
            // FilePath = txtFilePath.Text.Trim
            var cur = System.Windows.Application.Current.MainWindow.Cursor;
            var Gen = new API.GenXDT(Environment.ExpandEnvironmentVariables(this.txtFilePath.Text.Trim()), My.MySettingsProperty.Settings.XslFileName);
            FilePath = Gen.XMLfilePath;  // if the UI-supplied txtFilePath does not exist, Gen will try to create a default one.  This reassigns Filepath to the default
            this.txtFilePath.Text = FilePath; // makes the same change in the UI
            if (Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(FilePath))
            {
                var templatesMap = new Dictionary<string, string>();
                System.Windows.Application.Current.MainWindow.Cursor = Cursors.Wait;
                foreach (var node in this.TreeListView1.Nodes.Where(n => n.IsChecked == true))
                {
                    System.Data.DataRowView dataRowView = node.Content as System.Data.DataRowView;
                    Debug.Print(dataRowView[0].ToString() + Constants.vbCrLf + dataRowView[1].ToString() + Constants.vbCrLf + dataRowView[2].ToString());
                    templatesMap.Add(dataRowView[0].ToString(), dataRowView[2].ToString()); // cols 0 & 3
                }

                Gen.MakeXDTsFromTemplateMap(templatesMap, FilePath, this.chkCreateHTML.IsChecked == true);
            }
            else
            {
                Interaction.Beep();
                Interaction.MsgBox("The file path for saving SDC XML files does not exist at: " + FilePath, MsgBoxStyle.Exclamation, "Folder does not exist");
            }

            System.Windows.Application.Current.MainWindow.Cursor = cur;
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            string key = this.txtID.Text.Trim();
            string fileName = this.txtFileName.Text.Trim();
            bool loadBrowser = Conversions.ToBoolean(this.chkShowBrowser.EditValue);
            bool createHTML = Conversions.ToBoolean(this.chkCreateHTML.EditValue);
            BrowserPath = this.txtBrowserPath.Text.Trim();
            // FilePath = txtFilePath.Text.Trim
            string ns = this.txtNamespace.Text.Trim();
            var Gen = new API.GenXDT(Environment.ExpandEnvironmentVariables(this.txtFilePath.Text.Trim()), My.MySettingsProperty.Settings.XslFileName);
            FilePath = Gen.XMLfilePath;  // if the UI-supplied txtFilePath does not exist, Gen will try to create a default one.  This reassigns Filepath to the default
            this.txtFilePath.Text = FilePath; // makes the same change in the UI
            if (Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(FilePath))
            {
                if (Operators.CompareString(key, "", true) > 0 && Operators.CompareString(ns, "", true) > 0)
                {
                    key = string.Format("{0}.{1}", key, ns);
                    // TODO: Need to cache this generator, since it takes time to create it new each time.

                    Gen.MakeOneXDT(key, FilePath, loadBrowser, BrowserPath, createHTML);   // , fileName
                }
                else
                {
                    Interaction.Beep();
                    Interaction.MsgBox("You must enter valid values for template Key (ID), Namespace, and Filepath", MsgBoxStyle.Exclamation, "Invalid Entry");
                }
            }
            else
            {
                Interaction.Beep();
                Interaction.MsgBox("The file path for saving SDC XML files does not exist: " + FilePath, MsgBoxStyle.Exclamation, "Folder does not exist");
            }
        }

        public static object Nstr(object InObj, string strDefault = "")
        {
            if (InObj is null)
                return strDefault; // don't allow an unititialized object to be returned
            return InObj;
        }

        public static object N0(object InObj)
        {
            if (InObj is null)
                return 0;
            return InObj;
        }

        private void DXWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void cbCheckAll_Click(object sender, RoutedEventArgs e)
        {
            this.TreeListView1.CheckAllNodes();
        }

        private void cbUncheckAll_Click(object sender, RoutedEventArgs e)
        {
            this.TreeListView1.UncheckAllNodes();
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnGenerate_Click(sender, e);
                this.txtID.SelectAll();
            }
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var col = this.gridControl1.CurrentColumn;
            string cellText;
            // Dim row = TryCast(gridControl1.GetFocusedRow, System.Data.DataRowView)

            if (col.VisibleIndex == 0)
            {
                this.gridControl1.Cursor = Cursors.Wait;
                cellText = this.gridControl1.GetFocusedRowCellDisplayText(this.gridControl1.Columns[0]).ToString();
                var key = Strings.Split(cellText, ".", 2);
                this.txtID.Text = key[0];
                // txtID.UpdateLayout()
                this.txtNamespace.Text = key[1];

                // Conside inmplementing a DoEvents on main UI thread here, to update the UI
                // https://www.devexpress.com/Support/Center/Question/Details/Q322288/gridcontrol-how-to-force-cell-errors-redraw-idxdataerrorinfo
                // https://stackoverflow.com/questions/4502037/where-is-the-application-doevents-in-wpf
                // http://geekswithblogs.net/NewThingsILearned/archive/2008/08/25/refresh--update-wpf-controls.aspx
                // https://www.meziantou.net/2011/06/22/refresh-a-wpf-control

                btnGenerate_Click(sender, e);
                this.gridControl1.Cursor = Cursors.Arrow;
            }
            else if (col.VisibleIndex == 2)
            {
                cellText = this.gridControl1.GetFocusedRowCellDisplayText(this.gridControl1.Columns[0]).ToString();
                // Dim TE = New TE.TEinterop()

                TEinterop.LookupItemByCKey(cellText, "0");
            }
        }

        ~MainWindow()
        {
            TEinterop.Dispose();
        }

        private void gridControl1_GotFocus(object sender, RoutedEventArgs e)
        {
            string cellText = this.gridControl1.GetFocusedRowCellDisplayText(this.gridControl1.Columns[0]).ToString();
            var CTVkey = Strings.Split(cellText, ".", 2);
            this.txtID.Text = CTVkey[0];
            Debug.WriteLine(sender.ToString(), e.ToString());
        }

        private void btnLoadTE_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn is object)
                {
                    string cellText = this.gridControl1.GetFocusedRowCellDisplayText(this.gridControl1.Columns[0]).ToString();
                    var CTVkey = Strings.Split(cellText, ".", 2);
                    this.gridControl1.Cursor = Cursors.Wait;
                    // txtID.Text = CTVkey(0)

                    if (CultureInfo.CurrentCulture.CompareInfo.Compare(btn.Name, "btnKey", CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0) // Geneate XML/HTML
                    {
                        // btn.Content = "Gen: " + cellText
                        // MsgBox(cellText)
                        btnGenerate_Click(sender, e);
                    }

                    if (CultureInfo.CurrentCulture.CompareInfo.Compare(btn.Name, "btnLoadTE", CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0) // Load TE
                    {
                        TEinterop.LookupItemByCKey(cellText, "0");
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error");
            }
            finally
            {
                this.gridControl1.Cursor = Cursors.Arrow;
            }
        }

        private void gridControl1_AutoGeneratingColumn(object sender, AutoGeneratingColumnEventArgs e)
        {
        }

        private void gridControl1_AutoGeneratedColumns(object sender, RoutedEventArgs e)
        {
        }

        private void gridControl1_Loaded(object sender, RoutedEventArgs e)
        {
            return;
            GridControl grid = sender as GridControl;
            if (grid is object)
            {
                for (int i = 0, loopTo = gridControl1.VisibleItems.Count - 1; i <= loopTo; i++)
                {
                    try
                    {
                        int row = grid.GetRowHandleByListIndex(i);
                        var frmElem = grid.View.GetRowElementByRowHandle(row);
                        Button btn = DevExpress.Xpf.Core.Native.LayoutHelper.FindElementByName(frmElem, "btnKey") as Button;
                        var col = grid.Columns[1];
                        var cellVal = grid.GetCellValue(i, col);
                        if (btn is object)
                        {
                            btn.Content = "Gen: " + cellVal.ToString();
                            btn.ToolTip = btn.Content;
                        }
                    }
                    // btn.UpdateLayout()
                    // Dim unused = btn.CacheMode
                    // MainWindow.Owner.UpdateLayout()
                    // btn.Visibility =
                    catch (Exception ex)
                    {
                        Interaction.MsgBox("There are more VisibleItems than Rows in the control", MsgBoxStyle.Exclamation, "Error in loading rows");
                    }
                }
            }
        }

        public static async System.Threading.Tasks.Task<object> GetChildrenAsync(GridControl G, int row, int column)
        {
            var rList = await G.GetRowsAsync(0, G.VisibleRowCount);
            foreach (var r in rList)
            {
                RowControl rc = r as RowControl;
                Button btn = rc.FindName("btnKey") as Button;
                // btn.Content = rc.
            }

            return default(System.Threading.Tasks.Task<object>);
        }

        public void DumpLogicalTree(object parent, int level)
        {
            string typeName = parent.GetType().Name;
            string name = null;
            DependencyObject doParent = parent as DependencyObject;
            if (doParent is object)
            {
                name = Conversions.ToString(doParent.GetValue(NameProperty) ?? "");
            }
            else
            {
                name = parent.ToString();
            }

            Trace.WriteLine(string.Format("{0}: {1}", typeName, name));
            if (doParent is null)
                return;
            foreach (object child in LogicalTreeHelper.GetChildren(doParent))
                DumpLogicalTree(child, level + 1);
        }

        public void DumpVisualTree(DependencyObject parent, int level)
        {
            string typeName = parent.GetType().Name;
            string name = Conversions.ToString(parent.GetValue(NameProperty) ?? "");
            Trace.WriteLine(string.Format("{0}: {1}", typeName, name));
            if (parent is null)
                return;
            for (int i = 0, loopTo = VisualTreeHelper.GetChildrenCount(parent) - 1; i <= loopTo; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                DumpVisualTree(child, level + 1);
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj is object)
            {
                for (int i = 0, loopTo = VisualTreeHelper.GetChildrenCount(depObj) - 1; i <= loopTo; i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is object && child is T)
                    {
                        yield return (T)child;
                        // Trace.WriteLine(String.Format("{0}: {1}", typeName, name))
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0, loopTo = VisualTreeHelper.GetChildrenCount(obj) - 1; i <= loopTo; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is object && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    var childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild is object)
                        return childOfChild;
                }
            }

            return null;
        }
    }

    public class TestData
    {
        public string Text { get; set; }
        public int Number { get; set; }
    }

    public class TestDataViewModel : INotifyPropertyChanged
    {
        public TestDataViewModel()
        {
            /* TODO ERROR: Skipped EndRegionDirectiveTrivia */
            this.PropertyChanged += TestDataViewModel_PropertyChanged;
            _data = new TestData() { Text = string.Empty, Number = 0 };
        }

        private TestData _data;

        public string Text
        {
            get
            {
                return Data.Text;
            }

            set
            {
                if (CultureInfo.CurrentCulture.CompareInfo.Compare(Data.Text ?? "", value ?? "", CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0)
                {
                    return;
                }

                Data.Text = value;
                RaisePropertyChanged("Text");
            }
        }

        public int Number
        {
            get
            {
                return Data.Number;
            }

            set
            {
                if (Data.Number == value)
                {
                    return;
                }

                Data.Number = value;
                RaisePropertyChanged("Number");
            }
        }

        protected TestData Data
        {
            get
            {
                return _data;
            }
        }
        /* TODO ERROR: Skipped RegionDirectiveTrivia */
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void TestDataViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }

    public class DataSource
    {
        private ObservableCollection<TestDataViewModel> source;

        public DataSource()
        {
            source = CreateDataSource();
        }

        protected ObservableCollection<TestDataViewModel> CreateDataSource()
        {
            var res = new ObservableCollection<TestDataViewModel>();
            res.Add(new TestDataViewModel() { Text = "Breast Invasive", Number = 189 });
            res.Add(new TestDataViewModel() { Text = "Breast DCIS", Number = 211 });
            res.Add(new TestDataViewModel() { Text = "Breast Biomarker", Number = 169 });
            res.Add(new TestDataViewModel() { Text = "Colon and Rectum Resection", Number = 126 });
            res.Add(new TestDataViewModel() { Text = "Colon and Rectum Polypectomy", Number = 127 });
            res.Add(new TestDataViewModel() { Text = "Colon and Rectum Biomarker", Number = 228 });
            res.Add(new TestDataViewModel() { Text = "Colon and Rectum NET", Number = 196 });
            // res.Add(New TestDataViewModel() With {.Text = "Row7", .Number = 7})
            // res.Add(New TestDataViewModel() With {.Text = "Row8", .Number = 8})
            // res.Add(New TestDataViewModel() With {.Text = "Row9", .Number = 9})
            return res;
        }

        public ObservableCollection<TestDataViewModel> Data
        {
            get
            {
                return source;
            }
        }
    }
}
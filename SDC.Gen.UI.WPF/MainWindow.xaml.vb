Imports System
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Grid

Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.IO
'Imports System.Xml
Imports System.Xml
'Imports TE


Partial Public Class MainWindow
    Inherits DXWindow
    'Implements IDisposable

    Private Property BrowserPath As String
    Private TEinterop As TE.TEinterop
    Private Property FilePath As String


    Public Sub New()

        InitializeComponent()
        DataContext = New DataSource()
        TEinterop = New TE.TEinterop(My.Settings.TEpath)


        'If filePath = "" Then
        '    filePath = My.Settings.FilePath  'filePath, when set here,  will override settings files from other components
        '    If Not My.Computer.FileSystem.DirectoryExists(filePath) Then

        '        Try
        '            My.Computer.FileSystem.CreateDirectory(filePath)
        '        Catch ex As Exception
        '            MsgBox("Unable to create the file path: " + FilePath & vbCrLf & ex.Message + vbCrLf + ex.Data.ToString(), MsgBoxStyle.Exclamation, "SDC Folder was not created")
        '        End Try
        '    End If
        'End If
    End Sub

    Private Sub btnGenAll_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btnGenAllChecked.Click
        BrowserPath = txtBrowserPath.Text.Trim
        'FilePath = txtFilePath.Text.Trim
        Dim cur = Application.Current.MainWindow.Cursor
        Dim Gen As New SDC.Gen.API.GenXDT(Environment.ExpandEnvironmentVariables((txtFilePath.Text.Trim)), My.Settings.XslFileName)
        FilePath = Gen.XMLfilePath  ' if the UI-supplied txtFilePath does not exist, Gen will try to create a default one.  This reassigns Filepath to the default
        txtFilePath.Text = FilePath ' makes the same change in the UI

        If FileIO.FileSystem.DirectoryExists(FilePath) Then
            Dim templatesMap As New Dictionary(Of String, String)

            Application.Current.MainWindow.Cursor = Cursors.Wait


            For Each node In Me.TreeListView1.Nodes.Where(Function(n) CBool(n.IsChecked))

                Dim dataRowView = TryCast(node.Content, System.Data.DataRowView)
                Debug.Print(dataRowView(0).ToString & vbCrLf & dataRowView(1).ToString() & vbCrLf & dataRowView(2).ToString())

                templatesMap.Add(dataRowView(0).ToString, dataRowView(2).ToString) 'cols 0 & 3
            Next


            Gen.MakeXDTsFromTemplateMap(templatesMap, FilePath, CBool(chkCreateHTML.IsChecked))
        Else
            Beep()
            MsgBox("The file path for saving SDC XML files does not exist at: " & FilePath, MsgBoxStyle.Exclamation, "Folder does not exist")
        End If

        Application.Current.MainWindow.Cursor = cur

    End Sub


    Private Sub btnGenerate_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btnGenerate.Click
        Dim key As String = txtID.Text.Trim
        Dim fileName = txtFileName.Text.Trim
        Dim loadBrowser = CBool(chkShowBrowser.EditValue)
        Dim createHTML = CBool(chkCreateHTML.EditValue)
        BrowserPath = txtBrowserPath.Text.Trim
        'FilePath = txtFilePath.Text.Trim
        Dim ns As String = txtNamespace.Text.Trim

        Dim Gen As New SDC.Gen.API.GenXDT(Environment.ExpandEnvironmentVariables(txtFilePath.Text.Trim), My.Settings.XslFileName)
        FilePath = Gen.XMLfilePath  ' if the UI-supplied txtFilePath does not exist, Gen will try to create a default one.  This reassigns Filepath to the default
        txtFilePath.Text = FilePath ' makes the same change in the UI

        If FileIO.FileSystem.DirectoryExists(FilePath) Then
            If key > "" AndAlso ns > "" Then
                key = (String.Format("{0}.{1}", key, ns))
                'TODO: Need to cache this generator, since it takes time to create it new each time.

                Gen.MakeOneXDT(key, FilePath, loadBrowser, BrowserPath, createHTML)   ', fileName
            Else
                Beep()
                MsgBox("You must enter valid values for template Key (ID), Namespace, and Filepath", MsgBoxStyle.Exclamation, "Invalid Entry")
            End If
        Else
            Beep()
            MsgBox("The file path for saving SDC XML files does not exist: " & FilePath, MsgBoxStyle.Exclamation, "Folder does not exist")
        End If

    End Sub


    Public Shared Function Nstr(InObj As Object, Optional strDefault As String = "") As Object
        If InObj Is Nothing Then Return strDefault 'don't allow an unititialized object to be returned
        Return InObj
    End Function
    Public Shared Function N0(InObj As Object) As Object
        If InObj Is Nothing Then Return 0
        Return InObj
    End Function

    Private Sub DXWindow_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

    End Sub

    Private Sub cbCheckAll_Click(sender As Object, e As RoutedEventArgs) Handles cbCheckAll.Click
        TreeListView1.CheckAllNodes()

    End Sub

    Private Sub cbUncheckAll_Click(sender As Object, e As RoutedEventArgs) Handles cbUncheckAll.Click
        TreeListView1.UncheckAllNodes()
    End Sub

    Private Sub txtID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtID.KeyDown


        If e.Key = Input.Key.Enter Then
            btnGenerate_Click(sender, e)
            txtID.SelectAll()
        End If
    End Sub

    Private Sub gridControl1_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles gridControl1.MouseDoubleClick
        Dim col = gridControl1.CurrentColumn
        Dim cellText As String
        'Dim row = TryCast(gridControl1.GetFocusedRow, System.Data.DataRowView)

        If col.VisibleIndex = 0 Then
            gridControl1.Cursor = Cursors.Wait
            cellText = gridControl1.GetFocusedRowCellDisplayText(gridControl1.Columns(0)).ToString

            Dim key = Split(cellText, ".", 2)
            txtID.Text = key(0)
            'txtID.UpdateLayout()
            txtNamespace.Text = key(1)

            'Conside inmplementing a DoEvents on main UI thread here, to update the UI
            'https://www.devexpress.com/Support/Center/Question/Details/Q322288/gridcontrol-how-to-force-cell-errors-redraw-idxdataerrorinfo
            'https://stackoverflow.com/questions/4502037/where-is-the-application-doevents-in-wpf
            'http://geekswithblogs.net/NewThingsILearned/archive/2008/08/25/refresh--update-wpf-controls.aspx
            'https://www.meziantou.net/2011/06/22/refresh-a-wpf-control

            btnGenerate_Click(sender, e)
            gridControl1.Cursor = Cursors.Arrow
        ElseIf col.VisibleIndex = 2 Then
            cellText = gridControl1.GetFocusedRowCellDisplayText(gridControl1.Columns(0)).ToString
            'Dim TE = New TE.TEinterop()

            TEinterop.LookupItemByCKey(cellText, "0")
        End If


    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        TEinterop.Dispose()
    End Sub

    Private Sub gridControl1_GotFocus(sender As Object, e As RoutedEventArgs) Handles gridControl1.GotFocus
        Dim cellText = gridControl1.GetFocusedRowCellDisplayText(gridControl1.Columns(0)).ToString
        Dim CTVkey = Split(cellText, ".", 2)
        txtID.Text = CTVkey(0)
        Debug.WriteLine(sender.ToString, e.ToString)

    End Sub

    Private Sub btnLoadTE_Click(sender As Object, e As RoutedEventArgs) Handles gridControl1.MouseDown
        Try
            Dim btn As Button = TryCast(sender, Button)
            If btn IsNot Nothing Then
                Dim cellText = gridControl1.GetFocusedRowCellDisplayText(gridControl1.Columns(0)).ToString
                Dim CTVkey = Split(cellText, ".", 2)
                gridControl1.Cursor = Cursors.Wait
                'txtID.Text = CTVkey(0)

                If btn.Name = "btnKey" Then 'Geneate XML/HTML
                    'btn.Content = "Gen: " + cellText
                    'MsgBox(cellText)
                    btnGenerate_Click(sender, e)
                End If
                If btn.Name = "btnLoadTE" Then 'Load TE
                    TEinterop.LookupItemByCKey(cellText, "0")
                End If


            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
        Finally
            gridControl1.Cursor = Cursors.Arrow
        End Try

    End Sub

    Private Sub gridControl1_AutoGeneratingColumn(sender As Object, e As AutoGeneratingColumnEventArgs)

    End Sub

    Private Sub gridControl1_AutoGeneratedColumns(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub gridControl1_Loaded(sender As Object, e As RoutedEventArgs) Handles gridControl1.Loaded
        Return
        Dim grid = TryCast(sender, GridControl)
        If grid IsNot Nothing Then
            For i = 0 To gridControl1.VisibleItems.Count - 1
                Try
                    Dim row = grid.GetRowHandleByListIndex(i)
                    Dim frmElem = grid.View.GetRowElementByRowHandle(row)
                    Dim btn = TryCast(
                    DevExpress.Xpf.Core.Native.LayoutHelper.FindElementByName(frmElem, "btnKey"), Button)
                    Dim col = grid.Columns(1)
                    Dim cellVal = grid.GetCellValue(i, col)

                    If btn IsNot Nothing Then
                        btn.Content = "Gen: " + cellVal.ToString
                        btn.ToolTip = btn.Content
                    End If
                    'btn.UpdateLayout()
                    'Dim unused = btn.CacheMode
                    'MainWindow.Owner.UpdateLayout()
                    'btn.Visibility =
                Catch ex As Exception
                    MsgBox("There are more VisibleItems than Rows in the control", MsgBoxStyle.Exclamation, "Error in loading rows")
                End Try

            Next


        End If
    End Sub

    Public Shared Async Function GetChildrenAsync(G As GridControl, row As Integer, column As Integer) As System.Threading.Tasks.Task(Of Object)
        Dim rList As IList = Await (G.GetRowsAsync(0, G.VisibleRowCount))
        For Each r In rList
            Dim rc = TryCast(r, RowControl)
            Dim btn As Button = TryCast(rc.FindName("btnKey"), Button)
            'btn.Content = rc.
        Next

    End Function
    Public Sub DumpLogicalTree(ByVal parent As Object, ByVal level As Integer)
        Dim typeName As String = parent.[GetType]().Name
        Dim name As String = Nothing
        Dim doParent As DependencyObject = TryCast(parent, DependencyObject)

        If doParent IsNot Nothing Then
            name = CStr((If(doParent.GetValue(FrameworkElement.NameProperty), "")))
        Else
            name = parent.ToString()
        End If

        Trace.WriteLine(String.Format("{0}: {1}", typeName, name))
        If doParent Is Nothing Then Return

        For Each child As Object In LogicalTreeHelper.GetChildren(doParent)
            DumpLogicalTree(child, level + 1)
        Next
    End Sub
    Public Sub DumpVisualTree(ByVal parent As DependencyObject, ByVal level As Integer)
        Dim typeName As String = parent.[GetType]().Name
        Dim name As String = CStr((If(parent.GetValue(FrameworkElement.NameProperty), "")))
        Trace.WriteLine(String.Format("{0}: {1}", typeName, name))
        If parent Is Nothing Then Return

        For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(parent) - 1
            Dim child As DependencyObject = VisualTreeHelper.GetChild(parent, i)
            DumpVisualTree(child, level + 1)
        Next
    End Sub
    Public Shared Iterator Function FindVisualChildren(Of T As DependencyObject)(ByVal depObj As DependencyObject) As IEnumerable(Of T)
        If depObj IsNot Nothing Then

            For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(depObj) - 1
                Dim child As DependencyObject = VisualTreeHelper.GetChild(depObj, i)

                If child IsNot Nothing AndAlso TypeOf child Is T Then
                    Yield CType(child, T)
                    'Trace.WriteLine(String.Format("{0}: {1}", typeName, name))
                End If

                For Each childOfChild As T In FindVisualChildren(Of T)(child)
                    Yield childOfChild
                Next
            Next
        End If
    End Function
    Private Function FindVisualChild(Of childItem As DependencyObject)(ByVal obj As DependencyObject) As childItem
        For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(obj) - 1
            Dim child As DependencyObject = VisualTreeHelper.GetChild(obj, i)

            If child IsNot Nothing AndAlso TypeOf child Is childItem Then
                Return CType(child, childItem)
            Else
                Dim childOfChild As childItem = FindVisualChild(Of childItem)(child)
                If childOfChild IsNot Nothing Then Return childOfChild
            End If
        Next

        Return Nothing
    End Function
End Class



Public Class TestData
    Public Property Text() As String
    Public Property Number() As Integer
End Class

Public Class TestDataViewModel
    Implements INotifyPropertyChanged
    Private _data As TestData
    Public Sub New()
        _data = New TestData() With {.Text = String.Empty, .Number = 0}
    End Sub
    Public Property Text() As String
        Get
            Return Data.Text
        End Get
        Set(ByVal value As String)
            If Data.Text = value Then
                Return
            End If
            Data.Text = value
            RaisePropertyChanged("Text")
        End Set
    End Property
    Public Property Number() As Integer
        Get
            Return Data.Number
        End Get
        Set(ByVal value As Integer)
            If Data.Number = value Then
                Return
            End If
            Data.Number = value
            RaisePropertyChanged("Number")
        End Set
    End Property
    Protected ReadOnly Property Data() As TestData
        Get
            Return _data
        End Get
    End Property
#Region "INotifyPropertyChanged"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Protected Overridable Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub
    Protected Sub RaisePropertyChanged(ByVal propertyName As String)
        OnPropertyChanged(New PropertyChangedEventArgs(propertyName))
    End Sub
#End Region

    Private Sub TestDataViewModel_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles Me.PropertyChanged

    End Sub
End Class

Public Class DataSource
    Private source As ObservableCollection(Of TestDataViewModel)
    Public Sub New()
        source = CreateDataSource()
    End Sub

    Protected Function CreateDataSource() As ObservableCollection(Of TestDataViewModel)
        Dim res As New ObservableCollection(Of TestDataViewModel)()
        res.Add(New TestDataViewModel() With {.Text = "Breast Invasive", .Number = 189})
        res.Add(New TestDataViewModel() With {.Text = "Breast DCIS", .Number = 211})
        res.Add(New TestDataViewModel() With {.Text = "Breast Biomarker", .Number = 169})
        res.Add(New TestDataViewModel() With {.Text = "Colon and Rectum Resection", .Number = 126})
        res.Add(New TestDataViewModel() With {.Text = "Colon and Rectum Polypectomy", .Number = 127})
        res.Add(New TestDataViewModel() With {.Text = "Colon and Rectum Biomarker", .Number = 228})
        res.Add(New TestDataViewModel() With {.Text = "Colon and Rectum NET", .Number = 196})
        'res.Add(New TestDataViewModel() With {.Text = "Row7", .Number = 7})
        'res.Add(New TestDataViewModel() With {.Text = "Row8", .Number = 8})
        'res.Add(New TestDataViewModel() With {.Text = "Row9", .Number = 9})
        Return res
    End Function
    Public ReadOnly Property Data() As ObservableCollection(Of TestDataViewModel)
        Get
            Return source
        End Get
    End Property




End Class



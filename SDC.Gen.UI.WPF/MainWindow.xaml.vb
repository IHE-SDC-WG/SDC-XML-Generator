Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.ObjectModel

Imports System.ComponentModel
Imports System.Linq
'Imports TE


Partial Public Class MainWindow
    Inherits DXWindow
    'Implements IDisposable

    Private Property BrowserPath As String
    Private Property GridList As IList
    Private Property ConString As String
    Private Property SdcXmlGenerator As SDC.Gen.API.GenXDT
    'Private TEinterop As TE.TEinterop
    Private Property FilePath As String

    Public Sub New()
        InitializeComponent()
        DataContext = New DataSource()
        'TEinterop = New TE.TEinterop(My.Settings.TEpath)




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

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        FillData()
        SdcXmlGenerator = Nothing 'clear all Gen variables
    End Sub
    Private Sub FillData()
        My.Settings.Reload()
        ConString = My.Settings.EF_Con
        Dim sspEntities = New SSPEntities()
        'Dim user = System.Environment.UserName
        'ConString = ConString.Replace("*****", System.Environment.UserName + "@cap.org")
        sspEntities.Database.Connection.ConnectionString = ConString

        'We can display the "Initial Catalog=" and "Data Source=" from the ConString in the UI/XAML
        Dim conBuilder = New System.Data.Common.DbConnectionStringBuilder()
        conBuilder.ConnectionString = ConString

        Dim server As String
        Dim db As String

        Dim temp As String = ""
        temp = conBuilder("Data Source").ToString()
        If String.IsNullOrWhiteSpace(temp) Then server = "?" Else server = temp
        temp = conBuilder("Initial Catalog").ToString()
        If String.IsNullOrWhiteSpace(temp) Then db = "?" Else db = temp

        button.Content = "Connect Database" & "  (Current Server = " & server & ", " & "Database = " & db & ")"

        Dim query =
            From TV In sspEntities.TemplateVersions
            Join PT In sspEntities.ProtocolTemplates On PT.ProtocolTemplateKey Equals TV.ProtocolTemplateKey
            Join R In sspEntities.ListReleaseStates On R.ReleaseStateKey Equals TV.ReleaseStateKey
            Where TV.Active = True AndAlso PT.Active = True AndAlso
            (
            R.ReleaseVersionSuffix = "REL" OrElse
            R.ReleaseVersionSuffix.StartsWith("CPT") OrElse
            R.ReleaseVersionSuffix.StartsWith("RC") OrElse
            R.ReleaseVersionSuffix.StartsWith("TEST") OrElse
            PT.DraftTemplateVerKey = TV.TemplateVersionKey
            )
            Order By R.ReleaseVersionSuffix, PT.Lineage
            Select PT.Lineage, TV.TemplateVersionKey, TV.ReleaseKey, TV.Version, R.ReleaseVersionSuffix, TV.ProtocolTemplateKey, TV.ProtocolVersionKey

        'ConString = sspEntities.Database.Connection.ConnectionString

        Try
            GridList = query.ToList()
            gridControl1.ItemsSource = GridList
        Catch ex As Exception
            MessageBox.Show("Dataset could not be loaded.  Check your connection and permissions" & vbCrLf &
                            "Error: " & vbCrLf & ex.Message & vbCrLf & ex.InnerException.Message)
        End Try


    End Sub


    Private Sub btnGenAll_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btnGenAllChecked.Click
        'BrowserPath = txtBrowserPath.Text.Trim
        Dim cur = Application.Current.MainWindow.Cursor
        'Dim Gen As New SDC.Gen.API.GenXDT(Environment.ExpandEnvironmentVariables((txtFilePath.Text.Trim)), My.Settings.XslFileName, ConString, BrowserPath)
        'FilePath = SdcXmlGenerator.XMLfilePath  ' if the UI-supplied txtFilePath does not exist, Gen will try to create a default one.  This reassigns Filepath to the default
        'txtFilePath.Text = FilePath ' makes the same change in the UI

        'Dim key As String = Nothing
        Dim loadBrowser As Boolean = Nothing
        Dim createHTML As Boolean = Nothing
        Dim nmspace As String = Nothing
        Init(loadBrowser, createHTML, nmspace)


        If FileIO.FileSystem.DirectoryExists(FilePath) Then
            Dim templatesMap As New Dictionary(Of String, String)

            Application.Current.MainWindow.Cursor = Cursors.Wait
            Dim colTVkey = gridControl1.Columns(0)
            Dim colLineage = gridControl1.Columns(2)

            For Each node In Me.TreeListView1.Nodes.Where(Function(n) CBool(n.IsChecked))
                templatesMap.Add(TreeListView1.GetNodeValue(node, colTVkey).ToString, 'col 0
                                 TreeListView1.GetNodeValue(node, colLineage          'col 3
                                 ).ToString) 'cols 0 & 3
            Next

            SdcXmlGenerator.MakeXDTsFromTemplateMap(
                templatesMap, FilePath, CBool(chkCreateHTML.IsChecked))
        Else
            Beep()
            MsgBox("The file path for saving SDC XML files does not exist at: " & FilePath, MsgBoxStyle.Exclamation, "Folder does not exist")
        End If

        Application.Current.MainWindow.Cursor = cur

    End Sub


    Private Sub btnGenerate_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btnGenerate.Click

        Dim key As String = Me.txtID.Text.Trim()
        Dim loadBrowser As Boolean = Nothing
        Dim createHTML As Boolean = Nothing
        Dim nmspace As String = Nothing
        Dim customFileName As String = txtFileName.Text.Trim()
        Init(loadBrowser, createHTML, nmspace)

        If FileIO.FileSystem.DirectoryExists(FilePath) Then
            If key > "" AndAlso nmspace > "" Then
                SdcXmlGenerator.MakeOneXDT(key, FilePath, loadBrowser, BrowserPath, createHTML, customFileName)   ', fileName
            Else
                Beep()
                MsgBox("You must enter valid values for template Key (ID), Namespace, and Filepath", MsgBoxStyle.Exclamation, "Invalid Entry")
            End If
        Else
            Beep()
            MsgBox("The file path for saving SDC XML files does not exist: " & FilePath, MsgBoxStyle.Exclamation, "Folder does not exist")
        End If

    End Sub

    Private Sub Init(ByRef loadBrowser As Boolean, ByRef createHTML As Boolean, ByRef ns As String)
        loadBrowser = CBool(chkShowBrowser.EditValue)
        createHTML = CBool(chkCreateHTML.EditValue)
        ns = txtNamespace.Text.Trim

        If Not String.IsNullOrWhiteSpace(txtFilePath.Text.Trim) Then
            FilePath = Environment.ExpandEnvironmentVariables(txtFilePath.Text.Trim)
        ElseIf Not String.IsNullOrWhiteSpace(My.Settings.FilePath) Then
            FilePath = Environment.ExpandEnvironmentVariables(My.Settings.FilePath.Trim())
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtBrowserPath.Text.Trim) Then
            BrowserPath = Environment.ExpandEnvironmentVariables(txtBrowserPath.Text.Trim)
        ElseIf Not String.IsNullOrWhiteSpace(My.Settings.BrowserPath64) Then
            BrowserPath = Environment.ExpandEnvironmentVariables(My.Settings.BrowserPath64.Trim())
        ElseIf Not String.IsNullOrWhiteSpace(My.Settings.BrowserPath32) Then
            BrowserPath = Environment.ExpandEnvironmentVariables(My.Settings.BrowserPath32.Trim())
        End If

        If IsNothing(SdcXmlGenerator) Then SdcXmlGenerator = New SDC.Gen.API.GenXDT(FilePath, My.Settings.XslFileName, ConString, BrowserPath)
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

    'Private Sub gridControl1_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles gridControl1.MouseDoubleClick
    '    Dim col = gridControl1.CurrentColumn
    '    Dim cellText As String
    '    'Dim row = TryCast(gridControl1.GetFocusedRow, System.Data.DataRowView)

    '    If col.VisibleIndex = 0 Then
    '        gridControl1.Cursor = Cursors.Wait
    '        cellText = gridControl1.GetFocusedRowCellDisplayText(gridControl1.Columns(0)).ToString

    '        Dim key = Split(cellText, ".", 2)
    '        txtID.Text = key(0)
    '        'txtID.UpdateLayout()
    '        txtNamespace.Text = key(1)

    '        'Consider inmplementing a DoEvents on main UI thread here, to update the UI
    '        'https://www.devexpress.com/Support/Center/Question/Details/Q322288/gridcontrol-how-to-force-cell-errors-redraw-idxdataerrorinfo
    '        'https://stackoverflow.com/questions/4502037/where-is-the-application-doevents-in-wpf
    '        'http://geekswithblogs.net/NewThingsILearned/archive/2008/08/25/refresh--update-wpf-controls.aspx
    '        'https://www.meziantou.net/2011/06/22/refresh-a-wpf-control

    '        btnGenerate_Click(sender, e)
    '        gridControl1.Cursor = Cursors.Arrow
    '    ElseIf col.VisibleIndex = 2 Then
    '        cellText = gridControl1.GetFocusedRowCellDisplayText(gridControl1.Columns(0)).ToString
    '        'Dim TE = New TE.TEinterop()

    '        TEinterop.LookupItemByCKey(cellText, "0")
    '    End If


    'End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        'TEinterop.Dispose()
    End Sub

    Private Sub gridControl1_GotFocus(sender As Object, e As RoutedEventArgs) Handles gridControl1.GotFocus
        Dim cellText = gridControl1.GetFocusedRowCellDisplayText(gridControl1.Columns(0)).ToString
        Dim ID = Split(cellText, ".", 2)
        txtID.Text = ID(0)
        Debug.WriteLine(sender.ToString, e.ToString)

    End Sub

    Private Sub btnKey_Click(sender As Object, e As RoutedEventArgs) Handles gridControl1.MouseDown
        Try
            Dim btn As Button = TryCast(sender, Button)

            If btn IsNot Nothing Then
                Dim cellText = gridControl1.GetFocusedRowCellDisplayText(gridControl1.Columns(0)).ToString
                gridControl1.Cursor = Cursors.Wait

                If btn.Name = "btnKey" Then 'Geneate XML/HTML
                    'btn.Content = "Gen: " + cellText
                    'MsgBox(cellText)
                    btnGenerate_Click(sender, e)
                End If


            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
        Finally
            gridControl1.Cursor = Cursors.Arrow
        End Try

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



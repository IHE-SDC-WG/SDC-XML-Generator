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

    Private BrowserPath As String
    Private TEinterop As TE.TEinterop


    Public Sub New()
        BrowserPath = ""
        InitializeComponent()
        DataContext = New DataSource()
        TEinterop = New TE.TEinterop(My.Settings.TEpath)
    End Sub

    Private Sub btnGenAll_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btnGenAllChecked.Click
        Dim filePath = txtFilePath.Text.Trim
        If filePath = "" Then
            filePath = My.Settings.FilePath  'filePath, when set here,  will override settings files from other components
        End If

        If FileIO.FileSystem.DirectoryExists(filePath) Then
            Dim templatesMap As New Dictionary(Of String, String)

            For Each node In Me.TreeListView1.Nodes.Where(Function(n) CBool(n.IsChecked))

                Dim dataRowView = TryCast(node.Content, System.Data.DataRowView)
                Debug.Print(dataRowView(0).ToString & vbCrLf & dataRowView(1).ToString() & vbCrLf & dataRowView(2).ToString())

                templatesMap.Add(dataRowView(0).ToString, dataRowView(2).ToString) 'cols 0 & 3
            Next

            Dim Gen As New XML_Gen_API.GenXDT(filePath, My.Settings.XslFileName)
            Gen.MakeXDTsFromTemplateMap(templatesMap, filePath, CBool(chkCreateHTML.IsChecked))
        Else
            Beep()
            MsgBox("You must select templates and provide a valid file path to save teh XML file(s)", MsgBoxStyle.Exclamation, "Invalid Entry")
        End If

    End Sub


    Private Sub btnGenerate_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btnGenerate.Click
        Dim key As String = txtID.Text.Trim
        Dim fileName = txtFileName.Text.Trim
        Dim showBrowser = CBool(chkShowBrowser.EditValue)
        Dim outputHTML = CBool(chkCreateHTML.EditValue)
        Dim browserPath = txtBrowserPath.Text.Trim
        Dim ns As String = txtNamespace.Text.Trim

        Dim filePath = txtFilePath.Text.Trim
        If filePath = "" Then
            filePath = My.Settings.FilePath  'filePath, when set here,  will override settings files from other components
        End If

        If key > "" AndAlso ns > "" AndAlso
            FileIO.FileSystem.DirectoryExists(filePath) Then

            key = (String.Format("{0}.{1}", key, ns))
            'TODO: Need to cache this generator, since it takes time to create it new each time.
            Dim Gen As New XML_Gen_API.GenXDT(filePath, My.Settings.XslFileName)
            Gen.MakeOneXDT(key, fileName, filePath, showBrowser, browserPath, outputHTML)
        Else
            Beep()
            MsgBox("You must enter valid values for template Key (ID), Namespace, and Filepath", MsgBoxStyle.Exclamation, "Invalid Entry")
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

'Imports Spring.Context
'Imports Spring.Context.Support



<ComClass(ComClass1.ClassId, ComClass1.InterfaceId, ComClass1.EventsId)> _
Public Class ComClass1

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "13637136-620d-4598-9a85-3516e7dfc971"
    Public Const InterfaceId As String = "a536b431-de65-48c2-9344-684ce6cb8e4d"
    Public Const EventsId As String = "3dec6c2f-7c76-4522-917d-e73d5aa12310"
#End Region
    Public Event XDT_Ready(XDT As String)
    Public Event HTML_Ready(HTML As String)
    'Define the event
    'Public Event XDT_Ready As XDTchangeDelegate
    'Public Event HTML_Ready As HTMLchangeDelegate

    Private _browserPath As String
    Public _CreateHTML As Boolean = True
    Public _DisplayBrowser As Boolean = True

    Private _ErrorMsg As String
    Private _connectionString As String
    Private _FileSavePath As String
    Private _Filename As String
    Private _cTV_Ckey As Decimal
    '

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public Event E1(s As String)

    Function T1() As Integer
        Return 10
    End Function

    Sub T2()
        MsgBox("Test T2 " & My.Computer.FileSystem.CurrentDirectory)
        'Dim context As New Context
        'context.o()
        'Spring.Context.Support.ContextRegistry.RegisterContext(context)
        Dim ctx As IApplicationContext = ContextRegistry.GetContext()
        MsgBox("Test after ctx")
        Dim BTG As Capx.Apps.ChecklistTemplateGenerator.BatchTemplateGenerator = ctx.GetObject("batchTemplateGenerator")
        MsgBox("Test after BTG")
    End Sub

End Class


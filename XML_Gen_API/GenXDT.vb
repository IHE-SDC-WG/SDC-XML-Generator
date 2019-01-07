'Imports System.Runtime.InteropServices
Imports System.Xml.Xsl
Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Diagnostics
'Imports SDC
'Imports SDC.DAL.DataSets
Imports System.Collections.Generic
Imports System.Data


Public Class GenXDT

#Region "Private Fields and Events  "
    Public Event XDT_Ready(XDT As String)
    Public Event HTML_Ready(HTML As String)
    'Define the event
    'Public Event XDT_Ready As XDTchangeDelegate
    'Public Event HTML_Ready As HTMLchangeDelegate

    Private Property FileName As String
    Private Property FilePath As String
    Private Property HTMLfileName As String
    Private Property XMLfileName As String
    Private Property FullXMLfilePath As String
    Private Property BrowserPath As String
    'Private Property TemplateDataMapper As Capx.Apps.ChecklistTemplateGenerator.SrTemplateDataMapper

    'Private Property Transform() As XslCompiledTransform()
    Private Property Transform As XslCompiledTransform
    Private Property MaxXslIndex() As Integer = 4
    Private Property RI_Folder() As String()
    Private Property XslFileName() As String()
    Public Property DefaultXslFileName As String
#End Region

#Region "ctor"
    Public Sub New(RootFilePath As String, Optional defaultFileNameXSL As String = "")
        'TemplateDataMapper = New Capx.Apps.ChecklistTemplateGenerator.SrTemplateDataMapper
        If RootFilePath = "" Then
            RootFilePath = My.MySettings.Default.FilePath
            If RootFilePath = "" Then RootFilePath = "C:\tmp\"
            If RootFilePath = "" OrElse Not FileIO.FileSystem.DirectoryExists(RootFilePath) Then
                Beep()
                MsgBox("The File Path is not valid. Enter a valid File Path.", MsgBoxStyle.OkOnly, "Error!")
            End If
        End If
        Me.FilePath = RootFilePath
        DefaultXslFileName = defaultFileNameXSL


        ''TODO: This initialization should be done in an XML settings file
        ''!Create array of Reference Implementation (RI) folders
        'ReDim RI_Folder(MaxXslIndex)
        'RI_Folder(0) = "eCC1-single-checklist"
        'RI_Folder(1) = "eCC2-multiple-checklists"
        'RI_Folder(2) = "eCC3-advanced-functions"
        'RI_Folder(3) = "eCC4-HTML-metadata"
        'RI_Folder(4) = "eCC5-data-entry"
        ''TODO: check for existance of all these folders, and create them if they don't exist


        ''TODO: This initialization should be done in an XML settings file
        'ReDim XslFileName(MaxXslIndex)
        'XslFileName(0) = DefaultXslFileName
        'XslFileName(1) = DefaultXslFileName
        'XslFileName(2) = DefaultXslFileName
        'XslFileName(3) = DefaultXslFileName
        'XslFileName(4) = DefaultXslFileName
        ''TODO: check for existance of all these xslt's in the appropriate folders, and skip or report error if they don't exist

        Dim settings As New XsltSettings 'required to allow use of Document() function in XSL file
        settings.EnableDocumentFunction = True
        settings.EnableScript = False

        'ReDim Transform(MaxXslIndex) 'Create an array of compiled xsl transforms, one for each RI folder

        ''TODO: Error handling
        'Return

        'For i = 0 To MaxXslIndex
        '    Transform(i) = New XslCompiledTransform
        '    Dim xslFile = String.Format("{0}\{1}\{2}", RootFilePath, RI_Folder(i), XslFileName(i))
        '    Transform(i).Load(xslFile, settings, New Xml.XmlUrlResolver) 'Compile the XSLT only once for each batch of files to transform
        'Next
        'Dim xslFile = String.Format("{0}\{1}\{2}", RootFilePath, RI_Folder(i), XslFileName(i))

        Dim xslFile = String.Format("{0}\{1}", RootFilePath, DefaultXslFileName)
        If Transform Is Nothing Then  'TODO: for now we reuse a single xslt for all forms.  This may change...
            Transform = New XslCompiledTransform()
            Transform.Load(xslFile, settings, New Xml.XmlUrlResolver) 'Compile the XSLT only once for each batch of files to transform
        End If

    End Sub
#End Region
#Region "API Functions"
    Public Function MakeOneXDT(key As String,
                   Optional userFileName As String = "",
                   Optional path As String = "",
                   Optional loadBrowser As Boolean = True,
                   Optional webBrowserPath As String = "",
                   Optional outputHTML As Boolean = False) As String

        userFileName = userFileName.Trim
        FilePath = path
        BrowserPath = webBrowserPath

        Dim templateXML As String
        templateXML = MakeXDT(key)

        If Not String.IsNullOrWhiteSpace(templateXML) Then ' > "" Then
            If outputHTML Then MakeHTML()
            If loadBrowser Then ShowBrowser()
        Else
            MsgBox("The template with ID [ " & key & " ] was not found", MsgBoxStyle.Exclamation, "Template not found")
        End If

        Return templateXML
    End Function
    Public Sub MakeXDTsFromTemplateMap(
                           templatesMap As Dictionary(Of String, String),
                           Optional path As String = "",
                           Optional createHTML As Boolean = False)

        FilePath = path 'the place where we will store the XML / HTML files
        Dim templateXML As String
        For Each file In templatesMap
            templateXML = MakeXDT(file.Key)
            If String.IsNullOrWhiteSpace(templateXML) Then ' > "" Then
                If createHTML Then MakeHTML()
            Else
                MsgBox("The template with ID [ " & file.Key & " ] was not found", MsgBoxStyle.Exclamation, "Template not found")
            End If
        Next

    End Sub

#End Region

#Region "Private Helper Methods"
    Private Function MakeXDT(key As String, Optional userFileName As String = "") As String
        '!Create the XML:
        'TemplateDataMapper.XsltFileName = DefaultXslFileName

        Dim ser = New XmlSerializer(GetType(SDC.DAL.DataSets.FormDesignDataSets))
        Dim fdd = New SDC.DAL.DataSets.FormDesignDataSets()
        Dim stb As SDC.SDCTreeBuilderEcc =
            New SDC.SDCTreeBuilderEcc(key, fdd, "sdctemplate.xslt")

        Dim filename As String = stb.FormDesign.filename
        Dim formDesignXml As String = stb.FormDesign.Serialize()

        Dim orig As String = "<?xml version=""1.0"" encoding=""utf-8""?>"
        Dim fix As String = orig + vbCrLf + "<?xml-stylesheet type=""text/xsl"" href=""sdctemplate.xslt""?>"
        formDesignXml = formDesignXml.Replace(orig, fix)
        FullXMLfilePath = String.Format("{0}\{1}", FilePath, filename)
        System.IO.File.WriteAllText(FullXMLfilePath, formDesignXml, System.Text.Encoding.UTF8)


        'Dim templateXml As String = TemplateDataMapper.CreateOneTemplateByCkey(key, userFileName)
        'Dim templateXml As DataTable = (New SDC.DAL.DataSets.FormDesignDataSets()).dtGetFormDesign(CDec(key))
        'If formDesignXml = "" Then
        '    filename = ""
        '    userFileName = ""
        '    XMLfileName = ""
        '    HTMLfileName = ""
        '    FullXMLfilePath = ""
        'Else
        '    If Not String.IsNullOrWhiteSpace(userFileName) Then filename = userFileName
        '    If Not String.IsNullOrWhiteSpace(filename) Then XMLfileName = filename                  '& "_enh.xml" '"enh" means "enhanced eCC"
        '    HTMLfileName = FileName & ".html"       '& "_enh.html"
        '    FullXMLfilePath = String.Format("{0}\{1}", FilePath, XMLfileName)

        '    '!Save the XML file:
        '    System.IO.File.WriteAllText(FullXMLfilePath, formDesignXml, System.Text.Encoding.UTF8)
        '    'Debug.Assert(My.Computer.FileSystem.FileExists(fullXmlFilePath))

        '    '!Copy the xml files to all the Reference Implementation (RI) folders
        '    'For i = 0 To MaxXslIndex 'Copy xml files to RI folders
        '    '    Dim target = String.Format("{0}\{1}\{2}", FilePath, RI_Folder(i), XMLfileName)
        '    '    My.Computer.FileSystem.CopyFile(FullXMLfilePath, target, overwrite:=True)
        '    'Next
        'End If
        Return formDesignXml
    End Function

    Private Sub MakeHTML()

        'Try
        '    For i = 0 To Transform.GetUpperBound(0)
        '        Transform(i).Transform(
        '            String.Format("{0}\{1}\{2}", FilePath, RI_Folder(i), XMLfileName),
        '            String.Format("{0}\{1}\eCC{2}-HTML\{3}", FilePath, RI_Folder(i), i + 1, HTMLfileName))
        '    Next
        'Catch ex As Exception
        '    MsgBox(ex.Message.ToString & vbCrLf &
        '           DirectCast(IIf(ex.InnerException.ToString Is Nothing, "", ex.InnerException.ToString), String),
        '           Buttons:=MsgBoxStyle.Exclamation, Title:="Error in Xslt process")
        'End Try

        Transform.Transform(FilePath, HTMLfileName)

        'RaiseEvent HTML_Ready(Filename & ".html")
    End Sub

    Private Sub ShowBrowser()
        If BrowserPath = "" Then
            BrowserPath = "C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe"
            If Not My.Computer.FileSystem.FileExists(BrowserPath) Then
                BrowserPath = "C:\\Program Files\\Mozilla Firefox\\firefox.exe" 'For 32 bit systems
            End If
        End If

        If My.Computer.FileSystem.FileExists(BrowserPath) Then
            Process.Start(BrowserPath, FullXMLfilePath)
        Else
            MsgBox(String.Format("Browser filepath not found: {0}", BrowserPath.ToString), MsgBoxStyle.Critical, "File Not Found")
        End If
    End Sub

#End Region

End Class

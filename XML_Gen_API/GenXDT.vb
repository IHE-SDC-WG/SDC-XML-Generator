'Imports System.Runtime.InteropServices
Imports SDC.DAL.DataSets
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
    ReadOnly Property DatasetBuilder As FormDesignDataSets
    Private Property FileName As String
    Property XMLfilePath As String
    Private Property HTMLfilePath As String
    Private Property HTMLfileName As String
    Private Property XMLfileName As String
    Private Property FullXMLfilePath As String
    Private Property FullHTMLfilePath As String
    Property BrowserPath As String
    'Private Property TemplateDataMapper As Capx.Apps.ChecklistTemplateGenerator.SrTemplateDataMapper
    'Private Property Transform() As XslCompiledTransform()
    Private Property Transform As XslCompiledTransform
    ReadOnly Property MaxXslIndex() As Integer = 4
    ReadOnly Property RI_Folder() As String
    ReadOnly Property XslFileName() As String
    ReadOnly Property FullXSLfilePath As String
    ReadOnly Property ConString As String

    'Dim _defaultXslFileName As String
    'Property DefaultXslFileName As String
    '    Get
    '        Return _defaultXslFileName
    '    End Get
    '    Private Set
    '        _defaultXslFileName = Value
    '    End Set
    'End Property
#End Region

#Region "ctor"
    Public Sub New(RootFilePath As String, FileNameXSL As String, ConnectionString As String, WebBrowserPath As String)

        XslFileName = FileNameXSL
        DatasetBuilder = New FormDesignDataSets(ConnectionString)
        BrowserPath = WebBrowserPath

        'Test for existance of the RootFilePath
        If Not My.Computer.FileSystem.DirectoryExists(RootFilePath) Then
            Try
                My.Computer.FileSystem.CreateDirectory(RootFilePath)
                MsgBox($"An SDC folder was created for you here: {RootFilePath}", MsgBoxStyle.OkOnly)
            Catch ex As Exception
                Try
                    Dim defaultPath = Environment.ExpandEnvironmentVariables("%USERPROFILE%\Desktop\SDC Files")
                    My.Computer.FileSystem.CreateDirectory(defaultPath) 'create SDC folder in the desktop folder
                    Dim dir As DirectoryInfo
                    dir = My.Computer.FileSystem.GetDirectoryInfo(defaultPath)
                    RootFilePath = dir.FullName()
                    MsgBox(
$"The supplied SDC XML folder location could not be created, so a folder was created for you here: {RootFilePath}  - 
You will need to manually add the SDC xslt file to this location", MsgBoxStyle.OkOnly)
                Catch ex
                    MsgBox(
$"Error creating new folder for SDC XML files at: {RootFilePath}  - 
XML generation cannot continue" & vbCrLf & ex.Message, MsgBoxStyle.OkOnly, "Error!")
                    Return
                End Try

            End Try
        End If

        If Not My.Computer.FileSystem.DirectoryExists(RootFilePath + "\HTML") Then
            Try
                My.Computer.FileSystem.CreateDirectory(RootFilePath + "\HTML")
                MsgBox(
$"An SDC\HTML folder was created for you here: {RootFilePath + "\HTML"}  - 
You will need to manually add the .css file to this location", MsgBoxStyle.OkOnly)
            Catch ex As Exception
                MsgBox(
$"Error creating new folder for HTML files at {RootFilePath + "\HTML"};  - 
XML generation cannot continue" & vbCrLf & ex.Message, MsgBoxStyle.OkOnly, "Error!")
                Return
            End Try

        End If

        XMLfilePath = RootFilePath
        HTMLfilePath = RootFilePath + "\HTML"

#Region "junk"
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
#End Region

        Dim settings As New XsltSettings 'required to allow use of Document() function in XSL file

        Try
            settings.EnableDocumentFunction = True
            settings.EnableScript = False
        Catch ex As Exception
            MsgBox("Error setting XSLT compiler options." & vbCrLf & ex.Message, MsgBoxStyle.OkOnly, "Error!")
        End Try

#Region "junk"

        'ReDim Transform(MaxXslIndex) 'Create an array of compiled xsl transforms, one for each RI folder

        ''TODO: Error handling
        'Return

        'For i = 0 To MaxXslIndex
        '    Transform(i) = New XslCompiledTransform
        '    Dim xslFile = String.Format("{0}\{1}\{2}", RootFilePath, RI_Folder(i), XslFileName(i))
        '    Transform(i).Load(xslFile, settings, New Xml.XmlUrlResolver) 'Compile the XSLT only once for each batch of files to transform
        'Next
        'Dim xslFile = String.Format("{0}\{1}\{2}", RootFilePath, RI_Folder(i), XslFileName(i))
#End Region

        FullXSLfilePath = String.Format("{0}\{1}", XMLfilePath, XslFileName)
        If Not My.Computer.FileSystem.FileExists(FullXSLfilePath) Then

            MsgBox($"The SDC XSLT file does not exist at {FullXSLfilePath}.
HTML files cannot be generated until the XSLT file is manually added at this location.", MsgBoxStyle.OkOnly, "Error!")
            Return
        End If

        Try
            If Transform Is Nothing Then  'TODO: for now we reuse a single xslt for all forms.  This may change...
                Transform = New XslCompiledTransform()
                Transform.Load(FullXSLfilePath, settings, New Xml.XmlUrlResolver) 'Compile the XSLT only once for each batch of files to transform
            End If

        Catch ex As Exception
            MsgBox($"Error compiling XSLT file at: {FullXSLfilePath}" & vbCrLf & ex.Message, MsgBoxStyle.OkOnly, "Error!")
        End Try

    End Sub
#End Region
#Region "API Functions"
    Public Function MakeOneXDT(key As String,
                   Optional path As String = "",
                   Optional loadBrowser As Boolean = True,
                   Optional webBrowserPath As String = "",
                   Optional outputHTML As Boolean = False,
                   Optional customFileName As String = "") As String

        If webBrowserPath.Length > 0 Then BrowserPath = webBrowserPath
        If path.Length > 0 Then
            XMLfilePath = path
            HTMLfilePath = XMLfilePath + "\HTML"
        End If

        Dim templateXML As String
        templateXML = MakeXDT(key, customFileName)

        If Not String.IsNullOrWhiteSpace(templateXML) Then ' > "" Then
            If outputHTML Then
                If My.Computer.FileSystem.FileExists(FullXSLfilePath) Then
                    Try
                        MakeHTML()
                        If loadBrowser Then ShowBrowser()
                    Catch ex As Exception
                        MsgBox($"Error creating HTML for template with ID: {key}, and filename: {FullXMLfilePath} " + vbCrLf + ex.Message,
                               MsgBoxStyle.Exclamation, "Template not found")
                    End Try


                Else 'MsgBox($"An XSLT file for generating SDC HTML was not found at: {FullXSLfilePath}", MsgBoxStyle.Exclamation, "File not found")
                End If

            End If
        Else
            MsgBox($"The template with ID: {key}, and filename: {FullXMLfilePath} was not found", MsgBoxStyle.Exclamation, "Template not found")
        End If

        Return templateXML
    End Function
    Public Sub MakeXDTsFromTemplateMap(
                           templatesMap As Dictionary(Of String, String),
                           Optional path As String = "",
                           Optional createHTML As Boolean = False)

        XMLfilePath = path 'the place where we will store the XML / HTML files
        Dim templateXML As String
        For Each file In templatesMap
            templateXML = MakeXDT(file.Key)
            If Not String.IsNullOrWhiteSpace(templateXML) Then ' > "" Then
                If createHTML Then
                    Try
                        MakeHTML()
                    Catch ex As Exception
                        MsgBox($"Error creating HTML for template with ID: {file.Key}, and filename: {FullXMLfilePath} " + vbCrLf + ex.Message,
                               MsgBoxStyle.Exclamation, "Template not found")
                    End Try
                End If
            Else
                MsgBox($"The template with ID: {file.Key}, and filename: {FullXMLfilePath} was not generated", MsgBoxStyle.Exclamation, "Template not generated")
            End If
        Next

    End Sub

#End Region

#Region "Private Helper Methods"

    Private Function MakeXDT(key As String, Optional customFileName As String = "") As String
        '!Create the XML:

        Dim stb As SDC.SDCTreeBuilderEcc =
            New SDC.SDCTreeBuilderEcc(key, DatasetBuilder, XMLfilePath + "\" + XslFileName)
        FileName = ""
        'XMLfileName = stb.FormDesign.filename.Replace(":", ".")
        'HTMLfileName = XMLfileName.Replace(".xml", ".html")

        If customFileName.Length > 0 Then
            XMLfileName = customFileName + ".xml"
            HTMLfileName = customFileName + ".html"
        Else
            XMLfileName = stb.FormDesign.filename.Replace(":", ".")
            HTMLfileName = XMLfileName.Replace(".xml", ".html")
        End If

        Dim formDesignXml As String = stb.FormDesign.Serialize()

        Dim orig As String = "<?xml version=""1.0"" encoding=""utf-8""?>"
        Dim fix As String = orig + vbCrLf + "<?xml-stylesheet type=""text/xsl"" href=""sdctemplate.xslt""?>"
        formDesignXml = formDesignXml.Replace(orig, fix)
        FullXMLfilePath = String.Format("{0}\{1}", XMLfilePath, XMLfileName)
        FullHTMLfilePath = String.Format("{0}\{1}", HTMLfilePath, HTMLfileName)
        System.IO.File.WriteAllText(FullXMLfilePath, formDesignXml, System.Text.Encoding.UTF8)



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

        Transform.Transform(FullXMLfilePath, FullHTMLfilePath)

        'RaiseEvent HTML_Ready(Filename & ".html")
    End Sub

    Private Sub ShowBrowser()
        If BrowserPath = "" Then
            BrowserPath = "C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe"   'For 32 bit Firefox
            If Not My.Computer.FileSystem.FileExists(BrowserPath) Then
                BrowserPath = "C:\\Program Files\\Mozilla Firefox\\firefox.exe"  'For 64 bit Firefox
            End If
        End If

        If My.Computer.FileSystem.FileExists(BrowserPath) Then
            Process.Start(BrowserPath, """" + FullHTMLfilePath + """")
        Else
            MsgBox(String.Format("Browser filepath not found: {0}", BrowserPath.ToString), MsgBoxStyle.Critical, "File Not Found")
        End If
    End Sub

#End Region

End Class

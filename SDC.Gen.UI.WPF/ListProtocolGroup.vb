'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class ListProtocolGroup
    Public Property ProtocolGroupKey As Integer
    Public Property NamespaceKey As Integer
    Public Property ProtocolGroup As String
    Public Property ProtocolGroupSortName As String
    Public Property URI_Namespace As String
    Public Property ExternalKey As String
    Public Property CreatedByUserKey As Integer
    Public Property CreatedDateTime As Date
    Public Property LastUpdatedByUserKey As Integer
    Public Property LastUpdatedDateTime As Date
    Public Property VisibleFlag As Boolean
    Public Property TS As Byte()

    Public Overridable Property ProtocolGroupMappings As ICollection(Of ProtocolGroupMapping) = New HashSet(Of ProtocolGroupMapping)

End Class

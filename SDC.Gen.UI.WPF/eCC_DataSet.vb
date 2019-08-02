Partial Class eCC_DataSet
    Partial Public Class ChecklistTemplateVersionsDataTable
        Private Sub ChecklistTemplateVersionsDataTable_ColumnChanging(sender As Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.ChecklistTemplateVersionCkeyColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class
End Class

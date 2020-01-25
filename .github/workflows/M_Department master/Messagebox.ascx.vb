Imports AjaxControlToolkit
Partial Class Usercontrol_Messagebox
    Inherits System.Web.UI.UserControl
    Dim _message As String
    'Public Property showMessage() As Boolean
    '    Get
    '        Return (_message)
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _message = value
    '    End Set
    'End Property

    Public Function Show(ByVal Message As String) As Boolean
        lbl_msg.Text = Message
        ModalPopupExtender2.Show()
        Return True
    End Function
End Class

'This class was created to handle the late binding caused by changing the background image from the Maximize button during runtime 
Imports System.Drawing
Imports System.Windows.Forms

Public NotInheritable Class ControlBoxButton
    Inherits Button

    Private img_Close As Bitmap = My.Resources.Button_Close
    Private img_Minimize As Bitmap = My.Resources.Button_Minimize
    Private img_Maximize_0 As Bitmap = My.Resources.Button_Maximize_0
    Private img_Maximize_1 As Bitmap = My.Resources.Button_Maximize_1

    Public WriteOnly Property SetImage() As Integer
        Set(ByVal value As Integer)
            Select Case value
                Case 0
                    Me.BackgroundImage = img_Close
                Case 1
                    Me.BackgroundImage = img_Maximize_0
                Case 2
                    Me.BackgroundImage = img_Minimize
                Case 3
                    Me.BackgroundImage = img_Maximize_1
            End Select
        End Set
    End Property

    Public Sub New()
        Me.BackgroundImageLayout = ImageLayout.Center
    End Sub
End Class

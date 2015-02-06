'This class was created to handle the late binding caused by changing the background image from the Maximize button during runtime 
Imports System.Drawing
Imports System.Windows.Forms

Public NotInheritable Class ControlBoxButton
    Inherits Button

    Private img_Close(1) As Bitmap
    Private img_Minimize(1) As Bitmap
    Private img_Maximize(3) As Bitmap

    Private isOnHover As Boolean = False

    Public Sub New()
        'Do this with a loop to make it loop prettier?
        img_Close(0) = My.Resources.Button_Close_2_0
        img_Close(1) = My.Resources.Button_Close_2_1
        img_Minimize(0) = My.Resources.Button_Minimize_2_0
        img_Minimize(1) = My.Resources.Button_Minimize_2_1
        img_Maximize(0) = My.Resources.Button_Maximize_2_0_0
        img_Maximize(1) = My.Resources.Button_Maximize_2_0_1
        img_Maximize(2) = My.Resources.Button_Maximize_2_1_0
        img_Maximize(3) = My.Resources.Button_Maximize_2_1_1

        Me.BackgroundImageLayout = ImageLayout.Center Or ImageLayout.Stretch
        Me.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.BackColor = Color.White
        Me.FlatStyle = Windows.Forms.FlatStyle.Flat
        Me.FlatAppearance.MouseOverBackColor = Nothing
        Me.FlatAppearance.BorderColor = Color.White
        'Me.FlatAppearance.BorderSize = 0
        Me.Size = New Size(30, 26)
    End Sub

    Public WriteOnly Property SetImage() As Integer
        Set(ByVal value As Integer)
            Select Case value
                Case 0
                    Me.BackgroundImage = img_Close(0)
                Case 1
                    Me.BackgroundImage = img_Maximize(0)
                Case 2
                    Me.BackgroundImage = img_Minimize(0)
                Case 3
                    Me.BackgroundImage = img_Maximize(2)
            End Select
        End Set
    End Property

    Public WriteOnly Property SetHoverImage() As Integer
        Set(ByVal value As Integer)
            If isOnHover Then
                Select Case value
                    Case 0 'Close
                        Me.BackgroundImage = img_Close(1)
                    Case 1 'Maximize
                        Me.BackgroundImage = img_Maximize(1)
                    Case 2 'Minimize
                        Me.BackgroundImage = img_Minimize(1)
                    Case 3 'Maximize 2
                        Me.BackgroundImage = img_Maximize(3)
                End Select
            End If
        End Set
    End Property

    Public WriteOnly Property SetHover() As Boolean
        Set(ByVal value As Boolean)
            isOnHover = value
        End Set
    End Property
End Class

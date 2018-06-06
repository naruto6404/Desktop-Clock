Public Class Form1

    Dim hr, min, sec As Integer
    Dim pic As Image() = {My.Resources.n0, My.Resources.n1, My.Resources.n2, My.Resources.n3, My.Resources.n4, My.Resources.n5, My.Resources.n6, My.Resources.n7, My.Resources.n8, My.Resources.n9}
    Dim l, drag As Boolean
    Dim l2, x1, y, dragging_x, dragging_y As Integer
    Dim lo As String()
    Dim h12 As Boolean
    Dim tmbtn_me As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        timeshow()
        If My.Computer.FileSystem.FileExists("C:\Windows\ClockConfig.cfg") = True Then
            lo = Split(My.Computer.FileSystem.ReadAllText("C:\Windows\ClockConfig.cfg"), ",")
            Me.Location = New Point(lo(0), lo(1))
            If lo(2) = "True" Then
                h12 = True
                Button2.Image = My.Resources.h12
            Else
                h12 = False
                Button2.Image = My.Resources.h24
            End If
            If Me.TopMost Then
                Button3.Image = My.Resources.topmost_btn_on
            Else
                Button3.Image = My.Resources.topmost_btn
            End If
        End If
    End Sub

    Sub timeshow()
        hr = Now.Hour : min = Now.Minute : sec = Now.Second

        Label1.Text = Now.Year & "/" & Now.Month & "/" & Now.Day

        If h12 = True Then
            If hr > 12 Then
                hr -= 12
            ElseIf hr = 0 Then
                hr = 12
            End If
        End If

        If hr < 10 Then
            PictureBox1.Image = pic(0)
        Else
            PictureBox1.Image = pic(Strings.Left(hr, 1))
        End If

        PictureBox2.Image = pic(Strings.Right(hr, 1))

        If min < 10 Then
            PictureBox3.Image = pic(0)
        Else
            PictureBox3.Image = pic(Strings.Left(min, 1))
        End If

        PictureBox4.Image = pic(Strings.Right(min, 1))

        If sec < 10 Then
            PictureBox5.Image = pic(0)
        Else
            PictureBox5.Image = pic(Strings.Left(sec, 1))
        End If

        PictureBox6.Image = pic(Strings.Right(sec, 1))
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        timeshow()

        If tmbtn_me Then
            If Me.TopMost Then
                Button3.Image = My.Resources.topmost_btn_on
            Else
                Button3.Image = My.Resources.topmost_btn
            End If
        End If

        bar_hidden()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Image = My.Resources.Close_btn_press
        Me.Close()
    End Sub

    Private Sub Button1_MouseDown(sender As Object, e As MouseEventArgs) Handles Button1.MouseDown
        Button1.Image = My.Resources.Close_btn_press
    End Sub

    Private Sub Button1_MouseUp(sender As Object, e As MouseEventArgs) Handles Button1.MouseUp
        Button1.Image = My.Resources.Close_btn
        Me.Close()
    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        tmbtn_me = True
        Button1.Image = My.Resources.Close_btn_hover
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        tmbtn_me = True
        Button1.Image = My.Resources.Close_btn
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.TopMost = Not Me.TopMost
        If Me.TopMost Then
            Button3.Image = My.Resources.topmost_btn_on
        Else
            Button3.Image = My.Resources.topmost_btn
        End If
    End Sub

    Private Sub Button3_MouseEnter(sender As Object, e As EventArgs) Handles Button3.MouseEnter
        Button3.Image = My.Resources.topmost_btn_hover
        tmbtn_me = True
    End Sub

    Private Sub Button3_MouseLeave(sender As Object, e As EventArgs) Handles Button3.MouseLeave
        If Me.TopMost Then
            Button3.Image = My.Resources.topmost_btn_on
        Else
            Button3.Image = My.Resources.topmost_btn
        End If
        tmbtn_me = False
    End Sub

    Private Sub P2_MouseDown(sender As Object, e As MouseEventArgs) Handles P2.MouseDown, PictureBox1.MouseDown, PictureBox2.MouseDown, PictureBox3.MouseDown, PictureBox4.MouseDown, PictureBox5.MouseDown, PictureBox6.MouseDown, Label1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            drag = True
            dragging_x = e.X
            dragging_y = e.Y
        End If
    End Sub

    Private Sub P2_MouseMove(sender As Object, e As MouseEventArgs) Handles P2.MouseMove, PictureBox1.MouseMove, PictureBox2.MouseMove, PictureBox3.MouseMove, PictureBox4.MouseMove, PictureBox5.MouseMove, PictureBox6.MouseMove, Label1.MouseMove
        If drag = True Then
            Me.Left += e.X - dragging_x
            Me.Top += e.Y - dragging_y
        End If
        bar_hidden()
    End Sub

    Private Sub P2_MouseUp(sender As Object, e As MouseEventArgs) Handles P2.MouseUp, PictureBox1.MouseUp, PictureBox2.MouseUp, PictureBox3.MouseUp, PictureBox4.MouseUp, PictureBox5.MouseUp, PictureBox6.MouseUp, Label1.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If Me.Top < 0 Then
                Me.Top = 0
            ElseIf Me.Top + Me.Height > My.Computer.Screen.WorkingArea.Width Then
                Me.Top = My.Computer.Screen.WorkingArea.Width - Me.Height
            End If

            If Me.Left < 0 Then
                Me.Left = 0
            ElseIf Me.Left + Me.Width > My.Computer.Screen.WorkingArea.Width Then
                Me.Left = My.Computer.Screen.WorkingArea.Width - Me.Width
            End If
            drag = False
            My.Computer.FileSystem.WriteAllText("C:\Windows\ClockConfig.cfg", Me.Location.X & "," & Me.Location.Y & "," & h12, False)
        End If
    End Sub

    Sub bar_hidden()
        If MousePosition.X > Me.Left Then
            If MousePosition.Y > Me.Top Then
                If MousePosition.X < Me.Left + Me.Width Then
                    If MousePosition.Y < Me.Top + Me.Height Then
                        P2.Top = 0
                        Button1.Top = 0
                        Button2.Top = 0
                        Button3.Top = 0
                        PictureBox1.Top = 63
                        PictureBox2.Top = 63
                        PictureBox3.Top = 63
                        PictureBox4.Top = 63
                        PictureBox5.Top = 63
                        PictureBox6.Top = 63
                        Label1.Top = 11
                        Me.Height = 125
                    End If
                End If
            End If
        Else
            P2.Top = 0 - 48
            Button1.Top = 0 - 48
            Button2.Top = 0 - 48
            Button3.Top = 0 - 48
            PictureBox1.Top = 63 - 48
            PictureBox2.Top = 63 - 48
            PictureBox3.Top = 63 - 48
            PictureBox4.Top = 63 - 48
            PictureBox5.Top = 63 - 48
            PictureBox6.Top = 63 - 48
            Label1.Top = 11 - 48
            Me.Height = 125 - 48
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If h12 = True Then
            Button2.Image = My.Resources.h24
            h12 = False
        Else
            Button2.Image = My.Resources.h12
            h12 = True
        End If
        My.Computer.FileSystem.WriteAllText("C:\Windows\ClockConfig.cfg", Me.Location.X & "," & Me.Location.Y & "," & h12, False)
    End Sub
End Class

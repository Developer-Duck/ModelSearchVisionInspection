using ModelSearchVisionInspection.Views.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace ModelSearchVisionInspection.Views.Login
{
    public partial class LoginMain : Window
    {
        public LoginMain()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LogintxtUsername.Text == "admin" && LogintxtPassword.Password == "1234")
            {
                var mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow; // MainWindow 지정
                mainWindow.Show();
                this.Close(); // Login 창 닫기
            }
            else
            {
                MessageBox.Show("Invalid credentials");
            }
        }
        private void MoveToSignUp(object sender, RoutedEventArgs e)
        {
            var signUpWindow = new SignUpMain();
            signUpWindow.Show();  // SignUp 창을 일반 창으로 열기
            this.Close();         // 로그인 창 닫기
        }
    }
}
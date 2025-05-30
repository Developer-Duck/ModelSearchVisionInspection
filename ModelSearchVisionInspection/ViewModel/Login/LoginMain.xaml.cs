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
    /// <summary>
    /// LoginMain.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginMain : Window
    {
        public LoginMain()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // 간단한 로그인 체크 예시
            if (txtUsername.Text == "admin" && txtPassword.Password == "1234")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close(); // 로그인 창 닫기
            }
            else
            {
                MessageBox.Show("Invalid credentials");
            }
        }

    }
}

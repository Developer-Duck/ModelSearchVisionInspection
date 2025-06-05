using ModelSearchVisionInspection.Views.Login;
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

namespace ModelSearchVisionInspection.Views.SignUp
{
    /// <summary>
    /// SignUpMain.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SignUpMain : Window
    {
        public SignUpMain()
        {
            InitializeComponent();
        }
        private void MoveToLogin(object sender, RoutedEventArgs e)
        {
            var LoginWindow = new LoginMain();
            LoginWindow.Show();  // SignUp 창을 일반 창으로 열기
            this.Close();         // 로그인 창 닫기
        }
    }
}

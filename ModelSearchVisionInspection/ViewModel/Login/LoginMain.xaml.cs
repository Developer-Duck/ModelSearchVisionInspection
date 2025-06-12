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
using System.IO;
using Newtonsoft.Json;

namespace ModelSearchVisionInspection.Views.Login
{
    public partial class LoginMain : Window
    {
        private readonly string jsonFilePath = "users.json";

        public LoginMain()
        {
            InitializeComponent();

            // 키보드 이벤트 핸들러 추가
            this.KeyDown += LoginMain_KeyDown;
            LogintxtUsername.KeyDown += InputField_KeyDown;
            LogintxtPassword.KeyDown += InputField_KeyDown;
        }

        // 윈도우 전체에서 엔터키 처리
        private void LoginMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        // 입력 필드에서 엔터키 처리
        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // 입력값 유효성 검사
            if (string.IsNullOrWhiteSpace(LogintxtUsername.Text))
            {
                MessageBox.Show("사용자명 또는 이메일을 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                LogintxtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(LogintxtPassword.Password))
            {
                MessageBox.Show("비밀번호를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                LogintxtPassword.Focus();
                return;
            }

            // 기본 admin 계정 체크
            if (LogintxtUsername.Text == "admin" && LogintxtPassword.Password == "1234")
            {
                LoginSuccess();
                return;
            }

            // JSON 파일에서 사용자 정보 확인
            if (ValidateUserFromJson(LogintxtUsername.Text, LogintxtPassword.Password))
            {
                LoginSuccess();
            }
            else
            {
                MessageBox.Show("사용자명 또는 비밀번호가 올바르지 않습니다.", "로그인 실패", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateUserFromJson(string username, string password)
        {
            try
            {
                // JSON 파일에서 사용자 목록 로드
                List<UserInfo> users = LoadUsersFromJson();

                // 사용자명과 비밀번호 확인 (대소문자 구분 없이)
                return users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == password);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"사용자 정보 확인 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private List<UserInfo> LoadUsersFromJson()
        {
            if (!File.Exists(jsonFilePath))
            {
                return new List<UserInfo>();
            }

            string json = File.ReadAllText(jsonFilePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<UserInfo>();
            }

            return JsonConvert.DeserializeObject<List<UserInfo>>(json) ?? new List<UserInfo>();
        }

        private void LoginSuccess()
        {
            var mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow; // MainWindow 지정
            mainWindow.Show();
            this.Close(); // Login 창 닫기
        }

        private void MoveToSignUp(object sender, RoutedEventArgs e)
        {
            var signUpWindow = new SignUpMain();
            signUpWindow.Show();  // SignUp 창을 일반 창으로 열기
            this.Close();         // 로그인 창 닫기
        }
    }

    // 사용자 정보 클래스 (SignUp과 동일한 구조)
    public class UserInfo
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
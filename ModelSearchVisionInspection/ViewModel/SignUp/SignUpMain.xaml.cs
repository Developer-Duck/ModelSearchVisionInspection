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
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ModelSearchVisionInspection.Views.SignUp
{
    /// <summary>
    /// SignUpMain.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SignUpMain : Window
    {
        private readonly string jsonFilePath = "users.json";

        public SignUpMain()
        {
            InitializeComponent();
            // 회원가입 버튼 클릭 이벤트 연결
            signInButton.Click += SignInButton_Click;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            // 이메일 입력 확인
            if (string.IsNullOrWhiteSpace(SignIntxtUsername.Text))
            {
                MessageBox.Show("이메일을 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                SignIntxtUsername.Focus();
                return;
            }

            // 이메일 형식 검증
            if (!IsValidEmail(SignIntxtUsername.Text))
            {
                MessageBox.Show("올바른 이메일 형식을 입력해주세요.\n예: example@domain.com", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                SignIntxtUsername.Focus();
                return;
            }

            // 비밀번호 입력 확인
            if (string.IsNullOrWhiteSpace(SignIntxtPassword.Password))
            {
                MessageBox.Show("비밀번호를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                SignIntxtPassword.Focus();
                return;
            }

            // 비밀번호 유효성 검사
            if (!IsValidPassword(SignIntxtPassword.Password))
            {
                MessageBox.Show("비밀번호는 6글자 이상이며 대문자를 포함해야 합니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                SignIntxtPassword.Focus();
                return;
            }

            // 비밀번호 확인 입력 확인
            if (string.IsNullOrWhiteSpace(ChacktxtPassword.Password))
            {
                MessageBox.Show("비밀번호 확인을 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                ChacktxtPassword.Focus();
                return;
            }

            // 비밀번호 일치 확인
            if (SignIntxtPassword.Password != ChacktxtPassword.Password)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 개인정보 수집 동의 체크 확인
            if (!check_information.IsChecked.HasValue || !check_information.IsChecked.Value)
            {
                MessageBox.Show("개인정보 수집에 동의해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 모든 조건이 충족되면 회원가입 성공
            ProcessSignUp();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                // 정규표현식을 사용한 이메일 형식 검증
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            // 비밀번호는 6글자
            if (password.Length < 6)
                return false;
        }

        private void ProcessSignUp()
        {
            try
            {
                // 사용자 정보를 JSON에 저장
                SaveUserToJson(SignIntxtUsername.Text, SignIntxtPassword.Password);

                // 회원가입 성공 메시지 표시
                MessageBox.Show("회원가입이 성공적으로 완료되었습니다!", "회원가입 성공", MessageBoxButton.OK, MessageBoxImage.Information);

                // 로그인 페이지로 이동
                MoveToLoginPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"회원가입 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveUserToJson(string email, string password)
        {
            // 기존 사용자 목록 로드
            List<UserInfo> users = LoadUsersFromJson();

            // 중복 이메일 체크
            if (users.Any(u => u.Username.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("이미 존재하는 계정입니다.");
            }

            // 새 사용자 정보 생성
            var newUser = new UserInfo
            {
                Id = Guid.NewGuid().ToString(),
                Username = email,
                Password = password,
                CreatedDate = DateTime.Now
            };

            // 사용자 목록에 추가
            users.Add(newUser);

            // JSON 파일에 저장
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
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

        private void MoveToLoginPage()
        {
            var LoginWindow = new LoginMain();
            LoginWindow.Show();  // 로그인 창을 일반 창으로 열기
            this.Close();        // 회원가입 창 닫기
        }

        private void MoveToLogin(object sender, RoutedEventArgs e)
        {
            MoveToLoginPage();
        }
    }

    // 사용자 정보 클래스
    public class UserInfo
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
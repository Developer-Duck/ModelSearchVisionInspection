using ModelSearchVisionInspection.Models;
using ModelSearchVisionInspection.Services;
using ModelSearchVisionInspection.Views.Login;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ModelSearchVisionInspection.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly UserService _userService;
        private string _username;
        private string _password;
        private string _confirmPassword;
        private bool _isPrivacyAgreed;

        public SignUpViewModel()
        {
            _userService = new UserService();
            SignUpCommand = new RelayCommand(ExecuteSignUp, CanExecuteSignUp);
            MoveToLoginCommand = new RelayCommand(ExecuteMoveToLogin);
        }

        public string Username
        {
            get => _username;
            set
            {
                if (SetProperty(ref _username, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (SetProperty(ref _confirmPassword, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public bool IsPrivacyAgreed
        {
            get => _isPrivacyAgreed;
            set
            {
                if (SetProperty(ref _isPrivacyAgreed, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public ICommand SignUpCommand { get; }
        public ICommand MoveToLoginCommand { get; }

        private bool CanExecuteSignUp(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                   IsPrivacyAgreed;
        }

        private void ExecuteSignUp(object parameter)
        {
            try
            {
                // 이메일 형식 검증
                if (!IsValidEmail(Username))
                {
                    MessageBox.Show("올바른 이메일 형식을 입력해주세요.\n예: example@domain.com", "알림",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 비밀번호 유효성 검사
                if (!IsValidPassword(Password))
                {
                    MessageBox.Show("비밀번호는 6글자 이상이며 대문자를 포함해야 합니다.", "알림",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 비밀번호 일치 확인
                if (Password != ConfirmPassword)
                {
                    MessageBox.Show("비밀번호가 일치하지 않습니다.", "알림",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 새 사용자 정보 생성
                var newUser = new UserInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = Username,
                    Password = Password,
                    CreatedDate = DateTime.Now
                };

                // 사용자 저장
                _userService.SaveUser(newUser);

                // 회원가입 성공 메시지
                MessageBox.Show("회원가입이 성공적으로 완료되었습니다!", "회원가입 성공",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // 로그인 페이지로 이동
                ExecuteMoveToLogin(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"회원가입 중 오류가 발생했습니다: {ex.Message}", "오류",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteMoveToLogin(object parameter)
        {
            var loginWindow = new LoginMain();
            loginWindow.Show();

            // 현재 회원가입 창 닫기
            var currentWindow = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(w => w.GetType().Name == "SignUpMain");
            currentWindow?.Close();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
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
            // 비밀번호는 6글자 이상이며 대문자를 포함해야 함
            if (password.Length < 6)
                return false;

            return password.Any(char.IsUpper);
        }
    }
}
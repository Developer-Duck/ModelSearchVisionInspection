using ModelSearchVisionInspection.Services;
using ModelSearchVisionInspection.Views.SignUp;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ModelSearchVisionInspection.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UserService _userService;
        private string _username;
        private string _password;
        private bool _rememberMe;

        public LoginViewModel()
        {
            _userService = new UserService();
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            MoveToSignUpCommand = new RelayCommand(ExecuteMoveToSignUp);
        }

        public string Username
        {
            get => _username;
            set
            {
                if (SetProperty(ref _username, value))
                {
                    // CanExecute 상태 변경 알림
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
                    // CanExecute 상태 변경 알림
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public bool RememberMe
        {
            get => _rememberMe;
            set => SetProperty(ref _rememberMe, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand MoveToSignUpCommand { get; }

        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin(object parameter)
        {
            try
            {
                if (_userService.ValidateUser(Username, Password))
                {
                    // 로그인 성공
                    var mainWindow = new MainWindow();
                    Application.Current.MainWindow = mainWindow;
                    mainWindow.Show();

                    // 현재 로그인 창 닫기
                    var currentWindow = Application.Current.Windows
                        .Cast<Window>()
                        .FirstOrDefault(w => w.GetType().Name == "LoginMain");
                    currentWindow?.Close();
                }
                else
                {
                    MessageBox.Show("사용자명 또는 비밀번호가 올바르지 않습니다.", "로그인 실패",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"로그인 중 오류가 발생했습니다: {ex.Message}", "오류",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteMoveToSignUp(object parameter)
        {
            var signUpWindow = new SignUpMain();
            signUpWindow.Show();

            // 현재 로그인 창 닫기
            var currentWindow = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(w => w.GetType().Name == "LoginMain");
            currentWindow?.Close();
        }
    }
}
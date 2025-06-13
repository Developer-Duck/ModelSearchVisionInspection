using ModelSearchVisionInspection.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace ModelSearchVisionInspection.Views.Login
{
    public partial class LoginMain : Window
    {
        private LoginViewModel _viewModel;

        public LoginMain()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;

            // PasswordBox의 Password 바인딩 처리
            LogintxtPassword.PasswordChanged += (s, e) =>
            {
                _viewModel.Password = LogintxtPassword.Password;
            };

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
                if (_viewModel?.LoginCommand.CanExecute(null) == true)
                {
                    _viewModel.LoginCommand.Execute(null);
                }
            }
        }

        // 입력 필드에서 엔터키 처리
        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_viewModel?.LoginCommand.CanExecute(null) == true)
                {
                    _viewModel.LoginCommand.Execute(null);
                }
            }
        }
    }
}
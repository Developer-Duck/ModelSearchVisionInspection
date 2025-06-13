using ModelSearchVisionInspection.ViewModel;
using System.Windows;

namespace ModelSearchVisionInspection.Views.SignUp
{
    /// <summary>
    /// SignUpMain.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SignUpMain : Window
    {
        private SignUpViewModel _viewModel;

        public SignUpMain()
        {
            InitializeComponent();
            _viewModel = new SignUpViewModel();
            DataContext = _viewModel;

            // PasswordBox의 Password 바인딩 처리
            SignIntxtPassword.PasswordChanged += (s, e) =>
            {
                _viewModel.Password = SignIntxtPassword.Password;
            };

            ChacktxtPassword.PasswordChanged += (s, e) =>
            {
                _viewModel.ConfirmPassword = ChacktxtPassword.Password;
            };
        }
    }
}
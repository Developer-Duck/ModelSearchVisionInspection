using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ModelSearchVisionInspection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel();
            this.DataContext = viewModel;

            // 창 너비 계산
            viewModel.UpdateSize(this.ActualWidth, this.ActualHeight);
            this.SizeChanged += MainWindow_SizeChanged;

            // 현재 시간 초기 설정
            viewModel.UpdateCurrentDateTime();

            //  타이머 설정
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (s, e) => viewModel.UpdateCurrentDateTime();
            _timer.Start();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            viewModel.UpdateSize(this.ActualWidth, this.ActualHeight);
        }

    }
}
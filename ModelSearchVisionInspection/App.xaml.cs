using ModelSearchVisionInspection.Views.Login;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ModelSearchVisionInspection
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var login = new LoginMain();
            login.Show();
        }

    }

}

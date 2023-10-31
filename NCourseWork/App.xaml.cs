namespace NCourseWork
{
    using NCourseWork.Services.Navigation;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private readonly MainWindow mainWindow;
        private readonly INavigationService navigationService;

        public App(MainWindow mainWindow, INavigationService navigationService)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.navigationService = navigationService;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            navigationService.Init();
            mainWindow.Show();
        }
    }
}

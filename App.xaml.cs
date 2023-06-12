namespace activity2;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new FlyoutPage
        {
            Flyout = new NavigationPage(new MainPage()),
            Detail = new NavigationPage(new HolidaysPage())
        };
    }
}


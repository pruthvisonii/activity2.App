using Microsoft.Maui.Controls;

namespace activity2
{
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnHolidaysClicked(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new HolidaysPage());
            IsPresented = false;
        }
        private void OnYearPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle the SelectedIndexChanged event here
            // Add your custom logic
        }
    }
}

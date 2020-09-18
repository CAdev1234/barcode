using Xamarin.Forms;

namespace Inventario.Views
{
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {
            InitializeComponent();
        }

        private void ZXingScanner_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync();
                Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new ResultadosPage(result.Text)) };
            });
        }

    }
}

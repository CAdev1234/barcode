using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace Inventario.Views
{
    public partial class RegistrarPage : ContentPage
    {
        public RegistrarPage()
        {
            InitializeComponent();
        }

        public async Task<bool> CheckPermissions(Permission permission)
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            bool request = false;
            if (permissionStatus == PermissionStatus.Denied)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var title = $"Permissões de {permission}";
                    var question = $"Para poder escanear plaquetas a permissão de {permission} é necessária.";
                    var positive = "Definiçoes";
                    var negative = "Mais tarde";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null) return false;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return false;
                }
                request = true;
            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                var newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                if (newStatus.ContainsKey(permission) && newStatus[permission] != PermissionStatus.Granted)
                {
                    var title = $"Permissões de {permission}";
                    var question = $"Para poder escanear plaquetas a permissão de {permission} é necessária.";
                    var positive = "Definiçoes";
                    var negative = "Mais tarde";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null) return false;
                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return false;
                }
            }
            return true;
        }

        async private void ScanClicked(object sender, EventArgs e)
        {
            //prevenção para duplo click
            var haspermission = await CheckPermissions(Permission.Camera);

            //https://github.com/Redth/ZXing.Net.Mobile/issues/717
            await Task.Delay(1000);
            if (haspermission) await Navigation.PushAsync(new ScanPage());
        }

        async private void CadastrarClicked(object sender, EventArgs e)
        {
            var haspermission = await CheckPermissions(Permission.Camera);
            await Task.Delay(1000);
            if (haspermission)
                Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new NovoCadastroPage("")) };
        }

    }
}

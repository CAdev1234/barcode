using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using LiteDB;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Inventario.Views
    {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer

    public partial class UsuarioPage : ContentPage
        {



        public UsuarioPage()
            {
            InitializeComponent();


            }

        async private void GravarClicked(object sender, EventArgs e)
            {

            Preferences.Set("idUsuario", Usuario.Text);

            await DisplayAlert("Usuario", "Usuario atualizado com sucesso", "OK");

            Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new HomePage()) };
            }


        protected override void OnAppearing()
            {
            base.OnAppearing();

            Usuario.Text = Preferences.Get("idUsuario", "default_value");
            }
        }
    }
using System;
using System.Linq;
using LiteDB;
using Xamarin.Forms;

namespace Inventario.Views
{
    public partial class ResultadosPage : ContentPage
    {
        string ResultadoGlobal;
        public ResultadosPage(string Resultado)
        {
            InitializeComponent();
            ResultadoGlobal = Resultado;
        } 

        private async void OkClicked(object sender, EventArgs e)
        {
            if (Items.SelectedItem != null)
            {
                Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new EditarPage(ItemsID.SelectedItem.ToString())) };
            }
            else
            {
                await DisplayAlert("Resultados", "Deve selecionar um Item", "OK");
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            LiteDatabase _dataBase;
            LiteCollection<Models.Inventario> _dbInventario;
            _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Inventario.db"));
            _dbInventario = _dataBase.GetCollection<Models.Inventario>();

            var inventarios = _dbInventario.FindAll();
            var query = (from x in inventarios where x.Plaqueta == ResultadoGlobal select x.DescOriginal).ToList();
            var queryID = (from x in inventarios where x.Plaqueta == ResultadoGlobal select x.ID).ToList();

            if (query.Count == 1)
                Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new EditarPage(queryID.FirstOrDefault().ToString())) };

            if (query.Count == 0)
                Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new NovoPage(ResultadoGlobal)) };

            ItemsCount.Text = "Items (" + query.Count.ToString() + ")";
            Items.ItemsSource = query;
            ItemsID.ItemsSource = queryID;
            Items.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Picker));
            Items.SelectedIndexChanged += (sender, args) =>
            {
                ItemsID.SelectedIndex = Items.SelectedIndex;
            };
        }
    }
}

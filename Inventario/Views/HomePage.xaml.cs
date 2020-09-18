using System;
using System.Threading.Tasks;
using LiteDB;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Inventario.Views
    {
    public partial class HomePage : ContentPage
        {
        public HomePage()

            {

            InitializeComponent();

       
            }

  

        protected async override void OnAppearing()
            {
            base.OnAppearing();

         
            
            LiteDatabase _dataBase;
            LiteCollection<Models.InventarioNovo> _dbInventarioNovo;
            _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Inventario.db"));
            _dbInventarioNovo = _dataBase.GetCollection<Models.InventarioNovo>();

            LiteCollection<Models.InventarioEditar> _dbInventarioEditar;
            _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Inventario.db"));
            _dbInventarioEditar = _dataBase.GetCollection<Models.InventarioEditar>();
       


            lastSync.Text = Preferences.Get("lastSync", "default_value");
            Novos.Text = _dbInventarioNovo.Count().ToString();
            Editados.Text = _dbInventarioEditar.Count().ToString();

            }

    
        }
    }

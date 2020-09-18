using System;
using System.IO;
using System.Linq;
using LiteDB;
using Xamarin.Forms;

namespace Inventario.Views
    {
    public partial class InventarioDetailPage : ContentPage
        {


        string IDGlobal;

        public InventarioDetailPage(string ID)
            {
            InitializeComponent();

            IDGlobal = ID;


            }


        protected override void OnAppearing()
            {
            base.OnAppearing();

            LiteDatabase _dataBase;
            LiteCollection<Models.Inventario> _dbInventario;
            _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Inventario.db"));
            _dbInventario = _dataBase.GetCollection<Models.Inventario>();


            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);


            var inventarios = _dbInventario.FindAll();


            var query = (from x in inventarios where x.ID == int.Parse(IDGlobal) select x).FirstOrDefault();

            ID.Text = query.ID.ToString();


            if (query.CaminhoFoto == "-----")

                {
                ImagemLabel.IsVisible = false;
                Imagem.IsVisible = false;

                }


            Imagem.Source = ImageSource.FromFile(Path.Combine(path, query.CaminhoFoto));


            Origem.Text = query.Estabelecimento.ToString();

            Plaqueta.Text = query.Plaqueta.ToString();

            DescOriginal.Text = query.DescOriginal.ToString();

            DescAccount.Text = query.DescAccount.ToString();

            OrigemAccount.Text = query.Estabelecimento1.ToString();

            Setor.Text = query.Setor.ToString();

            SetorAccount.Text = query.Setor1.ToString();

            Localidade.Text = query.Localidade.ToString();

            LocalidadeAccount.Text = query.Localidade1.ToString();

            CaminhoFoto.Text = query.CaminhoFoto.ToString();

            NroSerie.Text = query.NroSerie.ToString();

            Marca.Text = query.Marca.ToString();

            Modelo.Text = query.Modelo.ToString();

            Obs.Text = query.Obs.ToString();

            }



        }
    }

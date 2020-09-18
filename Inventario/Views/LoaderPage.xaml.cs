using System;
using System.Data.SqlClient;
using Inventario.Services;
using LiteDB;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Inventario.Views
{
    public partial class LoaderPage : ContentPage
    {
        public LoaderPage()
        {
            InitializeComponent();
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            ActivityIndicator.IsEnabled = true;
            ActivityIndicator.IsRunning = true;
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                Preferences.Set("lastSync", DateTime.Now.ToString());

                LiteDatabase _dataBase;
                LiteCollection<Models.Inventario> _dbInventario;
                _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Inventario.db"));
                _dbInventario = _dataBase.GetCollection<Models.Inventario>();

                _dataBase.DropCollection("Inventario");
                string cs = "Server=sql5045.site4now.net,1433;Initial Catalog=DB_A5746A_inventario;User ID=DB_A5746A_inventario_admin;Password=Senha@123";

                try
                {
                    SqlConnection sqlconnection = new SqlConnection(cs);
                    using (sqlconnection)
                    {
                        SqlCommand command = new SqlCommand("SELECT * FROM TB_Inventario_Ativo ORDER BY ID", sqlconnection);
                        sqlconnection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Models.Inventario inventario = new Models.Inventario();
                                inventario.ID = reader.GetInt32(0);
                                try
                                {
                                    inventario.ID_Planilha = reader.GetInt32(1).ToString();
                                }
                                catch
                                {
                                    inventario.ID_Planilha = "-----";
                                }
                                try
                                {
                                    inventario.idUsuario = reader.GetString(2);
                                }
                                catch
                                {
                                    inventario.idUsuario = "-----";
                                }
                                try
                                {
                                    inventario.iContagem = reader.GetInt32(3).ToString();
                                }
                                catch
                                {
                                    inventario.iContagem = "-----";
                                }
                                try
                                {
                                    inventario.Empresa = reader.GetString(4);
                                }
                                catch
                                {
                                    inventario.Empresa = "-----";
                                }
                                try
                                {
                                    inventario.Estabelecimento = reader.GetString(5);
                                }
                                catch
                                {
                                    inventario.Estabelecimento = "-----";
                                }
                                try
                                {
                                    inventario.Estabelecimento1 = reader.GetString(6);
                                }
                                catch
                                {
                                    inventario.Estabelecimento1 = "-----";
                                }
                                try
                                {
                                    inventario.Plaqueta = reader.GetString(7);
                                }
                                catch
                                {
                                    inventario.Plaqueta = "-----";
                                }
                                try
                                {
                                    inventario.DescOriginal = reader.GetString(8);
                                }
                                catch
                                {
                                    inventario.DescOriginal = "-----";
                                }
                                try
                                {
                                    inventario.DescAccount = reader.GetString(9);
                                }
                                catch
                                {
                                    inventario.DescAccount = "-----";
                                }
                                try
                                {
                                    inventario.DescAccount2 = reader.GetString(10);
                                }
                                catch
                                {
                                    inventario.DescAccount2 = "-----";
                                }
                                try
                                {
                                    inventario.Cidade = reader.GetString(11);
                                }
                                catch
                                {
                                    inventario.Cidade = "-----";
                                }
                                try
                                {
                                    inventario.Unidade = reader.GetString(12);
                                }
                                catch
                                {
                                    inventario.Unidade = "-----";
                                }
                                try
                                {
                                    inventario.Setor = reader.GetString(13);
                                }
                                catch
                                {
                                    inventario.Setor = "-----";
                                }
                                try
                                {
                                    inventario.Setor1 = reader.GetString(14);
                                }
                                catch
                                {
                                    inventario.Setor1 = "-----";
                                }
                                try
                                {
                                    inventario.Localidade = reader.GetString(15);
                                }
                                catch
                                {
                                    inventario.Localidade = "-----";
                                }
                                try
                                {
                                    inventario.Localidade1 = reader.GetString(16);
                                }
                                catch
                                {
                                    inventario.Localidade1 = "-----";
                                }
                                try
                                {
                                    inventario.CaminhoFoto = reader.GetString(17);
                                }
                                catch
                                {
                                    inventario.CaminhoFoto = "-----";
                                }
                                try
                                {
                                    inventario.NroSerie = reader.GetString(18);
                                }
                                catch
                                {
                                    inventario.NroSerie = "-----";
                                }
                                try
                                {
                                    inventario.Marca = reader.GetString(19);
                                }
                                catch
                                {
                                    inventario.Marca = "-----";
                                }
                                try
                                {
                                    inventario.Modelo = reader.GetString(20);
                                }
                                catch
                                {
                                    inventario.Modelo = "-----";
                                }
                                try
                                {
                                    inventario.Quantidade = reader.GetString(21);
                                }
                                catch
                                {
                                    inventario.Quantidade = "-----";
                                }
                                try
                                {
                                    inventario.dtInsert = reader.GetString(22);
                                }
                                catch
                                {
                                    inventario.dtInsert = "-----";
                                }
                                try
                                {
                                    inventario.dtUpdate = reader.GetString(23);
                                }
                                catch
                                {
                                    inventario.dtUpdate = "-----";
                                }
                                try
                                {
                                    inventario.flagNovoRegistro = reader.GetString(24);
                                }
                                catch
                                {
                                    inventario.flagNovoRegistro = "-----";
                                }
                                try
                                {
                                    inventario.flagBaixar = reader.GetString(25);
                                }
                                catch
                                {
                                    inventario.flagBaixar = "-----";
                                }
                                try
                                {
                                    inventario.Obs = reader.GetString(26);
                                }
                                catch
                                {
                                    inventario.Obs = "-----";
                                }
                                _dbInventario.Upsert(inventario);
                                //Imagens
                                IDownloader downloader = DependencyService.Get<IDownloader>();
                                try
                                {
                                    if (reader.GetString(17) != null)
                                    {
                                        var fonte = "http://ecosta00-001-site1.dtempurl.com/imagens/" + reader.GetString(17);
                                        downloader.DownloadFile(fonte, "");
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                            reader.Close();
                            sqlconnection.Close();
                        }
                    }
                }
                catch (Exception ex)

                {

                }
                //foreach (var inventario in _dbInventario.FindAll())
                //    {
                //    Console.WriteLine(inventario.ID + " " + inventario.DescOriginal);

                Application.Current.MainPage = new MainPage();
            }

            Application.Current.MainPage = new MainPage();
        }
    }
}


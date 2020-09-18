using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using Inventario.Services;
using LiteDB;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Inventario.Views
    {
    public partial class TransferirPage : ContentPage
        {
        public TransferirPage()
            {
            InitializeComponent();

            }



        async void TransferirClicked(object sender, EventArgs e)
            {

            Transferir.IsEnabled = false;


            ActivityIndicator.IsRunning = true;
            ActivityIndicator.IsVisible = true;

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
                {
                ActivityIndicator.IsRunning = false;
                ActivityIndicator.IsVisible = false;
                Transferir.IsEnabled = true;


                await DisplayAlert("Internet", "Sem conexão", "OK");
                return;
                }




            LiteDatabase _dataBase;
            LiteCollection<Models.InventarioEditar> _dbInventarioEditar;
            _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Inventario.db"));
            _dbInventarioEditar = _dataBase.GetCollection<Models.InventarioEditar>();
            var inventarioseditar = _dbInventarioEditar.FindAll();

            string cs = "Server=sql5045.site4now.net,1433;Initial Catalog=DB_A5746A_inventario;User ID=DB_A5746A_inventario_admin;Password=Senha@123";



            foreach (var item in inventarioseditar)
                {

                SqlConnection sqlconnectioneditar = new SqlConnection(cs);

                try
                    {

                    using (sqlconnectioneditar)
                        {


                        if (string.IsNullOrEmpty(item.Estabelecimento)) item.Estabelecimento = "-----";
                        if (string.IsNullOrEmpty(item.Plaqueta)) item.Plaqueta = "-----";
                        if (string.IsNullOrEmpty(item.DescOriginal)) item.DescOriginal = "-----";
                        if (string.IsNullOrEmpty(item.DescAccount)) item.DescAccount = "-----";
                        if (string.IsNullOrEmpty(item.Estabelecimento1)) item.Estabelecimento1 = "-----";
                        if (string.IsNullOrEmpty(item.Setor)) item.Setor = "-----";
                        if (string.IsNullOrEmpty(item.Setor1)) item.Setor1 = "-----";
                        if (string.IsNullOrEmpty(item.Localidade)) item.Localidade = "-----";
                        if (string.IsNullOrEmpty(item.Localidade1)) item.Localidade1 = "-----";
                        if (string.IsNullOrEmpty(item.CaminhoFoto)) item.CaminhoFoto = "-----";
                        if (string.IsNullOrEmpty(item.NroSerie)) item.NroSerie = "-----";
                        if (string.IsNullOrEmpty(item.Marca)) item.Marca = "-----";
                        if (string.IsNullOrEmpty(item.Modelo)) item.Modelo = "-----";
                        if (string.IsNullOrEmpty(item.Obs)) item.Obs = "-----";


                        SqlCommand commandeditar = new SqlCommand("UPDATE TB_Inventario_Ativo SET Estabelecimento = '" + item.Estabelecimento

                            + "', Plaqueta = '" + item.Plaqueta
                            + "', DescOriginal = '" + item.DescOriginal
                            + "', DescAccount = '" + item.DescAccount
                            + "', Estabelecimento1 = '" + item.Estabelecimento1
                            + "', Setor = '" + item.Setor
                            + "', Setor1 = '" + item.Setor1
                            + "', Localidade = '" + item.Localidade
                            + "', Localidade1 = '" + item.Localidade1
                            + "', CaminhoFoto = '" + item.CaminhoFoto
                            + "', NroSerie = '" + item.NroSerie
                            + "', Marca = '" + item.Marca
                            + "', Modelo = '" + item.Modelo
                            + "', Obs = '" + item.Obs
                            + "', dtUpdate = '" + item.dtUpdate
                            + "', idUsuario = '" + item.idUsuario

                            + "' WHERE ID=" + item.ID, sqlconnectioneditar);
                        sqlconnectioneditar.Open();
                        commandeditar.ExecuteNonQuery();


                        }




                    //Imagens
                    string urleditar = "ftp://FTP.SITE4NOW.NET/inventario/Imagens/" + item.CaminhoFoto;

                    WebClient requesteditar = new WebClient();


                    requesteditar.Credentials = new NetworkCredential("ecosta00-001", "Senha@123");

                    string patheditar = "/storage/emulated/0/Android/data/com.companyname.inventario/files/pictures/Inventario";


                    string filelocationeditar = Path.Combine(patheditar, item.CaminhoFoto);

                    try

                        {

                        await requesteditar.UploadFileTaskAsync(new Uri(urleditar), filelocationeditar);


                        }

                    catch (Exception ex)

                        {


                        }
                    requesteditar.Dispose();



                    }

                catch (Exception ex)
                    {



                    }




                }

            _dataBase.DropCollection("InventarioEditar");



            LiteCollection<Models.InventarioNovo> _dbInventarioNovo;
            _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Inventario.db"));
            _dbInventarioNovo = _dataBase.GetCollection<Models.InventarioNovo>();
            var inventariosnovos = _dbInventarioNovo.FindAll();


            foreach (var item in inventariosnovos)
                {

                SqlConnection sqlconnectionnovo = new SqlConnection(cs);

                try
                    {

                    using (sqlconnectionnovo)
                        {



                        if (string.IsNullOrEmpty(item.Estabelecimento)) item.Estabelecimento = "-----";
                        if (string.IsNullOrEmpty(item.Plaqueta)) item.Plaqueta = "-----";
                        if (string.IsNullOrEmpty(item.DescOriginal)) item.DescOriginal = "-----";
                        if (string.IsNullOrEmpty(item.DescAccount)) item.DescAccount = "-----";
                        if (string.IsNullOrEmpty(item.Estabelecimento1)) item.Estabelecimento1 = "-----";
                        if (string.IsNullOrEmpty(item.Setor)) item.Setor = "-----";
                        if (string.IsNullOrEmpty(item.Setor1)) item.Setor1 = "-----";
                        if (string.IsNullOrEmpty(item.Localidade)) item.Localidade = "-----";
                        if (string.IsNullOrEmpty(item.Localidade1)) item.Localidade1 = "-----";
                        if (string.IsNullOrEmpty(item.CaminhoFoto)) item.CaminhoFoto = "-----";
                        if (string.IsNullOrEmpty(item.NroSerie)) item.NroSerie = "-----";
                        if (string.IsNullOrEmpty(item.Marca)) item.Marca = "-----";
                        if (string.IsNullOrEmpty(item.Modelo)) item.Modelo = "-----";
                        if (string.IsNullOrEmpty(item.Obs)) item.Obs = "-----";



                        SqlCommand commandnovo = new SqlCommand("INSERT INTO TB_Inventario_Ativo (Estabelecimento, Plaqueta, DescOriginal, DescAccount, Estabelecimento1, Setor, Setor1, Localidade, Localidade1, CaminhoFoto, NroSerie, Marca, Modelo, Obs, dtInsert, idUsuario) VALUES('"
                            + item.Estabelecimento
                            + "', '" + item.Plaqueta
                            + "', '" + item.DescOriginal
                            + "', '" + item.DescAccount
                            + "', '" + item.Estabelecimento1
                            + "', '" + item.Setor
                            + "', '" + item.Setor1
                            + "', '" + item.Localidade
                            + "', '" + item.Localidade1
                            + "', '" + item.CaminhoFoto
                            + "', '" + item.NroSerie
                            + "', '" + item.Marca
                            + "', '" + item.Modelo
                            + "', '" + item.Obs
                            + "', '" + item.dtInsert
                            + "', '" + item.idUsuario
                            + "')", sqlconnectionnovo);

                        sqlconnectionnovo.Open();
                        commandnovo.ExecuteNonQuery();


                        }



                    }
                catch (Exception ex)
                    {



                    }




                string urlnovo = "ftp://FTP.SITE4NOW.NET/inventario/Imagens/" + item.CaminhoFoto;

                WebClient requestnovo = new WebClient();


                requestnovo.Credentials = new NetworkCredential("ecosta00-001", "Senha@123");

                string pathnovo = "/storage/emulated/0/Android/data/com.companyname.inventario/files/pictures/Inventario";


                string filelocationnovo = Path.Combine(pathnovo, item.CaminhoFoto);




                try

                    {

                    await requestnovo.UploadFileTaskAsync(new Uri(urlnovo), filelocationnovo);


                    }

                catch (Exception ex)

                    {


                    }

                requestnovo.Dispose();

                }




            _dataBase.DropCollection("InventarioNovo");



            LiteCollection<Models.Inventario> _dbInventario;
            _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Inventario.db"));
            _dbInventario = _dataBase.GetCollection<Models.Inventario>();

            _dataBase.DropCollection("Inventario");

            string csmaster = "Server=sql5045.site4now.net,1433;Initial Catalog=DB_A5746A_inventario;User ID=DB_A5746A_inventario_admin;Password=Senha@123";




            try
                {
                SqlConnection sqlconnectionmaster = new SqlConnection(csmaster);

                using (sqlconnectionmaster)
                    {
                    SqlCommand commandmaster = new SqlCommand("SELECT * FROM TB_Inventario_Ativo ORDER BY ID", sqlconnectionmaster);
                    sqlconnectionmaster.Open();
                    SqlDataReader reader = commandmaster.ExecuteReader();
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

                        }

                            Preferences.Set("lastSync", DateTime.Now.ToString());

                            ActivityIndicator.IsRunning = false;
                            ActivityIndicator.IsVisible = false;
                            Transferir.IsEnabled = true;

                            await DisplayAlert("Transferir", "Transferência efetuada com sucesso", "OK");



                            Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new HomePage()) };

                            }
                        }

            catch (Exception ex)
                {



                }

            }
        }
    }




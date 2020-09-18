using System;
using System.Threading.Tasks;
using LiteDB;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Inventario.Views
    {
    public partial class NovoPage : ContentPage
        {

        string ResultadoGlobal;

        public NovoPage(string Resultado)
            {
            InitializeComponent();

            ResultadoGlobal = Resultado;

            }

        protected override void OnAppearing()

            {

            Plaqueta.Text = ResultadoGlobal;


            }

        async private void GravarClicked(object sender, EventArgs e)
            {

            Models.InventarioNovo inventario = new Models.InventarioNovo();

            inventario.idUsuario = Preferences.Get("idUsuario", "default_value");
            inventario.dtInsert = DateTime.Now.ToString();
            inventario.Estabelecimento = Origem.Text;

            inventario.Plaqueta = Plaqueta.Text;

            inventario.DescOriginal = DescOriginal.Text;
            inventario.DescAccount = DescAccount.Text;
            inventario.Estabelecimento1 = OrigemAccount.Text;
            inventario.Setor = Setor.Text;
            inventario.Setor1 = SetorAccount.Text;
            inventario.Localidade = Localidade.Text;
            inventario.Localidade1 = LocalidadeAccount.Text; ;
            inventario.CaminhoFoto = CaminhoFoto.Text;
            inventario.NroSerie = NroSerie.Text;
            inventario.Marca = Marca.Text;
            inventario.Modelo = Modelo.Text;
            inventario.Obs = Obs.Text;

            LiteDatabase _dataBase;
            LiteCollection<Models.InventarioNovo> _dbInventarioNovo;
            _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Inventario.db"));
            _dbInventarioNovo = _dataBase.GetCollection<Models.InventarioNovo>();

            int id = _dbInventarioNovo.Count() == 0 ? 1 : (int)(_dbInventarioNovo.Max(x => x.ID) + 1);

            inventario.ID = id;

            _dbInventarioNovo.Upsert(inventario);

            await DisplayAlert("Novo", "Registro criado com sucesso", "OK");
            Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new RegistrarPage()) };
            }


        async private void LimparClicked(object sender, EventArgs e)
            {
            Origem.Text = "";
            // Plaqueta.Text = "";
            DescOriginal.Text = "";
            DescAccount.Text = "";
            OrigemAccount.Text = "";
            Setor.Text = "";
            SetorAccount.Text = "";
            Localidade.Text = "";
            LocalidadeAccount.Text = "";
            CaminhoFoto.Text = "";
            NroSerie.Text = "";
            Marca.Text = "";
            Modelo.Text = "";
            Obs.Text = "";
            }


        public async Task<bool> CheckPermissions(Permission permission)
            {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            bool request = false;
            if (permissionStatus == PermissionStatus.Denied)
                {
                if (Device.RuntimePlatform == Device.iOS)
                    {

                    var title = $"Permissões de Armazenamento";
                    var question = $"Para poder tirar fotos a permissão de Armazenamento é necessária.";
                    var positive = "Definiçoes";
                    var negative = "Mais tarde";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

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
                    var title = $"Permissões de Armazenamento";
                    var question = $"Para poder tirar fotos a permissão de Armazenamento é necessária.";
                    var positive = "Definiçoes";
                    var negative = "Mais tarde";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

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



        async void FotoClicked(object sender, EventArgs e)


            {


            var haspermission = await CheckPermissions(Permission.Storage);

            if (haspermission)
                {

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                    Directory = "Inventario",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.Small,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Front,
                    Name = Guid.NewGuid().ToString().Substring(0, 8) + ".jpg"
                    });


                if (file == null)
                    return;

                CaminhoFoto.Text = file.AlbumPath.Replace("/storage/emulated/0/Pictures/Inventario/", "");


                Imagem.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();

                    // file.Dispose();
                    return stream;
                });

                ImagemLabel.IsVisible = true;
                Imagem.IsVisible = true;

                }
            }
        }
    }

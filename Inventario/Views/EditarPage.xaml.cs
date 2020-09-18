using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Inventario.Services;
using LiteDB;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Inventario.Views
{
    public partial class EditarPage : ContentPage
    {
        string IDGlobal;
        string PlaquetaGlobal;
        public EditarPage(string ID)
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
            var inventarios = _dbInventario.FindAll();
            var query = (from x in inventarios where x.ID == int.Parse(IDGlobal) select x).FirstOrDefault();
            ID.Text = query.ID.ToString();
            ID.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Picker));
            if (query.CaminhoFoto == "-----")
            {
                ImagemLabel.IsVisible = false;
                Imagem.IsVisible = false;
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Imagem.Source = ImageSource.FromFile(Path.Combine(path, query.CaminhoFoto));
            Origem.Text = query.Estabelecimento.ToString();

            Plaqueta.Text = query.Plaqueta.ToString();
            PlaquetaGlobal = query.Plaqueta.ToString();
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
        async private void GravarClicked(object sender, EventArgs e)
        {
            Models.InventarioEditar inventarioeditar = new Models.InventarioEditar();
            inventarioeditar.idUsuario = Preferences.Get("idUsuario", "default_value");
            inventarioeditar.dtUpdate = DateTime.Now.ToString();
            inventarioeditar.ID = int.Parse(ID.Text);
            inventarioeditar.Estabelecimento = Origem.Text;
            inventarioeditar.Plaqueta = Plaqueta.Text;
            inventarioeditar.DescOriginal = DescOriginal.Text;
            inventarioeditar.DescAccount = DescAccount.Text;
            inventarioeditar.Estabelecimento1 = OrigemAccount.Text;
            inventarioeditar.Setor = Setor.Text;
            inventarioeditar.Setor1 = SetorAccount.Text;
            inventarioeditar.Localidade = Localidade.Text;
            inventarioeditar.Localidade1 = LocalidadeAccount.Text; ;
            inventarioeditar.CaminhoFoto = CaminhoFoto.Text;
            inventarioeditar.NroSerie = NroSerie.Text;
            inventarioeditar.Marca = Marca.Text;
            inventarioeditar.Modelo = Modelo.Text;
            inventarioeditar.Obs = Obs.Text;
            LiteDatabase _dataBase;
            LiteCollection<Models.InventarioEditar> _dbInventarioEditar;
            _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Inventario.db"));
            _dbInventarioEditar = _dataBase.GetCollection<Models.InventarioEditar>();
            _dbInventarioEditar.Upsert(inventarioeditar);
            await DisplayAlert("Editar", "Registro atualizado com sucesso", "OK");
            Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new RegistrarPage()) };
        }
        async private void LimparClicked(object sender, EventArgs e)
        {
            Origem.Text = "";
            //  Plaqueta.Text = "";
            //  DescOriginal.Text = "";
            DescAccount.Text = "";
            OrigemAccount.Text = "";
            // Setor.Text = "";
            SetorAccount.Text = "";
            // Localidade.Text = "";
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
        async private void FotoClicked(object sender, EventArgs e)
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
                if (file == null) return;
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

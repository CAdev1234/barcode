using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Inventario.Models;
using Xamarin.Forms;

namespace Inventario.Views
    {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
        {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
            {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);
            }

        public async Task NavigateFromMenu(int id)
            {
            if (!MenuPages.ContainsKey(id))
                {
                switch (id)
                    {

                    case (int)MenuItemType.Home:
                        MenuPages.Add(id, new NavigationPage(new HomePage()));
                        break;

                    case (int)MenuItemType.Usuario:
                        MenuPages.Add(id, new NavigationPage(new UsuarioPage()));
                        break;


                    case (int)MenuItemType.Registrar:
                        MenuPages.Add(id, new NavigationPage(new RegistrarPage()));
                        break;

                    case (int)MenuItemType.Consultar:
                        MenuPages.Add(id, new NavigationPage(new InventariosPage("")));
                        break;

                    case (int)MenuItemType.Transferir:
                        MenuPages.Add(id, new NavigationPage(new TransferirPage()));
                        break;


                    }
                }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
                {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
                }
            }
        }
    }
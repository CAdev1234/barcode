using System.Collections.Generic;
using System.ComponentModel;
using Inventario.Models;
using Xamarin.Forms;

namespace Inventario.Views
    {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
        {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
            {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Home, Title="Início" },
                new HomeMenuItem {Id = MenuItemType.Usuario, Title="Configurações" },
                new HomeMenuItem {Id = MenuItemType.Registrar, Title="Registrar" },
                new HomeMenuItem {Id = MenuItemType.Consultar, Title="Consultar" },
                new HomeMenuItem {Id = MenuItemType.Transferir, Title="Transferir" },

            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
            }
        }
    }
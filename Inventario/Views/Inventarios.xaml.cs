using System.ComponentModel;
using Inventario.ViewModels;
using Xamarin.Forms;

namespace Inventario.Views
    {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class InventariosPage : ContentPage
        {
        InventariosViewModel viewModel;

        string TermoGlobal;


        public InventariosPage(string Termo)
            {
            InitializeComponent();
            TermoGlobal = Termo;



            BindingContext = viewModel = new InventariosViewModel(TermoGlobal);

            viewModel.PropertyChanged += delegate
                {

                    if (Device.RuntimePlatform == Device.Android && viewModel.Inventarios.Count > 0)


                        {

                        InventariosListView.ScrollTo(viewModel.Inventarios[0], ScrollToPosition.Start, false);


                        }


                    };
                }

        async void OnInventarioSelected(object sender, SelectedItemChangedEventArgs args)
            {
            var inventario = args.SelectedItem as Models.Inventario;
            if (inventario == null)
                return;

            await Navigation.PushAsync(new InventarioDetailPage(inventario.ID.ToString()));

            // Manually deselect item.
            InventariosListView.SelectedItem = null;
            }

       

        protected override void OnAppearing()
            {
            base.OnAppearing();


            Termo.Text = TermoGlobal;


            if (viewModel.Inventarios.Count == 0)
                viewModel.LoadInventariosCommand.Execute(null);
            }

        void PesquisarClicked(System.Object sender, System.EventArgs e)
            {


            Navigation.PopAsync();

            Application.Current.MainPage = new MainPage { Detail = new NavigationPage(new InventariosPage(Termo.Text) )};

            }
        }
    }
using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Inventario.Views;
using LiteDB;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace Inventario
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //   DependencyService.Register<MockDataStore>();
            #if DEBUG
            HotReloader.Current.Run(this);
            #endif
            MainPage = new LoaderPage();
        }
        protected override void OnStart()
        {
            AppCenter.Start("android=0ce43a32-d903-4c3e-8de4-7b528191b861;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}

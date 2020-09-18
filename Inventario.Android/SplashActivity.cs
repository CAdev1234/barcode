
using Android.App;
using Android.Content;

using Android.OS;

using System.Threading.Tasks;
using Android.Content.PM;
using Inventario.Droid;

namespace Hayashi.Droid
    {

    [Activity(Label = "Inventario", Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, NoHistory = true)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
        {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
            {
            base.OnCreate(savedInstanceState, persistentState);
          
         
         
            }

       

        // Launches the startup task
        protected override void OnStart()
            {


            base.OnStart();
       

            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
            }

      

       


        void SimulateStartup()
            {

            // await Task.Delay(5000); 

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }


            }



    }


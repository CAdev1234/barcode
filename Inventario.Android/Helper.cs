using System;
using System.IO;
using Inventario.Droid;
using Xamarin.Forms;
[assembly: Dependency(typeof(Helper))]
namespace Inventario.Droid
    {
    public class Helper : IHelper
        {
        public string GetFilePath(string file)
            {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, file);
            }
        }
    }
 using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Inventario.Models;
using Inventario.Views;
using System.Windows.Input;
using System.Data.SqlClient;
using LiteDB;
using System.Linq;
using System.Collections.Generic;

namespace Inventario.ViewModels
{
    public class InventariosViewModel : BaseViewModel
    {
        string TermoGlobal;
        private int _myResultados;
        public int MyResultados
        {
            get
            {
                return _myResultados;
            }
            set
            {
                if (_myResultados != value)
                {
                    _myResultados = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _myFlag;
        public bool MyFlag
        {
            get
            {
                return _myFlag;
            }
            set
            {
                if (_myFlag != value)
                {
                    _myFlag = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _myFlag2;
        public bool MyFlag2
        {
            get
            {
                return _myFlag2;
            }
            set
            {
                if (_myFlag2 != value)
                {
                    _myFlag2 = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ponteiro = 0;
        public int pagina = 25;
        public int contador = 0;

        public ObservableCollection<Models.Inventario> Inventarios { get; set; }
        public Command LoadInventariosCommand { get; set; }
        public InventariosViewModel(string Termo)
        {
            Title = "Consultar";
            Inventarios = new ObservableCollection<Models.Inventario>();
            LoadInventariosCommand = new Command(async () => await  ExecuteLoadInventariosCommand());
            TermoGlobal = Termo;
        }
        public ICommand LoadMore
        {
            get
            {
                return new Command(async () =>
                {
                    await ExecuteLoadInventariosCommand();
                });
            }
        }

        public ICommand Recarregar
        {
            get
            {
                return new Command(async () =>
                {
                    ponteiro = 0;
                    await ExecuteLoadInventariosCommand();
                });
            }
        }

        async Task ExecuteLoadInventariosCommand()
        {
            if (IsBusy) return;
            IsBusy = true;
            Inventarios.Clear();
            try
            {
                LiteDatabase _dataBase;
                LiteCollection<Models.Inventario> _dbInventario;
                _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Inventario.db"));
                _dbInventario = _dataBase.GetCollection<Models.Inventario>();
                var inventarios = _dbInventario.FindAll();
                var query = (from x in inventarios select x).ToList();
                MyResultados = query.Count;
                if (!string.IsNullOrEmpty(TermoGlobal))
                {
                    MyResultados = (from x in query where x.Plaqueta.ToUpper().Contains(TermoGlobal.ToUpper()) || x.DescOriginal.ToUpper().Contains(TermoGlobal.ToUpper()) || x.DescAccount.ToUpper().Contains(TermoGlobal.ToUpper()) select x).Count();
                    query = (from x in query where x.Plaqueta.ToUpper().Contains(TermoGlobal.ToUpper()) || x.DescOriginal.ToUpper().Contains(TermoGlobal.ToUpper()) || x.DescAccount.ToUpper().Contains(TermoGlobal.ToUpper()) select x).Skip(ponteiro).Take(pagina).ToList();
                }
                else
                {
                    query = (from x in query select x).Skip(ponteiro).Take(pagina).ToList();
                }
                for (int i = 0; i < query.Count(); i++)
                {
                    Models.Inventario inventario = new Models.Inventario();
                    inventario.ID = query[i].ID;
                    inventario.Plaqueta = query[i].Plaqueta;
                    inventario.DescOriginal = query[i].DescOriginal;
                    Inventarios.Add(inventario);
                    contador++;
                }
                if (Inventarios.Count < pagina || contador == MyResultados)
                {
                    MyFlag = true;
                }
                else
                {
                    MyFlag = false;
                }
                MyFlag2 = !MyFlag;
                ponteiro = ponteiro + pagina;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Sem Internet ou Banco de dados não disponível.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
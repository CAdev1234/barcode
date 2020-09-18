using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.Models
    {
    public enum MenuItemType
        {
        Home,
        Registrar,
        Consultar,
        Transferir,
        Usuario
     


        }
    public class HomeMenuItem
        {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
        }
    }

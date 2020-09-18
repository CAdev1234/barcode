using System;

namespace Inventario.Models
    {
    public class Inventario
        {

       
        public int ID { get; set; }
        public string ID_Planilha { get; set; }
        public string idUsuario { get; set; }
        public string iContagem { get; set; }
        public string Empresa { get; set; }
        public string Estabelecimento { get; set; }
        public string Estabelecimento1 { get; set; }
        public string Plaqueta { get; set; }
        public string DescOriginal { get; set; }
        public string DescAccount { get; set; }
        public string DescAccount2 { get; set; }
        public string Cidade { get; set; }
        public string Unidade { get; set; }
        public string Setor { get; set; }
        public string Setor1 { get; set; }
        public string Localidade { get; set; }
        public string Localidade1 { get; set; }
        public string CaminhoFoto { get; set; }
        public string NroSerie { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Quantidade { get; set; }
        public string dtInsert { get; set; }
        public string dtUpdate { get; set; }
        public string flagNovoRegistro { get; set; }
        public string flagBaixar { get; set; }
        public string Obs { get; set; }
       
        }
    }
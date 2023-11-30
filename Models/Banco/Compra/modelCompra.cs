using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AspViagens.Models.Banco
{
    public class modelCompra
    {
        [DisplayName("Código Compra")]
        public string idComp { get; set; }

        [DisplayName("Data da Compra")]
        public string dtComp { get; set; }

        [DisplayName("Código do Usuário")]
        public string idCliente { get; set; }

        public string DadoModel1 { get; set; }

        public List<modelPacote> PlanosPedidos = new List<modelPacote>(); 
    }
}
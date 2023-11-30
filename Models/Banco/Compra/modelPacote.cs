using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AspViagens.Models.Banco
{
    public class modelPacote
    {
        public Guid PlanoPedidoID { get; set; }

        [DisplayName("Código Compra")]
        public string idComp { get; set;}

        [DisplayName("Código Pacote")]
        public string idPacote { get; set;}

        [DisplayName("Pacote")]
        public string nomePacote { get; set;}

        [DisplayName("Imagem")]
        public string imagem { get; set;}

        public string dsPreco { get; set;}
    }
}
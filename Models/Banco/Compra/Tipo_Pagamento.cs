using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspViagens.Models.Banco
{
    public class Tipo_Pagamento
    {
        [Key]
        [DisplayName("Código do Pagamento")]
        public int id_Pagamento { get; set; }

        [DisplayName("Código do Cliente")]
        public string idCliente { get; set; }

        [DisplayName("Forma de Pagamento")]
        public string nm_Pagamento { get; set; }
    }
}
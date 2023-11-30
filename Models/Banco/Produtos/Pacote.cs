using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspViagens.Models.Banco
{
    public class Pacote
    {
        [DisplayName("Código do Pacote")]
        public string IdPacote { get; set; }

        [StringLength(250, ErrorMessage = "Maximo de 250 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar o nome do Pacote")]
        [DisplayName("Pacote")]
        public string nmPacote { get; set; }

        [StringLength(250, ErrorMessage = "Maximo de 250 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar um Preço")]
        [DisplayName("Preço")]
        public string dsPreco { get; set; }

        [StringLength(250, ErrorMessage = "Maximo de 250 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar a Descrição do Pacote")]
        [DisplayName("Descrição")]
        public string dsPacote { get; set; }

        [DisplayName("Imagem")]
        public string Imagem { get; set; }

        public string DataComp { get; set; }

        public double Valores { get; set; }

    }
}
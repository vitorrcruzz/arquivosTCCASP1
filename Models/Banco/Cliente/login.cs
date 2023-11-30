using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspViagens.Models
{
    public class login
    {
        [Key]
        [DisplayName("Código")]
        public string idCliente { get; set; } 

        [Required(ErrorMessage = "Obrigátório informar o Login")]
        [Display(Name = "Login")]
        [StringLength(30, ErrorMessage = "Maximo de 30 Caracteres")]
        [DisplayName("Login")]
        public string dsLogin { get; set; }

        [Required(ErrorMessage = "Obrigátório informar a Senha")]
        [Display(Name = "Senha")]
        [StringLength(8, ErrorMessage = "Maximo de 8 Caracteres")]
        [DisplayName("Senha")]
        public string dsSenha { get; set; }

        [Required(ErrorMessage = "Obrigátório informar o Tipo")]
        [DisplayName("Tipo")]
        public string tipo { get; set; }
    }
}
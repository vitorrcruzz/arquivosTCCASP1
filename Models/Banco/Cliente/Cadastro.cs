using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspViagens.Models
{
    public class Cadastro
    {

        [Key]
        [DisplayName("Código")]
        public int id { get; set; }

        [Required(ErrorMessage = "Obrigátório informar o Tipo")]
        [DisplayName("Tipo")]
        public string tipo { get; set; }

        [Required(ErrorMessage = "Obrigátório informar o Nome")]
        [StringLength(80,ErrorMessage ="Maximo de 80 Caracteres")]
        [DisplayName("Nome Completo")]
        public string nm { get; set; }

        [Required(ErrorMessage = "Obrigátório informar o CPF")]
        [StringLength(14, MinimumLength = 14, ErrorMessage ="Digite um CPF valido")]
        [DisplayName("CPF")]
        public string noCPF { get; set; }
        
        [StringLength(30, ErrorMessage = "Maximo de 30 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar o Login")]
        [DisplayName("Login")]
        public string dsLogin { get; set; }

        [Required(ErrorMessage = "Obrigátório informar a Senha")]
        [StringLength(8, ErrorMessage = "Maximo de 8 Caracteres")]
        [PasswordPropertyText]
        [DisplayName("Senha")]
        public string dsSenha { get; set; }
        
        [DisplayName("Confirme a senha")]
        [Compare("dsSenha", ErrorMessage ="As senhas não são iguais")]
        public string ConfirmaSenha { get; set; }
        

        [Required(ErrorMessage = "Obrigátório informar o Email")]
        [EmailAddress(ErrorMessage = "Informe um Email válido")]
        [StringLength(50, ErrorMessage = "Maximo de 50 Caracteres")]
        [DisplayName("E-mail")]
        public string dsEmail { get; set; }

        [Required(ErrorMessage = "Obrigátório informar um Telefone")]
        [RegularExpression("^(1[1-9]|2[12478]|3([1-5]|[7-8])|4[1-9]|5(1|[3-5])|6[1-9]|7[134579]|8[1-9]|9[1-9])9[0-9]{8}$", ErrorMessage = "Informe um telefone valido")]
        [DisplayName("Telefone")]
        public string Telefone { get; set; }

    }
}
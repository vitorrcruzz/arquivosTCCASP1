using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspViagens.Models.Banco
{
    public class Plano
    {
        [Key]
        [DisplayName("Código Plano")]
        public string idPlano { get; set; }

        [StringLength(80, ErrorMessage = "Maximo de 80 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar um Nome ")]
        [DisplayName("País")]
        public string nmPais { get; set; }

        public string IdPacote { get; set; }

        [Required(ErrorMessage = "Obrigátório informar o Nome da Cidade")]
        [DisplayName("Cidade")]
        public string nmCidade { get; set; }

        
        [Required(ErrorMessage = "Obrigátório informar a Descrição da cidade")]
        [DisplayName("Descrição")]
        public string dsCidade { get; set; }

        [StringLength(300, ErrorMessage = "Maximo de 300 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar o Clima")]
        [DisplayName("Clima")]
        public string dsClima { get; set;}

        [StringLength(250, ErrorMessage = "Maximo de 250 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar o inicio da estadia")]
        [DataType(DataType.DateTime)]
        [DisplayName("Ida")]
        public string dsEstadiaInicio { get; set;}

        [StringLength(150, ErrorMessage = "Maximo de 150 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar o termino da estadia")]
        [DataType(DataType.DateTime)]
        [DisplayName("Volta")]
        public string dsEstadiaTermino { get; set; }

        [StringLength(150, ErrorMessage = "Maximo de 150 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar o termino da estadia")]
        [DisplayName("Tempo de Estadia")]
        public string dsEstadiaTotal { get; set; }

        [StringLength(150, ErrorMessage = "Maximo de 150 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar a Moeda")]
        [DisplayName("Moeda local")]
        public string dsMoeda { get; set; }

        [StringLength(150, ErrorMessage = "Maximo de 150 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar o idioma Principal")]
        [DisplayName("Idioma Principal")]
        public string dsIdiomaP { get; set; }

        [StringLength(150, ErrorMessage = "Maximo de 150 Caracteres")]
        [Required(ErrorMessage = "Obrigátório informar o idioma Secundario")]
        [DisplayName("Idioma Secundário")]
        public string dsIdiomaS { get; set; }

    }
}
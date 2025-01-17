using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class TarjetaDTO
    {
        [Required(ErrorMessage = "Ingrese Titular")]
        public string? Titular { get; set; }

        [Required(ErrorMessage = "Ingrese Numero de tarjeta")]
        public string? NumeroTarjeta { get; set; }

        [Required(ErrorMessage = "Ingrese Fecha de expiracion")]
        public string? FechaExpiracion { get; set; }

        [Required(ErrorMessage = "Ingrese CVV")]
        public string? CVV { get; set; }
    }
}

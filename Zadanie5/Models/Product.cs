using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Zadanie5.Models;

namespace Zadanie5.Models
{
    public class Product
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Nazwa produktu")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        public string name { get; set; }

        [Display(Name = "Cena")]
        [Range(0, 999999.99, ErrorMessage = "Pole cena musi być pomiędzy 0 i 999999,99")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        public decimal price { get; set; }

    }
}

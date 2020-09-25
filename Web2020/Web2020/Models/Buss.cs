using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web2020.Models
{
    public class Buss
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string reiserFra { get; set; }
        [Required]
        public string reiserTil { get; set; }
        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime tidspunkt { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [StringLength(20, ErrorMessage = "{0} lengden må være mellom {2} og {1}.", MinimumLength = 2)]
        public string fornavn { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [StringLength(20, ErrorMessage = "{0} lengden må være mellom {2} og {1}.", MinimumLength = 2)]
        public string etternavn { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9]*$")]
        [StringLength(50, ErrorMessage = "{0} lengden må være mellom {2} og {1}.", MinimumLength = 2)]
        public string adresse { get; set; }
        [RegularExpression(@"^\w+@\w+\.\w{2,3}$")] 
        [StringLength(20, ErrorMessage = "{0} lengden må være mellom {2} og {1}.", MinimumLength = 2)]
        public string epost { get; set; }
        public string telefonnr { get; set; }
    }
}

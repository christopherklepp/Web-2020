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
        [StringLength(20)]
        public string fornavn { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [StringLength(20)]
        public string etternavn { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9]*$")]
        [StringLength(20)]
        public string adresse { get; set; }
        /*[RegularExpression(@"^[2-9]\d{7}*$")]
        [StringLength(20)]*/
        public string telefonnr { get; set; }
        public double pris { get; set; }
    }
}

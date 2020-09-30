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
        [Display(Name = "Tidspunkt")]
        [DataType(DataType.Date)]
        public DateTime tidspunkt { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [StringLength(20)]
        public string fornavn { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [StringLength(20)]
        public string etternavn { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        public string epost { get; set; }
        public double pris { get; set; }

    }
}

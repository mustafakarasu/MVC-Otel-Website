using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCOtel_2.Models
{
    [Table("tblOdalar")]
    public class OdaModel
    {
        [Key]
        public int OdaModelID { get; set; }
        public string Baslik { get; set; }
        public decimal Fiyat { get; set; }
        public int m2 { get; set; }
        public string ResimURL { get; set; }
    }
}

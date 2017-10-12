using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCOtel_2.Models
{
    public class RezervasyonModel
    {
        [Key]
        public int RezervasyonModelID { get; set; }
        public string UserID { get; set; }
        public int OdaID { get; set; }        
        public DateTime GirisTarih { get; set; }
        public DateTime CikisTarih { get; set; }
        public decimal Fiyat { get; set; }
        public int KacKisi { get; set; }

    }
}

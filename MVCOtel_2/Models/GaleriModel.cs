using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCOtel_2.Models
{
    public class GaleriModel
    {
        [Key]
        public int GaleriModelID { get; set; }
        public string ResimYoluGaleri { get; set; }
        public string BaslikGaleri { get; set; }
    }
}

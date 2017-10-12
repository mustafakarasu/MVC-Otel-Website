using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCOtel_2.Models
{
    public class SliderModel
    {
        [Key]
        public int SliderModelID { get; set; }

        [Required]
        public string ResimYolu { get; set; }
        public int Sira { get; set; }
    }
}

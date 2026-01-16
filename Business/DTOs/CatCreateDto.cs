using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CatCreateDto
    { 
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [MaxLength(50, ErrorMessage = "Kategori adı 50 karakterden uzun olamaz.")]
        public string CName { get; set; }
    }
}

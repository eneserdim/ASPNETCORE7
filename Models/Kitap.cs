using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core_Web_Project.Models
{
    public class Kitap
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Kitap Adı")]
        public string KitapAdi { get; set; }

        [DisplayName("Tanım")]
        public string Tanim { get; set; }

        [Required]
        [DisplayName("Yazar")]
        public string Yazar { get; set; }

        [Required]
        [Range(10, 5000)] //Kitap Fiyatı Min 10₺ Max 5000₺ olabilir Range -> Aralık
        [DisplayName("Kitap Fiyatı")]
        public double Fiyat { get; set; }

        [ValidateNever] //Kontrol yapmaması için ekliyoruz
        public int KitapTuruId { get; set; }
        [ForeignKey("KitapTuruId")]

        [ValidateNever] //Kontrol yapmaması için ekliyoruz
        public KitapTuru KitapTuru { get; set;}

        
        [ValidateNever] //Kontrol yapmaması için ekliyoruz
        public string ResimUrl { get; set; }
    }
}

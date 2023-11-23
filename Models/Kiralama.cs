using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core_Web_Project.Models
{
    public class Kiralama
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OgrenciId { get; set; }

        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }

        [ValidateNever] //Kontrol yapmaması için ekliyoruz
        public int KitapId { get; set; }
        [ForeignKey("KitapId")]

        [ValidateNever] //Kontrol yapmaması için ekliyoruz
        public Kitap Kitap { get; set; }
    }
}

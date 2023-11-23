using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core_Web_Project.Models
{
    public class KitapTuru
    {
        [Key] //primary key
        public int Id { get; set; }


        [Required(ErrorMessage ="Hata! Lütfen Tekrar Deneyiniz...")] //not null,
        [MaxLength(30)] //Max Karakter Sayısı
        [DisplayName("Kitap Türü Adı")]
        public string Ad { get; set; }
    }
}

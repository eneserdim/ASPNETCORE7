namespace Core_Web_Project.Models
{
    public interface IKiralamaRepository : IRepository<Kiralama>
    {
        void Guncelle(Kiralama kiralama);
        void Kaydet();
    }
}

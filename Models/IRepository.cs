using System.Linq.Expressions;

namespace Core_Web_Project.Models
{
    public interface IRepository <T> where T : class
    {
        // T -> KitapTuru Generic
        IEnumerable<T> GetAll (string? IncludeProps = null);
        T Get(Expression<Func<T, bool>> filtre, string? IncludeProps = null);
        void Ekle(T entity);
        void Sil(T entity);
        void SilAralik(IEnumerable<T> entities);

    }
}

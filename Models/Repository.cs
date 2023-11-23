using Core_Web_Project.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core_Web_Project.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        internal DbSet<T> dbSet;  //dbSet => _uygulamaDbContext.KitapTurleri

        public Repository(UygulamaDbContext uygulamaDbContext)
        {
          _uygulamaDbContext = uygulamaDbContext;
            this.dbSet = _uygulamaDbContext.Set<T>();

            _uygulamaDbContext.Kitaplar.Include(k => k.KitapTuru).Include(k => k.KitapTuruId); //Foreign Key'de id yerine metinsel verileri getirmek için kullanılır
            _uygulamaDbContext.Kiralamalar.Include(k => k.Kitap).Include(k => k.KitapId);

        }

        public void Ekle(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filtre, string? IncludeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
            sorgu = sorgu.Where(filtre);
            if (!string.IsNullOrEmpty(IncludeProps))
            {
                foreach (var includeProp in IncludeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }
            return sorgu.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? IncludeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
            if (!string.IsNullOrEmpty(IncludeProps))
            {
                foreach (var includeProp in IncludeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }
            return sorgu.ToList();
        }

        public void Sil(T entity)
        {
           dbSet.Remove(entity);
        }

        public void SilAralik(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities); // Verilen aralığı siler
        }
    }
}

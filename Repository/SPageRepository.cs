using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebCrawlerWPF.Models;

namespace WebCrawlerWPF.Repository
{
  public  class SPageRepository: ISPageRepository
    {

        DbContext _context;
        DbSet<SPage> _dbSet;

        public SPageRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<SPage>();
        }
        public void Delete(SPage entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public IEnumerable<SPage> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public List<SPage> GetAll(Expression<Func<SPage, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public SPage GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public void Insert(SPage entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Update(SPage entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
    
}

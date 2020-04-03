using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WebCrawlerWPF.Models;

namespace WebCrawlerWPF.Repository
{
    public class SPageRepositoryProxy : ISPageRepository
    {

        List<SPage> pages;
        SPageRepository sPageRepository;
        DbContext _context;
       // DbSet<SPage> _dbSet;

        public SPageRepositoryProxy(DbContext context)
        {
            _context = context;
            pages = new List<SPage>();
            //  _dbSet = context.Set<SPage>();
        }
        public SPage GetById(int id)
        {
            SPage page = pages.FirstOrDefault(p => p.Id == id);

            if (page == null)
            {
                if (sPageRepository == null)
                    sPageRepository = new SPageRepository(_context);
                page = sPageRepository.GetById(id);
                pages.Add(page);
            }
            return page;
        }

        public void Delete(SPage entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SPage> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<SPage> GetAll(Expression<Func<SPage, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public void Insert(SPage entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SPage entity)
        {
            throw new NotImplementedException();
        }
    }
}

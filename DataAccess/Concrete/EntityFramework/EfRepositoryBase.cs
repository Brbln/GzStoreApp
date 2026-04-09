using DataAccess.Abstract;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRepositoryBase<Tentity> : IEntityRepository<Tentity>
        where Tentity : class,IEntity,new()
    {
        protected readonly GamzeDbContext _context;
        public EfRepositoryBase(GamzeDbContext context)
        {
            _context = context;
        }
        public void Add(Tentity entity)
        {
            _context.Set<Tentity>().Add(entity);
            _context.SaveChanges();            
        }

        public void Delete(Tentity entity)
        {
            _context.Set<Tentity>().Remove(entity);
            _context.SaveChanges();            
        }

        public Tentity Get(Expression<Func<Tentity, bool>> filter)
        { 
                return _context.Set<Tentity>().SingleOrDefault(filter);
        }

        public List<Tentity> GetAll(Expression<Func<Tentity, bool>> filter = null)
        { 
                return filter == null ? _context.Set<Tentity>().ToList() :
                    _context.Set<Tentity>().Where(filter).ToList();
        }

        public void Update(Tentity entity)
        {
            _context.Set<Tentity>().Update(entity);
            _context.SaveChanges();
            
        }
    }
}

using Repository_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Pattern
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        ICollection<TEntity> GetAll();
        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(TKey id);
        TKey Insert(TEntity model);
        bool Update(TEntity model);
        bool Delete(TKey id);
    }
}

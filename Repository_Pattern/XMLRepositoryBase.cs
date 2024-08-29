using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Repository_Domain;
using Repository_Source;

namespace Repository_Pattern
{

    public class XMLRepositoryBase<TContext, TEntity, TKey> :IRepository<TEntity,TKey> where TContext : XMLSet<TEntity> where TEntity : class
    {
        protected XMLSet<TEntity> m_context;
        public XMLRepositoryBase(string fileName)
        {
            m_context = new XMLSet<TEntity>(fileName);
        }
        public bool Delete(TKey id)
        {
            try
            {
                List<IEntity<TKey>> items = m_context.Data as List<IEntity<TKey>>;
                items.Remove(items.First(f => f.ProductId.Equals(id)));
                m_context.Data = items as ICollection<TEntity>;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var list = m_context.Data.AsQueryable().Where(predicate).ToList();
                return list as ICollection<TEntity>;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public TEntity Get(TKey id)
        {
            try
            {
                List<IEntity<TKey>> items = m_context.Data as List<IEntity<TKey>>;
                return items.FirstOrDefault(f => f.ProductId.Equals(id)) as TEntity;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public ICollection<TEntity> GetAll()
        {
            return m_context.Data;
        }
        public TKey Insert(TEntity model)
        {
            var list = m_context.Data;
            dynamic entityWithKeyId = model;

            TKey maxId = list.Count > 0 ? list.Max(item => ((dynamic)item).ProductId) : default(TKey);

            TKey newEntityId = GetNextAvailableId(maxId);

            entityWithKeyId.ProductId = newEntityId;

            list.Add(model);
            m_context.Data = list;

            return newEntityId;
        }

        private TKey GetNextAvailableId(TKey currentMaxId)
        {
            if (EqualityComparer<TKey>.Default.Equals(currentMaxId, default(TKey)))
            {
                return (TKey)(object)1;
            }
            else
            {
                return (TKey)(object)(((dynamic)currentMaxId) + 1);
            }
        }
        public bool Remove(TKey id)
        {
            return Delete(id);
        }
        public bool Update(TEntity model)
        {
            try
            {
                IEntity<TKey> imodel = model as IEntity<TKey>;
                List<IEntity<TKey>> items = m_context.Data as List<IEntity<TKey>>;
                items.Remove(items.FirstOrDefault(f =>
                f.ProductId.Equals(imodel.ProductId)));
                items.Add(imodel);
                m_context.Data = items as ICollection<TEntity>;
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void Save()
        {
            m_context.Save();
        }
        public void Load()
        {
            m_context.Load();
        }
    }
}

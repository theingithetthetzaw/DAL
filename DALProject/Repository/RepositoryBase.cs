using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Repository
{
    public abstract class RepositoryBase<T> where T : class, new()
    {

        public DBContext entityContext;
        protected abstract T AddEntity(DBContext entityContext, T entity);
        protected abstract T AddOrUpdateEntity(DBContext entityContext, T entity);
        protected abstract IQueryable<T> GetEntities();
        protected abstract IQueryable<T> GetEntitiesWithoutTracking();
        protected abstract T GetEntity(DBContext entityContext, int id);
        protected abstract T UpdateEntity(DBContext entityContext, T entity);

       


        public int Add(T entity)
        {
            int result = 0;
            using (DBContext entityContext = new DBContext())
            {
                var obj = AddEntity(entityContext, entity);
                result = entityContext.SaveChanges();

            }
            return result;
        }
        public int InsertList(List<T> entities)
        {

            using (DBContext entityContext = new DBContext())
            {

                for (int i = 0; i < entities.Count(); i++)
                {
                    entityContext.Set<T>().Add(entities[i]);
                }
                return entityContext.SaveChanges();
            }

        }

        public int updateMultipleRecords(List<T> entities)
        {
            using (DBContext entityContext = new DBContext())
            {

                for (int i = 0; i < entities.Count(); i++)
                {
                    entityContext.Entry(entities[i]).State = EntityState.Modified;
                }
                return entityContext.SaveChanges();
            }

        }



        public T AddWithGetObj(T entity)
        {
            try
            {
                using (DBContext entityContext = new DBContext())
                {
                    var obj = AddEntity(entityContext, entity);
                    if (entityContext.SaveChanges() > 0)
                    {
                        return obj;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public T UpdatewithObj(T entity)
        {
            using (DBContext entityContext = new DBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Modified;
                if (entityContext.SaveChanges() > 0)
                {
                    return entity;
                }
                return null;
            }
        }

        public T AddGetObj(T entity)
        {

            using (DBContext entityContext = new DBContext())
            {
                var obj = AddEntity(entityContext, entity);

                return obj;

            }
            return null;
        }

        public T Insert(T entity)
        {

            using (DBContext entityContext = new DBContext())
            {
                var obj = AddEntity(entityContext, entity);

                return obj;

            }
            return null;
        }

        public int Remove(T entity)
        {
            using (DBContext entityContext = new DBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                return entityContext.SaveChanges();
            }
        }

        public int Remove(int id)
        {
            using (DBContext entityContext = new DBContext())
            {
                T entity = GetEntity(entityContext, id);
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                return entityContext.SaveChanges();
            }
        }

        public int Update(T entity)
        {
            using (DBContext entityContext = new DBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Modified;
                return entityContext.SaveChanges();
            }
        }
      

        public T UpdateComplete(T OldEntity, T NewEntity)
        {
            entityContext.Entry(OldEntity).State = EntityState.Detached;
            entityContext.Set<T>().Attach(NewEntity);
            entityContext.Entry(NewEntity).State = EntityState.Modified;
            try
            {
                var result = entityContext.SaveChanges();
                if (result > 0)
                {
                    return NewEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                //  ex = ex.StackTrace();
                return null;
            }


        }


        public IQueryable<T> Get()
        {
            return GetEntities();
        }
        public IQueryable<T> GetWithoutTracking()
        {
            return GetEntitiesWithoutTracking();
        }

        public T Get(int id)
        {
            using (DBContext entityContext = new DBContext())
            {
                return GetEntity(entityContext, id);
            }
        }


    }
}
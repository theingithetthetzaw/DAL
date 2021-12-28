using Repository;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DALProject.Repository
{
    public class PersonRepository : RepositoryBase<tbCustomer>
    {
        public PersonRepository()
        {
            this.entityContext = new DBContext();
        }
        public PersonRepository(DBContext context)
        {
            this.entityContext = context;
        }
        protected override tbCustomer AddEntity(DBContext entityContext, tbCustomer entity)
        {
            return entityContext.tbcustomer.Add(entity);
        }
        protected override tbCustomer AddOrUpdateEntity(DBContext entityContext, tbCustomer entity)
        {
            if (entity.PersonID == default(int))
            {
                return entityContext.tbcustomer.Add(entity);
            }
            else
            {

                return entityContext.tbcustomer.FirstOrDefault(e => e.PersonID == entity.PersonID);
            }
        }

        protected override tbCustomer UpdateEntity(DBContext entityContext, tbCustomer entity)
        {
            return entityContext.tbcustomer.FirstOrDefault(e => e.PersonID == entity.PersonID);

        }

        protected override IQueryable<tbCustomer> GetEntities()
        {
            return entityContext.tbcustomer.AsQueryable();
        }
        protected override IQueryable<tbCustomer> GetEntitiesWithoutTracking()
        {
            return entityContext.tbcustomer.AsNoTracking().AsQueryable();
        }

        protected override tbCustomer GetEntity(DBContext entityContext, int id)
        {
            return entityContext.tbcustomer.FirstOrDefault(e => e.PersonID == id);
        }

    }
}

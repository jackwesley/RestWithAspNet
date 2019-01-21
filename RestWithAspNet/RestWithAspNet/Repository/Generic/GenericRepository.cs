using Microsoft.EntityFrameworkCore;
using RestWithAspNet.Model.Base;
using RestWithAspNet.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MySqlContext _context;
        private DbSet<T> dataset;
        public GenericRepository(MySqlContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }
        public T Create(T obj)
        {
            try
            {
                dataset.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public void Delete(long id)
        {

            var result = dataset.SingleOrDefault(x => x.Id.Equals(id));
            try
            {
                if (result != null)
                    dataset.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(long? id)
        {
            return id == null ? false : dataset.Any(x => x.Id == id);
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(x => x.Id == id);
        }

        public T Update(T obj)
        {
            if (!Exists(obj.Id)) return null;

            var result = dataset.SingleOrDefault(x => x.Id.Equals(obj.Id));
            try
            {
                _context.Entry(result).CurrentValues.SetValues(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return obj;
        }
    }
}

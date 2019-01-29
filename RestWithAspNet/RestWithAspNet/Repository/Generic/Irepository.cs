using RestWithAspNet.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T obj);
        T FindById(long id);
        List<T> FindAll();
        T Update(T obj);
        void Delete(long id);
        bool Exists(long? id);

        List<T> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}

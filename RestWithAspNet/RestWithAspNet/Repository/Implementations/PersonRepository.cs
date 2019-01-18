using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;

namespace RestWithAspNet.Repository.Implementations
{
    public class PersonRepository : IPersonRepository
    {
        private MySqlContext _context;

        public PersonRepository(MySqlContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person; ;
        }

        public Person FindById(long id)
        {
            try
            {
                return _context.Persons.SingleOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return null;

            var result = _context.Persons.SingleOrDefault(x => x.Id.Equals(person.Id));
            try
            {
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return person;
        }

        public bool Exists(long? id)
        {
            return id == null ? false : _context.Persons.Any(x => x.Id == id);
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public void Delete(long id)
        {

            var result = _context.Persons.SingleOrDefault(x => x.Id.Equals(id));
            try
            {
                if (result != null)
                    _context.Persons.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}

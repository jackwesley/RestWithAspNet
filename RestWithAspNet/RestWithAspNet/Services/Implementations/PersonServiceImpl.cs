using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;

namespace RestWithAspNet.Services.Implementations
{
    public class PersonServiceImpl : IPersonService
    {
        private MySqlContext _context;

        public PersonServiceImpl(MySqlContext context)
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
            if (!Exists(person.Id)) return new Person();

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

        private bool Exists(long? id)
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
                if(result != null)
                _context.Persons.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

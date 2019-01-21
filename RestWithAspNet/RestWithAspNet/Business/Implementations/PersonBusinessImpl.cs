using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;
using RestWithAspNet.Repository;

namespace RestWithAspNet.Business.Implementations
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IPersonRepository _personRepository;

        public PersonBusinessImpl(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Create(Person person)
        {
            return _personRepository.Create(person);
        }

        public Person FindById(long id)
        {

            return _personRepository.FindById(id);
        }

        public Person Update(Person person)
        {
            return _personRepository.Update(person);
        }

        public List<Person> FindAll()
        {
            return _personRepository.FindAll();
        }

        public void Delete(long id)
        {
            _personRepository.Delete(id);
        }
    }
}

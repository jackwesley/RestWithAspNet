using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RestWithAspNet.Data.Converters;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;
using RestWithAspNet.Repository;
using RestWithAspNet.Repository.Generic;

namespace RestWithAspNet.Business.Implementations
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IRepository<Person> _personRepository;
        private readonly PersonConverter _personConverter;

        public PersonBusinessImpl(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
            _personConverter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _personConverter.Parse(person);
            return _personConverter.Parse((_personRepository.Create(personEntity)));
        }

        public PersonVO FindById(long id)
        {

            return _personConverter.Parse(_personRepository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _personConverter.Parse(person);
            return _personConverter.Parse(_personRepository.Update(personEntity));
        }

        public List<PersonVO> FindAll()
        {
            return _personConverter.ParseList(_personRepository.FindAll());
        }

        public void Delete(long id)
        {
            _personRepository.Delete(id);
        }
    }
}

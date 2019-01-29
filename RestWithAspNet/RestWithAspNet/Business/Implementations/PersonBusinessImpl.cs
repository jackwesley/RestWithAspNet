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
using Tapioca.HATEOAS.Utils;

namespace RestWithAspNet.Business.Implementations
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IPersonRepository _personRepository;
        private readonly PersonConverter _personConverter;

        public PersonBusinessImpl(IPersonRepository personRepository)
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

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _personConverter.ParseList(_personRepository.FindByName(firstName, lastName));
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

        public PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;
            string query = $"SELECT * FROM Persons WHERE 1=1";
            if (!string.IsNullOrEmpty(name))
                query = query + $" AND FirstName LIKE '%{name}%'";
           
            query = query + $" ORDER BY FirstName {sortDirection} LIMIT {pageSize} OFFSET {page}";

            string countQuery = $"SELECT * FROM Persons WHERE 1=1";
            if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $" AND FirstName LIKE '%{name}%'";
            var totalResults = _personRepository.GetCount(countQuery);

            var persons = _personConverter.ParseList(_personRepository.FindWithPagedSearch(query));
            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page,
                List = persons,
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };
        }


        public void Delete(long id)
        {
            _personRepository.Delete(id);
        }
    }
}

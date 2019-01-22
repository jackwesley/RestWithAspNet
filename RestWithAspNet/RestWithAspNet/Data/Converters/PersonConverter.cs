using RestWithAspNet.Data.Converter;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Data.Converters
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin != null)
            {
                return new Person
                {
                    Id = origin.Id,
                    FirstName = origin.FirstName,
                    Adress = origin.Adress,
                    Gender = origin.Gender,
                    LastName = origin.LastName
                };
            }

            return null;
        }

        public PersonVO Parse(Person origin)
        {
            if (origin != null)
            {
                return new PersonVO
                {
                    Id = origin.Id,
                    FirstName = origin.FirstName,
                    Adress = origin.Adress,
                    Gender = origin.Gender,
                    LastName = origin.LastName
                };
            }

            return null;
        }

        public List<Person> ParseList(List<PersonVO> origins)
        {
            if (origins != null && origins.Any())
            {
                return origins.Select(item => Parse(item)).ToList();
            }

            return null;

        }

        public List<PersonVO> ParseList(List<Person> origins)
        {
            if (origins != null && origins.Any())
            {
                return origins.Select(item => Parse(item)).ToList();
            }

            return null;
        }
    }
}

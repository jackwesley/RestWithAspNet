using System;
using System.Collections.Generic;
using System.Threading;
using RestWithAspNet.Model;

namespace RestWithAspNet.Services.Implementations
{
    public class PersonServiceImpl : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person; ;
        }

        public Person FindById(long id)
        {
            return new Person
            {
                Id = 1,
                FirstName = "Jack",
                LastName = "Wesley",
                Adress = "Rua Sabino,239 SP Brasil",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for(int i = 1; i < 9; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }

            return persons;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = $"Person Name {i}",
                LastName = $"Person Last Name {i}",
                Adress = "Algum lugar do Brasil",
                Gender = (i%2 == 0) ? "Male" : "Female"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

        public void Delete(long id)
        {
        }
    }
}

using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RestWithAspNet.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindByName(string firstName, string lastName);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        void Delete(long id);
        PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}

using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Business
{
    public interface ILoginBusiness
    {
        object FindByLogin(UserVO user);
    }
}

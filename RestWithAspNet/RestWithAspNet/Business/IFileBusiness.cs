using RestWithAspNet.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Business
{
    public interface IFileBusiness
    {
        Byte[] GetPDFFile();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RestWithAspNet.Data.VO;

namespace RestWithAspNet.Business.Implementations
{
    public class FileBusinessImpl : IFileBusiness
    {
        public byte[] GetPDFFile()
        {
            string path = Directory.GetCurrentDirectory();
            var fullPath = path + "\\Other\\Onde-investir-em-2019.pdf";

            return File.ReadAllBytes(fullPath);
        }
    }
}

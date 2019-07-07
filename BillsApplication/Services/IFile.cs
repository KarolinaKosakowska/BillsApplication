using BillsData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services
{
    public interface IFile
    {
        void Add(File file);
        byte[] SetAttachment(IFormFile formFile);
    }
}

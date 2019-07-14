using BillsApplication.Data;
using BillsData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services
{
    public class FileService : IFile
    {
        private readonly ApplicationDbContext context;

        public FileService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(IFormFile formFile)
        {
            var file = new File();
            file.Attachment = SetAttachment(formFile);
            context.Add(file);
            context.SaveChangesAsync();
        }

        public byte[] SetAttachment(IFormFile formFile)
        {
            using (var memoryStream = new System.IO.MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}

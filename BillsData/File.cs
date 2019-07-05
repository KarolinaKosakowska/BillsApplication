using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BillsData
{
    public class File: BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public int TransactionId { get; set; }

        public byte[] Attachment { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}

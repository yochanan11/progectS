using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace progectS.Models
{
    //ממיר תמונה לבייטים
    public class ParsePhoto
    {
        public byte[] Get(IFormFile file)
        {
            if (file == null) return null;
            MemoryStream stream = new MemoryStream();
            file.CopyTo(stream);
            return stream.ToArray();
        }
    }

}

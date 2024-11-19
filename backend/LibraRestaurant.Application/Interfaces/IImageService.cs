using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadFile(string base64, string fileName, string folder);
    }
}

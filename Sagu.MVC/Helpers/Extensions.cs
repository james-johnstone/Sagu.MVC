using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sagu.MVC
{
    public static class Extensions
    {
        public static string ToBase64(this HttpPostedFileBase upload)
        {
            if (upload == null || upload.ContentLength == 0)
                return string.Empty;

            var fileBytes = new byte[upload.InputStream.Length];
            var uploadedBytes = upload.InputStream.Read(fileBytes, 0, (int) upload.InputStream.Length);

            return Convert.ToBase64String(fileBytes);
        }
    }
}

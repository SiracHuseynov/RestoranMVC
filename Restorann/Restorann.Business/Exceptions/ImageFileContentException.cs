using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorann.Business.Exceptions
{
    public class ImageFileContentException : Exception
    {
        public ImageFileContentException(string? message) : base(message)
        {
        }
    }
}

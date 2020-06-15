using Advensco.ALForsanBE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advensco.ALForsanBE.ViewModel
{
    public class ImageVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Caption { get; set; }
        public List<Gallery> Galleries { get; set; }
        public int GalleryId { get; set; }
    }
}
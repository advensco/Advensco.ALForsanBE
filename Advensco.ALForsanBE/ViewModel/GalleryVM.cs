using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Advensco.ALForsanBE.Models;

namespace Advensco.ALForsanBE.ViewModel
{
    public class GalleryVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Image> Images { get; set; }
    }
}
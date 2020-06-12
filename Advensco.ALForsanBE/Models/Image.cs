using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Advensco.ALForsanBE.Models
{
    public class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }

    }

    public  class ImageVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public HttpPostedFileBase Path { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advensco.ALForsanBE.Models
{
    public class Gallery
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Title { get; set; }
        public List<Image> Images { get; set; }

    }
}
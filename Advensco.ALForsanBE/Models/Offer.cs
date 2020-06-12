using System.ComponentModel.DataAnnotations.Schema;

namespace Advensco.ALForsanBE.Models
{
    public class Offer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public Image CoverImage { get; set; }

    }
}
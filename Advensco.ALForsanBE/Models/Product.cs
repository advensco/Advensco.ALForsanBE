using System.ComponentModel.DataAnnotations.Schema;

namespace Advensco.ALForsanBE.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; }

    }
}
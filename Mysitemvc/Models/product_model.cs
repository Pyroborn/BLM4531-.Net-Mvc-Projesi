using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mysitemvc.Models
{
    public class product_model
    {
        [DisplayName("Id Number")]
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string Description { get; set; }

        public product_model(int value1, string value2, decimal value3, string value4)
        {
            this.Id = value1;
            this.Name = value2;
            this.Price = value3;
            this.Description = value4;
        }
        public product_model()
        {

        }
    }
}

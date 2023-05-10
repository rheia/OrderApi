using System.ComponentModel.DataAnnotations.Schema;

namespace OrderApi.Models
{
    public class RentalItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public decimal? RentalRate { get; set; }

        [ForeignKey("Kind")]
        public int KindId { get; set; }

        public Kind? Kind { get; set; } = null!;

        /*This will be the discriminator column*/
        public int KindTypeId { get; set; }

        public int? RentalItemId { get; set; }

    }

}

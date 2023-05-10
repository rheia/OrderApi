using System.ComponentModel.DataAnnotations.Schema;

namespace OrderApi.Models
{
    public class Rental
    {
        public int Id { get; set; }
        
        public DateTime RentalDate { get; set; }        
        public DateTime? ReturnDate { get; set; }
        public decimal? TotalCost { get; set; }

        [ForeignKey("RentalItem")]
        public int? RentalItemId { get; set; }
        public RentalItem? RentalItem { get; set; }       
        public string? CustomerName { get; set; }
        public string? CustomerPhoneNumber { get; set; }

        [ForeignKey("Kind")]
        public int KindId { get; set; }

        public Kind? Kind { get; set; }

        public int Status { get; set; }

        public string? Damages { get; set; }

        public int? FuelConsumed { get; set; }

    }
}

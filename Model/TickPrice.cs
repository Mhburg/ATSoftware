using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class TickerPrice
    {
        [Key]
        public int Tick_Id { get; set; }
        [Required]
        public int Ticker_Id { get; set; }
        [Required]
        public int Field { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Can_Auto_Execute { get; set; }
        [Required]
        public long TimeStamp { get; set; }
    }
}

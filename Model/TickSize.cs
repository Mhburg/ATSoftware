using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class TickSize
    {
        [Key]
        public int Tick_Id { get; set; }
        [Required]
        public int Ticker_Id { get; set; }
        [Required]
        public int Field { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public long TimeStamp { get; set; }
    }
}

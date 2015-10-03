using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class TickGeneric
    {
        [Key]
        public int Tick_Id { get; set; }
        [Required]
        public int Ticker_Id { get; set; }
        [Required]
        public int Field { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public long TimeStamp { get; set; }
    }
}

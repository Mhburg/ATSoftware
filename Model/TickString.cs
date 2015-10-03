using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class TickString
    {
        [Key]
        public int Tick_Id { get; set; }
        [Required]
        public int Ticker_Id { get; set; }
        [Required]
        public int Tick_Type { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public long TimeStamp { get; set; }
    }
}

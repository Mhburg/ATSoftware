using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class TickerIdRef
    {
        [Key]
        [Column(TypeName = "varchar(5)")]
        public string Symbol { get; set; }
        [Required]
        public int TickerId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CarWebAPI.Models
{
    public class CarMake
    {
        [Range(0, int.MaxValue)]
        public int MakeID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string Type { get; set; }
    }
}

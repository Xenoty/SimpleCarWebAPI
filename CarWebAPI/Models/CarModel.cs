using System.ComponentModel.DataAnnotations;

namespace CarWebAPI.Models
{
    public class CarModel
    {
        [Range(0, int.MaxValue)]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Model { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int MakeID { get; set; }
    }
}

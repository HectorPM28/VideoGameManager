using System.ComponentModel.DataAnnotations;

namespace VideoGameManager.Models
{
    public class Game
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is mandatory")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Genre is mandatory")]
        public string Genre { get; set; } = string.Empty;
        [Range(1970, 2030, ErrorMessage = "Invalida year")]
        public int Year { get; set; }
        [Range(0, 10, ErrorMessage = "Score must be between 0 - 10")]
        public double Score { get; set; }
        public string Description { get; set; } = string.Empty;      
    }

}

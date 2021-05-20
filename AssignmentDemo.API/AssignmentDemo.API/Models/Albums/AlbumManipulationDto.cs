using AssignmentDemo.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentDemo.API.Models.Albums
{
    [AlbumTitleShuldNotbeEmpty(ErrorMessage = "Album Field Can not be Empty")]
    public abstract class AlbumManipulationDto
    {


        [Required(ErrorMessage = "userID must be b/w 1 to 200")]
        [Range(1, 200)]
        public int userId { get; set; }

        [Required(ErrorMessage = "userID must be b/w 1 to 3000")]
        [Range(1, 3000)]
        public int id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Title must be less than 30 char")]
        public string title { get; set; }


    }
}

using AssignmentDemo.API.Models.Albums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentDemo.API.ValidationAttributes
{
    /// <summary>
    /// 
    /// </summary>
    public class AlbumTitleShuldNotbeEmpty : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
          ValidationContext validationContext)
        {
            var album = (AlbumManipulationDto)validationContext.ObjectInstance;

            if (album.title == "")
            {
                return new ValidationResult(ErrorMessage,
                    new[] { nameof(AlbumManipulationDto) });
            }

            return ValidationResult.Success;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Title is too long")]
        [MinLength(3, ErrorMessage = "Title is too short")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(500, ErrorMessage = "Content is too long")]
        [MinLength(3, ErrorMessage = "Content is too short")]
        public string Content { get; set; } = string.Empty;
       
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTutorial.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [DataType(DataType.MultilineText)]
        public string CommentBody { get; set; }

        public Post Post { get; set; }
        public Guid PostId { get; set; }
    }
}

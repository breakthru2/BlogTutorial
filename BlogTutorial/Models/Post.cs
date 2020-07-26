using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTutorial.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }

        [Required]
        [Display(Name = "Post Body")]
        [DataType(DataType.MultilineText)]
        public string PostBody { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Post Image")]
        public string PostImage { get; set; }

        public Category Category { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        public IdentityUser User { get; set; }

        [Required]
        [Display(Name = "User's Name")]
        public string UserId { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}

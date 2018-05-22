using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_InfinityMesh.ViewModels
{
    public class BlogVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field 'Title' must be filled!")]
        [StringLength(64, ErrorMessage = "Field 'Title' can't be bigger than 64 characters!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Field 'Summary' must be filled!")]
        [StringLength(350, ErrorMessage = "Field 'Summary' can't be bigger than 350 characters!")]
        public string Summary { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Field 'Content' must be filled!")]
        [StringLength(3500, ErrorMessage = "Field 'Content' can't be bigger than 64 characters!")]
        public string Content { get; set; }
        [Required(ErrorMessage = "A Publish Date must be chosen!")]
        public DateTime PublishDate { get; set; }

        public int UserId { get; set; }
    }
}
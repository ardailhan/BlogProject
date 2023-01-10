﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.ViewModels.Article.Create
{
    public class CreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Display(Name ="Article Picture")]
        public IFormFile ArticlePicture { get; set; }
    }
}

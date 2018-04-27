using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PlaceUp.Models
{
    public class Category
    {
        [Key]
        //[HiddenInput(DisplayValue = false)]
        public Guid CategoryId { get; set; }
        
        [Required(ErrorMessage = "Please enter category name")]
        [StringLength(70, MinimumLength = 3)]
        [DisplayName("Category")]
        public string Name { get; set; }

        public virtual ICollection<Place> Places { get; set; }
    }
}
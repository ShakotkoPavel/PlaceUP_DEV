using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaceUp.Models
{
    public class Tag
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid? TagId { get; set; }

        public string Name { get; set; }

        public ICollection<Place> Places { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaceUp.Models
{
    public class Feedback
    {
        [HiddenInput(DisplayValue = false)]
        public Guid FeedbackId { get; set; }

        [Required]
        public bool LikeDislike { get; set; }

        [Required(ErrorMessage = "Please text your review")]
        [DataType(DataType.MultilineText)]
        public string Review { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid? PlaceId { get; set; }

        public Place Place { get; set; }
    }
}
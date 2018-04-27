using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaceUp.Models
{
    public class Place
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid PlaceId { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [StringLength(150)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter adress of place")]
        [StringLength(250)]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone number")]
        public string Phone { get; set; }

        [DisplayName("Website")]
        [DataType(DataType.Url)]
        public string WebSite { get; set; }

        [Required(ErrorMessage = "Please enter email adress")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Opening date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime OpeningDate { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public virtual Category Category { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid? CategoryId { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public Place()
        {
            Feedbacks = new List<Feedback>();
            Tags = new List<Tag>();
        }
    }
}
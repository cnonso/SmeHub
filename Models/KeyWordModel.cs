using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc; 

namespace SME_Hub.Models
{
    public class KeyWordModel
    {
        public KeyWordModel()
        {
            AvailableKeyWords = new List<SelectListItem>();
            AvailablePlaces = new List<SelectListItem>();
        }

        [Display(Name="KeyWord")]
         public int KeyWordId { get; set; }
        public IList<SelectListItem> AvailableKeyWords { get; set; }


        [Display(Name = "Place")]
        public int PlaceId { get; set; }
        public IList<SelectListItem> AvailablePlaces { get; set; }
    }
}
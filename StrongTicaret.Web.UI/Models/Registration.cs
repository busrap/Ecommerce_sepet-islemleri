using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StrongTicaret.Web.UI.Models
{
    public class Registration
    {

        
   
            [Display(Name = "FirstName", ResourceType = typeof(language))]
            [Required(ErrorMessageResourceType = typeof(language), ErrorMessageResourceName = "FirstNameRequired")]
            public string FirstName
            {
                get;
                set;
            }
            [Display(Name = "LastName", ResourceType = typeof(language))]
            [Required(ErrorMessageResourceType = typeof(language), ErrorMessageResourceName = "LastNameRequired")]
            public string LastName
            {
                get;
                set;
            }
            [Display(Name = "Email", ResourceType = typeof(language))]
            [Required(ErrorMessageResourceType = typeof(language), ErrorMessageResourceName = "EmailRequired")]
            [RegularExpression(@"^([0-9a-zA-Z]([/+/-_/.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-/w]*[0-9a-zA-Z]*/.)+[a-zA-Z0-9]{2,3})$", ErrorMessageResourceType = typeof(language), ErrorMessageResourceName = "EmailInvalid")]
            public string Email
            {
                get;
                set;
            }
            [Display(Name = "Country", ResourceType = typeof(language))]
            [Required(ErrorMessageResourceType = typeof(language), ErrorMessageResourceName = "CountryNameRequired")]
            public string Country
            {
                get;
                set;
            }
        
    


}
}
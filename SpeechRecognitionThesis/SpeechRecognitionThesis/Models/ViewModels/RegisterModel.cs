using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.ViewModels
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }
        public void OnGet()
        {
        }
    }
}

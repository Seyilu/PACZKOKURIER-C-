using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_LAB6.Pages
{
    public class Index4 : PageModel
    {
        private readonly ILogger<Index4> _logger;

        public Index4(ILogger<Index4> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}

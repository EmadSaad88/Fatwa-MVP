using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FatwaQA.Models;
using Microsoft.AspNetCore.Localization;

namespace FatwaQA.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        // Map short culture codes to full culture codes
        var fullCulture = culture switch
        {
            "en" => "en-US",
            "ar" => "ar-SA",
            _ => "en-US"
        };

        if (!string.IsNullOrEmpty(fullCulture))
        {
            Response.Cookies.Append(
                "CultureCookie",
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(fullCulture)),
                new CookieOptions 
                { 
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true,
                    HttpOnly = false,
                    Path = "/",
                    SameSite = SameSiteMode.Lax
                }
            );
        }

        return LocalRedirect(returnUrl ?? "/");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

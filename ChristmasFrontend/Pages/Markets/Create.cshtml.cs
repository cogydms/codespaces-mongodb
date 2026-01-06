using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChristmasFrontend.Models;
using ChristmasFrontend.Services;

namespace ChristmasFrontend.Pages.Markets;

public class CreateModel : PageModel
{
    private readonly ChristmasService _service;

    public CreateModel(ChristmasService service)
    {
        _service = service;
    }

    [BindProperty]
    public ChristmasMarket Market { get; set; } = new();
    public Locations Location { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            await _service.CreateAsync(Market);
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error creating Market. Please try again.");
            return Page();
        }
    }
}

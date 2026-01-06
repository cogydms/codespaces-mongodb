using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChristmasFrontend.Models;
using ChristmasFrontend.Services;
using System.Security.Cryptography.X509Certificates;

namespace ChristmasFrontend.Pages.Markets;

public class DeleteModel : PageModel
{
    private readonly ChristmasService _service;

    public DeleteModel(ChristmasService service)
    {
        _service = service;
    }

    [BindProperty]
    public ChristmasMarket Market { get; set; } = new();
    public Locations Location { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var market = await _service.GetMarketByIdAsync(id);

        if (market == null)
            return NotFound();

        Market = market;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return RedirectToPage("/Index");
        } 
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error deleting Market. Please try again.");
            return Page();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChristmasFrontend.Models;
using ChristmasFrontend.Services;

namespace ChristmasFrontend.Pages.Markets;

public class EditModel : PageModel
{
    private readonly ChristmasService _service;

    public EditModel(ChristmasService service)
    {
        _service = service;
    }
    [BindProperty]
    public ChristmasMarket Market { get; set; } = new();
    public Locations Location { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var market = await _service.GetMarketByIdAsync(id);
        
        if(market ==null)
            return NotFound();
        
        Market = market;
        Location = await _service.GetLocationByIdAsync(market.Location_Id);
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            await _service.UpdateAsync(Market.Id, Market);
            return RedirectToPage("/Index");
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Error updating the market. Please try again.");
            return Page();
        }
    }
}
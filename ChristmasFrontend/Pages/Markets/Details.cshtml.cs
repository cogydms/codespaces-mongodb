using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChristmasFrontend.Models;
using ChristmasFrontend.Services;

namespace ChristmasFrontend.Pages.Markets;

public class DetailsModel : PageModel
{
    private readonly ChristmasService _service;

    public DetailsModel(ChristmasService service)
    {
        _service = service;
    }

    public ChristmasMarket Market { get; set; } = new();
    public Locations Location { get; set; } = new();


    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            return RedirectToPage("/Index");

        var market = await _service.GetMarketByIdAsync(id);
        if (market == null)
            return RedirectToPage("/Index");

        Market = market;
        Location = await _service.GetLocationByIdAsync(market.Location_Id);

        return Page();
    }

}

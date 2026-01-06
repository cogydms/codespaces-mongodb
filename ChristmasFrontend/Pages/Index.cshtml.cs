using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ChristmasFrontend.Models;
using ChristmasFrontend.Services;

namespace ChristmasFrontend.Pages;

public class IndexModel : PageModel
{
    private readonly ChristmasService _service;
    
    [BindProperty(SupportsGet = true)]
    public DateTime? SelectedDate { get; set; }

    public IndexModel(ChristmasService service)
    {
        _service = service;
    }

    // 마켓 + 로케이션 정보를 함께 담을 리스트
    public class MarketWithLocation
    {
        public ChristmasMarket Market { get; set; } = new();
        public Locations Location { get; set; } = new();
    }

    public List<MarketWithLocation> MarketDetails { get; set; } = new();

    public async Task OnGetAsync(string? keyword)
    {
        MarketDetails.Clear();

        List<ChristmasMarket> markets;

        if (string.IsNullOrWhiteSpace(keyword))
            markets = await _service.GetMarketsAsync();
        else
            markets = await _service.SearchMarketAsync(keyword);

        // 날짜 필터
        if (SelectedDate.HasValue)
        {
            markets = markets
                .Where(m => m.StartDate <= SelectedDate.Value && m.EndDate >= SelectedDate.Value)
                .ToList();
        }

        foreach (var market in markets)
        {
            var location = await _service.GetLocationByIdAsync(market.Location_Id);
            MarketDetails.Add(new MarketWithLocation
            {
                Market = market,
                Location = location
            });
        }
    }

}

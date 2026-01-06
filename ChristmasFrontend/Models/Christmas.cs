using System.ComponentModel.DataAnnotations;

namespace ChristmasFrontend.Models;

public class ChristmasMarket
{
    public string Id {get; set;}

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Location ID is required")]
    public string Location_Id { get; set; } = string.Empty;

    public string Description { get; set; }

    [Required(ErrorMessage = "StartDate is required")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "EndDate is required")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; } = DateTime.Now;

}
public class Locations
{
    [Required(ErrorMessage = "Location ID is required")]
    public string Id { get; set; }

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; }

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; set; }
    public string Address { get; set; }
    public string GoogleMapUrl { get; set; }
}

using InventoryRepo.Models.ViewModel;
using InventoryRepo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class CustomerOrderViewModel
{
    public CustomerOrder CustomerOrder { get; set; }

    public IEnumerable<SelectListItem> Products { get; set; }

    public IEnumerable<SelectListItem> Customers { get; set; }

    [NotMapped]
    public List<OrderedProductViewModel> Items { get; set; }

    [Display(Name = "Selected Products")]
    public List<int> ProductIds { get; set; }

    [NotMapped]
    public string DisplayProducts { get; set; }

    // Other properties as needed
}

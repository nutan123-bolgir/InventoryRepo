using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryRepo.Models.ViewModel
{
    public class CustomerBillViewModel
    {

        public int CustomerOrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public string CustomerName { get; set; }

        public List<OrderedProductViewModel> Items { get; set; }

        public IEnumerable<SelectListItem> ItemList { get; set; }

        public CustomerBillViewModel()
        {
            Items = new List<OrderedProductViewModel>();
        }
        public List<OrderedProductViewModel> SelectedProducts { get; set; }
        public decimal Quantity { get; set; }
        public decimal GrandTotal { get; set; }


    }
}

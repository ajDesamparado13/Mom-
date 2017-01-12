using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using MomPlusAdmin.Domain.Entity;
using System.ComponentModel;

namespace MomPlusAdmin.Models
{
    public class InventoryModel
    {
        public Guid? user_id { get; set; }
       
       [Required]
       public int noOfMeds { get; set; }

       public IEnumerable<SelectListItem> selectedItem { get; set; }       
       [Required]
       public String ItemDescription { get; set; }
       
       public String checkUpType { get; set; }
    }

    public class ItemModel
    {
        [DisplayName("Item ID")]
        public int item_id { get; set; }
        
        [Required(ErrorMessage = "*Required")]
        public String Description { get; set; }

        [Required(ErrorMessage = "*Required")]
        [DisplayName("Unit Of Issued")]
        public String UnitOfIssued { get; set; }
        
        [Required(ErrorMessage = "*Required")]
        public int Qty { get; set; }
        
        [Required(ErrorMessage = "*Required")]
        [DisplayName("Unit Cost")]
        public Decimal UnitCost { get; set; }

        [DisplayName("Total Amount")]
        public Decimal? TotalAmount { get; set; }
    }
}
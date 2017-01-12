using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Models;
using MomPlusAdmin.Domain.Entity;
using System.Data;
using MomPlusAdmin.Domain.Repository.AdminRepo;

namespace MomPlusAdmin.Controllers.Inventory
{
    public class InventoryController : Controller
    {
        //
        // GET /Inventory/
        private MomPlusDbContext context = new MomPlusDbContext();
        private AdminRepo repo = new AdminRepo();
         
        #region Inventory processes (Display item in dropdown, update inventory ..)
        private IEnumerable<SelectListItem> displayItemName()
        {
            var result = context.Inventory.ToList();
            ViewBag.list = result;
            //return PartialView();

            var itemdes = context.Inventory.Select(x =>
                      new SelectListItem
                      {
                          Value = x.item_description,
                          Text = x.item_description
                      });

            return new SelectList(itemdes, "Value", "Text");

        }

        public ActionResult InventoryList(Guid? id, String CheckUpType)
        {
           if(Session["Login"] != null)
           {
            InventoryEntity result = new InventoryEntity();
            var model = new InventoryModel()
            {
                user_id = id,
                ItemDescription = result.item_description,
                selectedItem = displayItemName(),
                checkUpType = CheckUpType,
            };
            return View(model);
           }
           TempData["sessionIsExpired"] = "Your session has expired.";
           return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult InventoryList(InventoryModel model)
        {

            if (ModelState.IsValid)
            {
                var des = model.ItemDescription.ToString();

                InventoryEntity getItem = context.Inventory.Where(i => i.item_description.Equals(des)).FirstOrDefault();

                var totalAmount = model.noOfMeds * getItem.unit_cost;

                if (model.noOfMeds > getItem.qty)
                {
                    TempData["inventoryNotUpdated"] = "Oops! please provide valid input";
                }
                else
                {

                    var entity = new InventoryEntity()
                    {
                        item_id = getItem.item_id,
                        unit_of_issued = getItem.unit_of_issued,
                        item_description = des,
                        qty = getItem.qty - model.noOfMeds,
                        unit_cost = getItem.unit_cost,
                        total_amount = getItem.total_amount - totalAmount,

                    };
                    repo.updateInventory(entity);
                    isAlmostEmpty();
                    TempData["isUpdated"] = "Success";
                }
            }
            else
            {
                TempData["inputError"] = "true";
            }
            return RedirectToAction("InventoryList",model);
        }
        public ActionResult RedirectToProfile(Guid id, InventoryModel model) 
        {
             switch(model.checkUpType)
            {
                case "Prenatal": return RedirectToAction("CreatePrenatalRecord", "Prenatal", new { id = id });  break;
                case "updatePrenatal": return RedirectToAction("addTTR_and_Trimester", "Prenatal", new { id = id }); break;

                case "Postpartum": return RedirectToAction("createPostPartum", "Postpartum", new { id = id }); break;
                case "updatePostpartum": return RedirectToAction("updatePostpartum", "Postpartum", new { id = id });break;

                case "Immunization": return RedirectToAction("addImmunization", "Baby", new { id = id }); break;
                case "updateImmunization": return RedirectToAction("updateImmunization", "Baby", new { id = id }); break; break;
            }
             return RedirectToAction("getMotherProfile", "MotherProfile", new { id = id });
        }
        #endregion

        #region display inventory

        public ActionResult InventoryItem()
        {
         
            var inventoryItem = from Inventory in context.Inventory
                            select new ItemModel
                            {
                                item_id = Inventory.item_id,
                                Description = Inventory.item_description,
                                UnitOfIssued = Inventory.unit_of_issued,
                                Qty = Inventory.qty,
                                UnitCost = Inventory.unit_cost,
                                TotalAmount = Inventory.total_amount,
                                
                            };
                           List<ItemModel> item = inventoryItem.ToList();
                            return View(item);

        }
        #endregion

        #region edit
        
        public PartialViewResult update(int id)
        {
            var inventoryItem = from Inventory in context.Inventory where Inventory.item_id != id
                                select new ItemModel
                                {
                                    item_id = Inventory.item_id,
                                    Description = Inventory.item_description,
                                    UnitOfIssued = Inventory.unit_of_issued,
                                    Qty = Inventory.qty,
                                    UnitCost = Inventory.unit_cost,
                                    TotalAmount = Inventory.total_amount,
                                   
                                };
                    ViewData["operationType"] = "Edit";
                    List<ItemModel> item = inventoryItem.ToList();
                    Edit(id);
                    return PartialView("InventoryItem", item);

        }
        public PartialViewResult Edit(int id)
        {
            var item = context.Inventory.Find(id);
            ItemModel model = new ItemModel()
            {
                item_id = item.item_id,
                UnitOfIssued = item.unit_of_issued,
                Description = item.item_description,
                Qty = item.qty,
                UnitCost = item.unit_cost,
                TotalAmount = item.total_amount,
            };
                               
            return PartialView(model);

        }
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult updateItem(ItemModel model)
        {
            if (ModelState.IsValid)
            {
                InventoryEntity entity = new InventoryEntity()
                {
                    item_id = model.item_id,
                    unit_of_issued = model.UnitOfIssued,
                    item_description = model.Description,
                    qty = model.Qty,
                    unit_cost = model.UnitCost,
                    total_amount = model.Qty * model.UnitCost,
                };
                repo.updateInventory(entity);
                isAlmostEmpty();
                TempData["isSaved"] = "Saved"; // make it modal
                return RedirectToAction("InventoryItem");
            }
            else
            {
                TempData["isNotSaved"] = "NotSaved"; // make it modal
                return RedirectToAction("InventoryItem");
            }
        }
        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {
            repo.r_deleteItem(id);
            return RedirectToAction("InventoryItem");
        }

        #endregion

        #region Inventory Add item

        public PartialViewResult NewItem()
        {
            var inventoryItem = from Inventory in context.Inventory
                                select new ItemModel
                                {
                                    item_id = Inventory.item_id,
                                    Description = Inventory.item_description,
                                    UnitOfIssued = Inventory.unit_of_issued,
                                    Qty = Inventory.qty,
                                    UnitCost = Inventory.unit_cost,
                                    TotalAmount = Inventory.total_amount,

                                };
            ViewData["operationType"] = "NewItem";
            addNewItem();
            List<ItemModel> item = inventoryItem.ToList();
            return PartialView("InventoryItem", item);
        } 

        [HttpGet]
        public PartialViewResult addNewItem()
        {
            return PartialView();
        }

        [HttpPost]
        [ActionName("addNewItem")]
        public ActionResult submitItem(ItemModel model)
        {

            if (ModelState.IsValid)
            {
                var totalAmount = model.Qty * model.UnitCost;
                InventoryEntity entity = new InventoryEntity()
                {
                    unit_of_issued = model.UnitOfIssued,
                    item_description = model.Description,
                    qty = model.Qty,
                    unit_cost = Convert.ToDecimal(model.UnitCost),
                    total_amount = Convert.ToDecimal(totalAmount),
                };
                TempData["isSaved"] = "Saved"; // make it modal
                repo.addItem(entity);
                isAlmostEmpty();
                return RedirectToAction("InventoryItem");

            }
            else
            {
                TempData["isNotSaved"] = "NotSaved"; // make it modal
                return RedirectToAction("InventoryItem");
            }
            
        }
        
        #endregion

        #region check item count

        
        public PartialViewResult isAlmostEmpty()
        {
            var count =context.Inventory.Any(a => a.qty <=5);

            if (count)
            {
                ViewBag.isAlmostEmpty = "empty";
            }
            return PartialView();
        }
        #endregion

        #region Search item from inventory

        public ActionResult searchItem(String SearchString)
        {

            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                InventoryEntity item = context.Inventory.Where(i => i.item_description.Contains(SearchString)
                           || i.unit_of_issued.Contains(SearchString)).FirstOrDefault();
                
               
                if (item != null)
                {
                    var inventory = from s in context.Inventory where s.item_description.Contains(SearchString)
                                    || s.unit_of_issued.Contains(SearchString)
                                    select new ItemModel()
                                    {
                                    item_id = item.item_id,
                                    Description = item.item_description,
                                    UnitOfIssued = item.unit_of_issued,
                                    UnitCost = item.unit_cost,
                                    TotalAmount = item.total_amount,
                                    Qty = item.qty
                                    };
                   
                    List<ItemModel> _item = inventory.ToList();
                    TempData["searchFound"] = 1;
                    return View("InventoryItem", _item);

                }
                else
                {
                    TempData["searchNotFound"] = "Item not found.";
                    return RedirectToAction("InventoryItem");

                }
            }
        }

        #endregion



    }
}

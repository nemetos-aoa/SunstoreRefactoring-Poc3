using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog.Objects;
using Mediachase.Commerce.Engine;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Shared;
using Mediachase.Commerce.Website.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServerCommerceSite
{
    public class LabStoreHelper
    {
        
        /// <summary>
        /// Get current market service
        /// </summary>
        private static readonly ICurrentMarket _currentMarketService = ServiceLocator.Current.GetInstance<ICurrentMarket>();

        /// <summary>
        /// Get sale price of entry in current market
        /// </summary>
        /// <param name="entry"> The entry </param>
        /// <param name="quantity"> The quantity</param>
        /// <returns></returns>
        public static Price GetSalePrice(Entry entry, decimal quantity)
        {
            var currentMarket = _currentMarketService.GetCurrentMarket();
            return StoreHelper.GetSalePrice(entry, quantity, currentMarket);
        }

        /// <summary>
        /// Get discount price of entry in current market
        /// </summary>
        /// <param name="entry">The entry</param>
        /// <returns></returns>
        public static Price GetDiscountPrice(Entry entry)
        {
            var currentMarket = _currentMarketService.GetCurrentMarket();
            return StoreHelper.GetDiscountPrice(entry, string.Empty, string.Empty, currentMarket);
        }

        /// <summary>
        /// Allow Add to Cart ( check quantity availble when add to cart)
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="entry"></param>
        /// <param name="fixedQuantity"></param>
        /// <param name="quantity"></param>
        /// <param name="warehouseCode"></param>
        /// <param name="errorMessage"></param>        
        public static bool AllowAddToCart(Cart cart, Entry entry, bool fixedQuantity, decimal quantity, string warehouseCode, out string errorMessage)
        {
            if (!fixedQuantity)
            {
                if (cart.OrderForms.Count > 0)
                {
                    var lineItem = cart.OrderForms[0].LineItems.Cast<LineItem>().SingleOrDefault(l => l.CatalogEntryId == entry.ID);
                    if (lineItem != null)
                    {
                        quantity += lineItem.Quantity;
                    }
                }
            }

            if (GetSalePrice(entry, quantity) == null)
            {
                errorMessage = "No Price.";
                return false;
            }

            if (entry.WarehouseInventories == null)
            {
                errorMessage = "No Warehouse.";
                return false;
            }

            var warehouseInventoryList = entry.WarehouseInventories.WarehouseInventory.Where(i => i.WarehouseCode == warehouseCode);
            var warehouseInventory = warehouseInventoryList.FirstOrDefault();

            if (warehouseInventory == null)
            {
                errorMessage = "No Warehouse.";
                return false;
            }
            //If entry is not available, check for pre-order
            if (entry.StartDate > FrameworkContext.Current.CurrentDateTime)
            {
                if (warehouseInventory.AllowPreorder)
                {
                    if (warehouseInventory.PreorderAvailabilityDate > FrameworkContext.Current.CurrentDateTime)
                    {
                        errorMessage = "Preorder is unavailable.";
                        return false;
                    }
                    if (quantity > warehouseInventory.PreorderQuantity)
                    {
                        errorMessage = "Not enough for Pre-order quantity.";
                        return false;
                    }
                }
                else
                {
                    errorMessage = "Product is not yet available. Pre-order is not allowed.";
                    return false;
                }
            }
            else
            {
                if (warehouseInventory.InStockQuantity > 0 &&
                    warehouseInventory.InStockQuantity >=
                        warehouseInventory.ReservedQuantity + quantity)
                {
                    errorMessage = "NoError";
                    return true;
                }

                //Not enough quantity in stock, check for backorder
                if (!warehouseInventory.AllowBackorder)
                {
                    if (warehouseInventory.InStockQuantity < quantity ||
                        (warehouseInventory.InStockQuantity -
                            warehouseInventory.ReservedQuantity) < quantity)
                    {
                        errorMessage = "Out of stock.";
                        return false;
                    }
                    errorMessage = "Not enough quantity.";
                    return false;
                }

                if (warehouseInventory.BackorderAvailabilityDate > FrameworkContext.Current.CurrentDateTime)
                {
                    errorMessage = "Not enough quantity. Backorder is unavailable.";
                    return false;
                }
                if (quantity > warehouseInventory.InStockQuantity -
                    warehouseInventory.ReservedQuantity + warehouseInventory.BackorderQuantity)
                {
                    errorMessage = "Not enough for Backorder quantity.";
                    return false;
                }
            }
            errorMessage = "NoError";
            return true;
        }

        public static Money ConvertCurrency(Money money, Currency currency)
        {
            var retVal = !CurrencyFormatter.CanBeConverted(money.Currency, currency)
                ? money
                : CurrencyFormatter.ConvertCurrency(money, currency);

            return retVal;
        }
		
        public static string RunWorkflowAndReturnFormattedMessage(Cart cart, string workflowName)
        {
            WorkflowResults results = cart.RunWorkflow(workflowName);
            IEnumerable<string> resultsMessages = OrderGroupWorkflowManager.GetWarningsFromWorkflowResult(results);
            string returnString = string.Empty;

            if (resultsMessages.Count() > 0)
            {
                returnString = "Workflow Messages: ";
                foreach (string result in resultsMessages)
                {
                    returnString += result + "<BR />";
                }
            }

            return returnString;
        }		
    }
}
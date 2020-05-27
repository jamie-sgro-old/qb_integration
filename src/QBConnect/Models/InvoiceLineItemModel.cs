using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBConnect.Models {
  public class InvoiceLineItemModel {

    /// <summary>
    /// A monetary amount.
    /// </summary>
    public double? Amount { get; set; }

    /// <summary>
    /// Classes can be used to separate transactions into 
    /// meaningful categories. (For example, transactions 
    /// could be classified according to department, business 
    /// location, or type of work.) In QuickBooks, class 
    /// tracking is off by default. A ClassRef aggregate 
    /// refers to one of these named classes. For example, 
    /// in a TimeTracking message, ClassRef refers to the 
    /// QuickBooks class into which the timed activity falls.
    /// If a ClassRef aggregate includes both FullName and 
    /// ListID, FullName will be ignored. In an InvoiceAdd 
    /// request, if you specify a ClassRef for the whole invoice, 
    /// that same ClassRef is automatically used in the line 
    /// items. If you want to clear that (that is, have NO ClassRef
    /// for the line item, you can clear it in the line item by
    /// simply not specifying it in the line item.
    /// </summary>
    public string ClassRef { get; set; }

    /// <summary>
    /// A descriptive text field.
    /// </summary>
    public string Desc { get; set; }
    public string InventorySiteLocationRef { get; set; }
    public string InventorySiteRef { get; set; }
    public bool? IsTaxable { get; set; }

    /// <summary>
    /// Depending on the request containing it, ItemRef can refer to 
    /// an item on any Item list such as ItemDiscount, ItemInventory, 
    /// and so forth, or it may accept only a subset of item types.
    /// For example, here are some requests that impose limits on what
    /// items ItemRef can refer to. For PurchaseOrder and Bill requests,
    /// ItemRef cannot refer to discount items or sales-tax itemsFor
    /// VehicleMilageAdd requests, the ItemRef must refer to a service 
    /// item or an other charge item.For BillingRateAdd requests, the 
    /// ItemRef must refer to a service item. You can use an ItemQuery 
    /// request to get information about all the items that are set up
    /// in the QuickBooks file. “Items” are line items used for fast
    /// entry on sales and purchase forms. They include services and 
    /// goods that a business buys and sells, as well as special items
    /// that perform calculations–for example, subtotal, discount, and
    /// sales-tax items. Note: In a request, if an ItemRef aggregate 
    /// includes both FullName and ListID, FullName will be ignored.
    /// </summary>
    public string ItemRef { get; set; }
    public string LinkToTxnTxnID { get; set; }
    public string LinkToTxnTxnLineID { get; set; }

    /// <summary>
    /// If you specify both Rate and Amount in a request, 
    /// the Rate you provide will be ignored, and you will 
    /// receive a warning. If you specify both Quantity and 
    /// Amount in an Add or Mod request, QuickBooks will 
    /// use them to calculate Rate. (Rate, Amount, and 
    /// Quantity cannot be cleared.)
    /// </summary>
    public double? ORRatePriceLevelRate { get; set; }

    /// <summary>
    /// Indicates the price of something as a percent.
    /// </summary>
    public double? ORRatePriceLevelRatePercent { get; set; }

    /// <summary>
    /// You can use price levels to specify custom pricing for 
    /// specific customers. Once you create a price level for 
    /// a customer, QuickBooks will automatically use the custom 
    /// price in new invoices, sales receipts, sales orders or 
    /// credit memos for that customer. You can override this 
    /// automatic feature, however, when you create the invoices,
    /// sales receipts, etc.) The user can now specify a price 
    /// level on line items in the following supported sales 
    /// transactions: invoices, sales receipts, credit memos, 
    /// and sales orders. Notice that the response data for 
    /// the affected sales transaction does not list the price
    /// level that was used. The response simply lists the 
    /// Rate for the item, which was set using the price level.
    /// </summary>
    public string ORRatePriceLevelPriceLevelRef { get; set; }

    /// <summary>
    /// Other, Other1, and Other2 are standard QuickBooks custom
    /// fields available to transactions. The Other field is a 
    /// transaction-level field, like the FOB field, PO Number 
    /// field, and so forth. This field appears only once for 
    /// the transaction: you can write to it and modify it. The 
    /// Other1 and Other2 fields exist at the line item level; 
    /// each line item has them, and you can write or modify the
    /// value in each line. These custom fields are available for
    /// immediate use: you don’t need to enable these in the 
    /// transaction template to be able to access them via SDK. 
    /// (However, those Other, Other1, Other2 fields and their 
    /// values are viewable and printable in QuickBooks only if 
    /// the transaction template has these enabled!) Note: you 
    /// cannot use DataExtDef to define Other, Other1, Other2 for
    /// the transaction. There is no need to in any case. Those 
    /// are automatically available. Notice that the Other, Other1,
    /// and Other2 names are the real SDK names for those custom 
    /// fields: that is, their DataExtName value will always be Other,
    /// Other1, or Other2. Even if the user has re-labelled those 
    /// custom fields to something else, such as “Barracks Number”,
    /// or “Max Headroom”, or even “Pleni Potentiary”. This 
    /// re-labelling has no effect on the SDK. You’ll always write to
    /// them or modify them as Other, Other1, or Other2.
    /// </summary>
    public string Other1 { get; set; }
    public string Other2 { get; set; }

    /// <summary>
    /// Refers to a QuickBooks account. If you are using QB Online 
    /// edition, you cannot specify an accounts receivable account 
    /// here. If an OverrideItemAccountRef aggregate includes both 
    /// FullName and ListID, FullName will be ignored.
    /// </summary>
    public string OverrideItemAccountRef { get; set; }

    /// <summary>
    /// QuantityFor transactions: If an item line add on a transaction
    /// request specifies Quantity and Amount but not Rate, QuickBooks
    /// will use Quantity and Amount to calculate Rate. Likewise, if
    /// a request specifies Quantity and Rate but not Amount, 
    /// QuickBooks will calculate the Amount. If a transaction add 
    /// request includes a reference to an ItemDiscount item, do not
    /// include a Quantity element as well, or you will get an error.
    /// For Item requests: Quantity indicates how many of this item 
    /// there are.
    /// </summary>
    public double? Quantity { get; set; }

    /// <summary>
    /// Each item on a sales form is assigned a sales-tax code that
    /// indicates whether the item is taxable or non-taxable, and why.
    /// Two general codes, which can be modified but not deleted, 
    /// appear on the sales-tax code list by default:Non-taxable 
    /// (Name = NON; Desc = Non-Taxable; IsTaxable = false)Taxable 
    /// (Name = TAX; Desc = Taxable; IsTaxable = true) A sales-tax 
    /// code can be deleted only if it is no longer associated with
    /// any customer, item, or transaction. If the “Do You Charge 
    /// Sales Tax?” preference within QuickBooks is set to No, 
    /// QuickBooks will assign the default non-taxable sales-tax code
    /// to all sales. A SalesTaxCodeRef aggregate refers to a 
    /// sales-tax code on the list. In a request, if a SalesTaxCodeRef 
    /// aggregate includes both FullName and ListID, FullName will be 
    /// ignored. In a Customer message, SalesTaxCodeRef refers to the 
    /// sales-tax code that will be used for items related to this 
    /// customer. In an ItemInventory message, SalesTaxCodeRef refers 
    /// to the type of sales tax that will be charged for this item, 
    /// if it is a taxable item and if sales tax is set up within 
    /// QuickBooks.
    /// </summary>
    public string SalesTaxCodeRef { get; set; }

    /// <summary>
    /// Indicates the date on which the QuickBooks user performs the
    /// service for the customer.
    /// </summary>
    public DateTime? ServiceDate { get; set; }
    public double? TaxAmount { get; set; }

    /// <summary>
    /// In a transaction line item, the name of the unit of measure 
    /// selected from within the item’s available units. If the company
    /// file is enabled only for single unit of measure per item, this
    /// must be the base unit!
    /// </summary>
    public string UnitOfMeasure { get; set; }

  }
}

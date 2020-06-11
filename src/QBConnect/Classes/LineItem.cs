using QBConnect.Classes.Interfaces;
using QBConnect.Models;
using QBFC13Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBConnect.Classes {
  internal class LineItem : IInvoiceLineAppender {

    #region Constructor

    public LineItem(IInvoiceAdd invoiceAdd) {
      this.Line = invoiceAdd.ORInvoiceLineAddList.Append();
    }

    #endregion Constructor


    #region Interface



    #endregion Interface


    #region Propterties

    public IORInvoiceLineAdd Line { get; set; }

    private double? Amount {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.Amount.SetValue((double)value);
        }
      }
    }
    private string ItemRef {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.ItemRef.FullName.SetValue(value);
        }
      }
    }
    private string Desc {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.Desc.SetValue(value);
        }
      }
    }
    private string ClassRef {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.ClassRef.FullName.SetValue(value);
        }
      }
    }
    private string InventorySiteLocationRef {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.InventorySiteLocationRef.FullName
          .SetValue(value);
        }
      }
    }
    private string InventorySiteRef {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.InventorySiteRef.FullName.SetValue(value);
        }
      }
    }
    private bool? IsTaxable {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.IsTaxable.SetValue((bool)value);
        }
      }
    }
    private string LinkToTxnTxnID {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.LinkToTxn.TxnID.SetValue(value);
        }
      }
    }
    private string LinkToTxnTxnLineID {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.LinkToTxn.TxnLineID.SetValue(value);
        }
      }
    }
    private double? ORRatePriceLevelRate {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.ORRatePriceLevel.Rate.SetValue((double)value);
        }
      }
    }
    private double? ORRatePriceLevelRatePercent {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.ORRatePriceLevel.RatePercent.SetValue((double)value);
        }
      }
    }
    private string ORRatePriceLevelPriceLevelRef {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.ORRatePriceLevel.PriceLevelRef
            .FullName.SetValue(value);
        }
      }
    }
    private string Other1 {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.Other1.SetValue(value);
        }
      }
    }
    private string Other2 {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.Other2.SetValue(value);
        }
      }
    }
    private string OverrideItemAccountRef {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.OverrideItemAccountRef.FullName.SetValue(value);
        }
      }
    }
    private double? Quantity {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.Quantity.SetValue((double)value);
        }
      }
    }
    private string SalesTaxCodeRef {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.SalesTaxCodeRef.FullName.SetValue(value);
        }
      }
    }
    private DateTime? ServiceDate {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.ServiceDate.SetValue((DateTime)value);
        }
      }
    }
    private double? TaxAmount {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.TaxAmount.SetValue((double)value);
        }
      }
    }
    private string UnitOfMeasure {
      set {
        if (value != null) {
          Line.InvoiceLineAdd.UnitOfMeasure.SetValue(value);
        }
      }
    }

    #endregion Propterties


    #region Methods

    public void AddLine(IInvoiceLineItemModel lineItem) {
      Amount =                        lineItem.Amount;
      ClassRef =                      lineItem.ClassRef;
      Desc =                          lineItem.Desc;
      InventorySiteLocationRef =      lineItem.InventorySiteLocationRef;
      InventorySiteRef =              lineItem.InventorySiteRef;
      IsTaxable =                     lineItem.IsTaxable;
      ItemRef =                       lineItem.ItemRef;
      LinkToTxnTxnID =                lineItem.LinkToTxnTxnID;
      LinkToTxnTxnLineID =            lineItem.LinkToTxnTxnLineID;
      ORRatePriceLevelRate =          lineItem.ORRatePriceLevelRate;
      ORRatePriceLevelRatePercent =   lineItem.ORRatePriceLevelRatePercent;
      ORRatePriceLevelPriceLevelRef = lineItem.ORRatePriceLevelPriceLevelRef;
      Other1 =                        lineItem.Other1;
      Other2 =                        lineItem.Other2;
      OverrideItemAccountRef =        lineItem.OverrideItemAccountRef;
      Quantity =                      lineItem.Quantity;
      SalesTaxCodeRef =               lineItem.SalesTaxCodeRef;
      ServiceDate =                   lineItem.ServiceDate;
      TaxAmount =                     lineItem.TaxAmount;
      UnitOfMeasure =                 lineItem.UnitOfMeasure;
    }

    #endregion Methods

  }
}

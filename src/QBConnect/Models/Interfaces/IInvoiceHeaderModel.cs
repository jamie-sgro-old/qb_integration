using System;

namespace QBConnect.Models {
  public interface IInvoiceHeaderModel {
    /// <summary>
    /// Refers to an accounts receivable account in the QuickBooks
    /// file. (The AccountType of this account will be 
    /// AccountsReceivable.) If an ARAccountRef aggregate includes
    /// both FullName and ListID, FullName will be ignored. If this
    /// field is in a transaction that links to other transactions,
    /// make sure this ARAccountRef matches the ARAccountRef used 
    /// in the other transactions. For example, in an ARRefundCreditCard
    /// transaction, the ARAccountRef of the credit card refund 
    /// ransaction must match the ARAccountRef used in each of the 
    /// linked credit transactions.
    /// 
    /// FullName (along with ListID) is a way to identify a list object.
    /// The FullName is the name prefixed by the names of each ancestor,
    /// for example Jones:Kitchen:Cabinets. FullName values are not case
    /// -sensitive.
    /// </summary>
    string ARAccountRefFullName { get; set; }

    /// <summary>
    /// Refers to an accounts receivable account in the QuickBooks
    /// file. (The AccountType of this account will be 
    /// AccountsReceivable.) If an ARAccountRef aggregate includes
    /// both FullName and ListID, FullName will be ignored. If this
    /// field is in a transaction that links to other transactions,
    /// make sure this ARAccountRef matches the ARAccountRef used 
    /// in the other transactions. For example, in an ARRefundCreditCard
    /// transaction, the ARAccountRef of the credit card refund 
    /// ransaction must match the ARAccountRef used in each of the 
    /// linked credit transactions.
    /// 
    /// Along with FullName, ListID is a way to identify a list object. 
    /// When a list object is added to QuickBooks through the SDK or 
    /// through the QuickBooks user interface, the server assigns it a
    /// ListID. A ListID is not unique across lists, but it is unique 
    /// across each particular type of list. For example, two customers 
    /// could not have the same ListID, and a customer could not have 
    /// the same ListID as an employee (because Customer and Employee 
    /// are both name lists). But a customer could have the same ListID 
    /// as a non-inventory item.
    /// </summary>
    string ARAccountRefListID { get; set; }

    /// <summary>
    /// Classes can be used to separate transactions into meaningful 
    /// categories. (For example, transactions could be classified 
    /// according to department, business location, or type of work.) 
    /// In QuickBooks, class tracking is off by default. A ClassRef 
    /// aggregate refers to one of these named classes. For example, 
    /// in a TimeTracking message, ClassRef refers to the QuickBooks
    /// class into which the timed activity falls. If a ClassRef
    /// aggregate includes both FullName and ListID, FullName will be 
    /// ignored. In an InvoiceAdd request, if you specify a ClassRef
    /// for the whole invoice, that same ClassRef is automatically used 
    /// in the line items. If you want to clear that (that is, have NO 
    /// ClassRef for the line item, you can clear it in the line item
    /// by simply not specifying it in the line item.
    /// </summary>
    string ClassRefFullName { get; set; }

    /// <summary>
    /// Classes can be used to separate transactions into meaningful 
    /// categories. (For example, transactions could be classified 
    /// according to department, business location, or type of work.) 
    /// In QuickBooks, class tracking is off by default. A ClassRef 
    /// aggregate refers to one of these named classes. For example, 
    /// in a TimeTracking message, ClassRef refers to the QuickBooks
    /// class into which the timed activity falls. If a ClassRef
    /// aggregate includes both FullName and ListID, FullName will be 
    /// ignored. In an InvoiceAdd request, if you specify a ClassRef
    /// for the whole invoice, that same ClassRef is automatically used 
    /// in the line items. If you want to clear that (that is, have NO 
    /// ClassRef for the line item, you can clear it in the line item
    /// by simply not specifying it in the line item.
    /// </summary>
    string ClassRefListID { get; set; }

    /// <summary>
    /// A standard message such as “Thank you for your business,” or “Please 
    /// sign and return this estimate to indicate your approval.” A customer
    /// message can be included at the bottom of a form. A CustomerMsgRef 
    /// aggregate refers to one of the messages on the CustomerMsg list. In 
    /// a request, if a CustomerMsgRef aggregate includes both FullName and 
    /// ListID, FullName will be ignored.
    /// </summary>
    string CustomerMsgRefFullName { get; set; }

    /// <summary>
    /// A standard message such as “Thank you for your business,” or “Please 
    /// sign and return this estimate to indicate your approval.” A customer
    /// message can be included at the bottom of a form. A CustomerMsgRef 
    /// aggregate refers to one of the messages on the CustomerMsg list. In 
    /// a request, if a CustomerMsgRef aggregate includes both FullName and 
    /// ListID, FullName will be ignored.
    /// </summary>
    string CustomerMsgRefListID { get; set; }

    /// <summary>
    /// The customer list includes information about the QuickBooks user’s 
    /// customers and the individual jobs that are being performed for them.
    /// A CustomerRef aggregate refers to one of the customers (or customer
    /// jobs) on the list. In a request, if a CustomerRef aggregate includes
    /// both FullName and ListID, FullName will be ignored. Special cases to
    /// note:In SalesReceipt and ReceivePayment requests, CustomerRef refers
    /// to the customer or customer job to which the payment is credited.In 
    /// a TimeTracking request, CustomerRef refers to the customer or customer
    /// job to which this time could be billed. If IsBillable is set to true,
    /// CustomerRef is required in TimeTrackingAdd. In an ExpenseLineAdd 
    /// request, if AccountRef refers to an A/P account, CustomerRef must refer
    /// to a vendor (not to a customer). If AccountRef refers to any other 
    /// type of account, the CustomerRef must refer to a customer.
    /// </summary>
    string CustomerRefFullName { get; set; }

    /// <summary>
    /// The customer list includes information about the QuickBooks user’s 
    /// customers and the individual jobs that are being performed for them.
    /// A CustomerRef aggregate refers to one of the customers (or customer
    /// jobs) on the list. In a request, if a CustomerRef aggregate includes
    /// both FullName and ListID, FullName will be ignored. Special cases to
    /// note:In SalesReceipt and ReceivePayment requests, CustomerRef refers
    /// to the customer or customer job to which the payment is credited.In 
    /// a TimeTracking request, CustomerRef refers to the customer or customer
    /// job to which this time could be billed. If IsBillable is set to true,
    /// CustomerRef is required in TimeTrackingAdd. In an ExpenseLineAdd 
    /// request, if AccountRef refers to an A/P account, CustomerRef must refer
    /// to a vendor (not to a customer). If AccountRef refers to any other 
    /// type of account, the CustomerRef must refer to a customer.
    /// </summary>
    string CustomerRefListID { get; set; }

    /// <summary>
    /// Refers to the sales-tax code for sales related to this customer. (That
    /// is, it refers to a member of the SalesTaxCode list.) The sales-tax code
    /// indicates whether an item is taxable or non-taxable, and why. In a 
    /// request, if a CustomerSalesTaxCodeRef aggregate includes both FullName 
    /// and ListID, FullName will be ignored.
    /// </summary>
    string CustomerSalesTaxCodeRefFullName { get; set; }

    /// <summary>
    /// Refers to the sales-tax code for sales related to this customer. (That
    /// is, it refers to a member of the SalesTaxCode list.) The sales-tax code
    /// indicates whether an item is taxable or non-taxable, and why. In a 
    /// request, if a CustomerSalesTaxCodeRef aggregate includes both FullName 
    /// and ListID, FullName will be ignored.
    /// </summary>
    string CustomerSalesTaxCodeRefListID { get; set; }

    /// <summary>
    /// If DueDate is not included in an InvoiceAdd request, QuickBooks might 
    /// determine the due date according to the terms set for this customer.
    /// </summary>
    DateTime? DueDate { get; set; }

    /// <summary>
    /// Purchase order number.
    /// </summary>
    string PONumber { get; set; }

    /// <summary>
    /// For future use with international versions of QuickBooks.
    /// </summary>
    bool? IsTaxIncluded { get; set; }

    /// <summary>
    /// If this is set to true, at runtime the customer referenced in this 
    /// transaction will be checked for a valid email address. If there is 
    /// no valid email address, the request will fail. If there is a valid 
    /// email address currently in QuickBooks for the customer, and the request
    /// succeeds, the transaction will be added to QuickBook’s list of forms to
    /// be emailed, possibly in a batch. Notice that setting this field to true
    /// does not actually perform the emailing. Only the QuickBooks user can do
    /// that from within QuickBooks. This cannot be done from the SDK. Setting 
    /// this field to false does not prevent the QuickBooks user from emailing 
    /// the transaction later. It simply results in the transaction NOT being
    /// placed in the list of transactions to be emailed.
    /// </summary>
    bool? IsToBeEmailed { get; set; }

    /// <summary>
    /// If IsToBePrinted is set to true, this transaction is on a list of forms
    /// to be printed later. The user can then choose to print all these forms
    /// at once. Notice that setting this field to true does not actually 
    /// perform the printing. Only the QuickBooks user can do that from within
    /// QuickBooks. This cannot be done from the SDK. Setting this field to 
    /// false does not prevent the QuickBooks user from printing the transaction 
    /// later. It simply results in the transaction NOT being placed in the list
    /// of transactions to be printed.
    /// </summary>
    bool? IsToBePrinted { get; set; }

    /// <summary>
    /// QuickBooks templates specify how to print certain transactions. A 
    /// template query returns the names of all templates that have been defined
    /// in QuickBooks. A TemplateRef element refers to one of these templates.
    /// </summary>
    string TemplateRefFullName { get; set; }

    /// <summary>
    /// QuickBooks templates specify how to print certain transactions. A 
    /// template query returns the names of all templates that have been defined
    /// in QuickBooks. A TemplateRef element refers to one of these templates.
    /// </summary>
    string TemplateRefListID { get; set; }

    /// <summary>
    /// Refers to the payment terms associated with this entity. (This will be
    /// an item on the DateDrivenTerms or StandardTerms list.) If a TermsRef 
    /// aggregate includes both FullName and ListID, FullName will be ignored.
    /// </summary>
    string TermsRefFullName { get; set; }

    /// <summary>
    /// Refers to the payment terms associated with this entity. (This will be
    /// an item on the DateDrivenTerms or StandardTerms list.) If a TermsRef 
    /// aggregate includes both FullName and ListID, FullName will be ignored.
    /// </summary>
    string TermsRefListID { get; set; }

    /// <summary>
    /// The date of the invoice. If you leave TxnDate out of an InvoiceAdd message,
    /// QuickBooks might prefill it with the date of the invoice that was most recently saved.
    /// </summary>
    DateTime? TxnDate { get; set; }

    /// <summary>
    /// Whatever address you specify in this aggregate must not result in an address
    /// greater than 5 lines, otherwise you’ll get a runtime error, because QuickBooks
    /// doesn’t support addresses more than 5 lines. There are two ways to specify an
    /// address within this aggregate: Using Addr1 through Addr3 along with the other
    /// possible aggregate elements, such as City, State, Postalcode.Using Addr1, Addr2,
    /// Addr3, Addr4, and Addr5 to fully specify the address. If you use this so
    /// called “address block” approach, you cannot use any other address elements,
    /// such as City, State, etc. (Note: this approach is not valid for
    /// EmployeeAdd/Mod/Query) If you use the address block approach above, the lines
    /// Addr1…Addr5 are each printed as a separate line on the transaction, and the values
    /// are returned in the Ret object under the aggregate ShipAddressBlock or
    /// BillAddressBlock.
    /// </summary>
    string BillAddress { get; set; }

    /// <summary>
    /// Whatever address you specify in this aggregate must not result in an address
    /// greater than 5 lines, otherwise you’ll get a runtime error, because QuickBooks
    /// doesn’t support addresses more than 5 lines. There are two ways to specify an
    /// address within this aggregate: Using Addr1 through Addr3 along with the other
    /// possible aggregate elements, such as City, State, Postalcode.Using Addr1, Addr2,
    /// Addr3, Addr4, and Addr5 to fully specify the address. If you use this so
    /// called “address block” approach, you cannot use any other address elements,
    /// such as City, State, etc. (Note: this approach is not valid for
    /// EmployeeAdd/Mod/Query) If you use the address block approach above, the lines
    /// Addr1…Addr5 are each printed as a separate line on the transaction, and the values
    /// are returned in the Ret object under the aggregate ShipAddressBlock or
    /// BillAddressBlock.
    /// </summary>
    string ShipAddress { get; set; }

    /// <summary>
    /// QuickBooks uses the term FOB, “freight on board,” to indicate the place from 
    /// which the product is shipped. The FOB has no accounting implications.
    /// </summary>
    string FOB { get; set; }

    /// <summary>
    /// Other, Other1, and Other2 are standard QuickBooks custom fields available to
    /// transactions. The Other field is a transaction-level field, like the FOB field,
    /// PO Number field, and so forth. This field appears only once for the transaction:
    /// you can write to it and modify it. The Other1 and Other2 fields exist at the
    /// line item level; each line item has them, and you can write or modify the value
    /// in each line. These custom fields are available for immediate use: you don’t
    /// need to enable these in the transaction template to be able to access them via
    /// SDK. (However, those Other, Other1, Other2 fields and their values are viewable
    /// and printable in QuickBooks only if the transaction template has these enabled!)
    /// Note: you cannot use DataExtDef to define Other, Other1, Other2 for the
    /// transaction. There is no need to in any case. Those are automatically available.
    /// Notice that the Other, Other1, and Other2 names are the real SDK names for
    /// those custom fields: that is, their DataExtName value will always be Other,
    /// Other1, or Other2. Even if the user has re-labelled those custom fields to
    /// something else, such as “Barracks Number”, or “Max Headroom”, or even “Pleni
    /// Potentiary”. This re-labelling has no effect on the SDK. You’ll always write
    /// to them or modify them as Other, Other1, or Other2.
    /// </summary>
    string Other { get; set; }
  }
}
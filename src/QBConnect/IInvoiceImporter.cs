using System;
using System.Collections.Generic;
using QBConnect.Models;

namespace QBConnect {
  public interface IInvoiceImporter : IDisposable {
    void Import(IInvoiceHeaderModel headerData, List<IInvoiceLineItemModel> lineItems);

    void Rollback();

    /// <summary>
    /// Check if template name provided is included in the list of QB template names
    /// </summary>
    /// <param name="userTemplateName">The name of the template to be used</param>
    /// <returns></returns>
    List<string> GetTemplateNamesList();

    List<string> GetInvoiceIdList();

    void Dispose();
  }
}
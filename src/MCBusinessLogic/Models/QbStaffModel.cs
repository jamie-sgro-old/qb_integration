using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Models {
  public class QbStaffModel {
    public string Item { get; set; }
    public double? Quantity { get; set; }
    public string StaffName { get; set; }
    public string TimeInOut { get; set; }
    public DateTime? ServiceDate { get; set; }
    public string ItemRef { get; set; }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.ViewModels.Interfaces {
  /// <summary>
  /// Interface for the events that trigger when a user interacts with a control
  /// that uses QBFC13 or QuickBooks more generally
  /// </summary>
  public interface IQbInteractable {

    /// <summary>
    /// General Purpose WPF TextBlock to report on potential errors or successes
    /// resulting from the QbInteraction
    /// </summary>
    string ConsoleMessage { get; set; }

    /// <summary>
    /// Determines whether the WPF Item called QbInteract is enabled
    /// Caliburn Micro uses naming conventions beginning in Can* to determine
    /// this property
    /// </summary>
    bool CanQbInteract { get; set; }

    /// <summary>
    /// Determines whether the WPF Item called QbProgressBar is visible or hidden
    /// Caliburn Micro uses naming conventions ending in *IsVisible to determine
    /// this property
    /// </summary>
    bool QbProgressBarIsVisible { get; set; }

    /// <summary>
    /// Executed when 'Import/Export to QuickBooks' button is pressed
    /// Connect to QB and send/recieve all data necessary to fulfill the page's function
    /// </summary>
    Task QbInteract();
  }
}

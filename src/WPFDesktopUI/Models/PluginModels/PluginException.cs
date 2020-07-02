using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopUI.Models.PluginModels {
  class PluginException : Exception {
    public PluginException() {
    }

    public PluginException(string message)
      : base(message) {
    }

    public PluginException(string message, Exception inner)
      : base(message, inner) {
    }
  }
}

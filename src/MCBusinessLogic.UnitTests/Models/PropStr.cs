using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCBusinessLogic.UnitTests.Models {
  internal static class PropStr {
    public static List<string> GetList(object cls) {
      List<string> propStr = new List<string>();
      foreach (var prop in cls.GetType().GetProperties()) {
        propStr.Add(prop.Name);
      }

      return propStr;
    }
  }
}

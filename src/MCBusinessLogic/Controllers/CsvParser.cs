using MCBusinessLogic.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers {
  public class CsvParser {
    public static DataTable ParseFromFile(string path, string delim) {
      using (var csvParser = new TextFieldParser(path)) {
        csvParser.CommentTokens = new string[] { "#" };
        csvParser.SetDelimiters(new string[] { delim });
        csvParser.HasFieldsEnclosedInQuotes = true;

        // Init DataTable
        var dt = new DataTable();
        dt.Clear();

        // Map variable header strings to a set of variable names needed for the Model
        string[] headers = csvParser.ReadFields();
        if (headers == null) return null;

        foreach (string header in headers) {
          dt.Columns.Add(header);
        }


        while (!csvParser.EndOfData) {
          // Read current line fields, pointer moves to the next line.
          object[] fields = csvParser.ReadFields();
          if (fields == null) continue;
          dt.Rows.Add(fields);
        }
        return dt;
      }
    }
  }
}

using MCBusinessLogic.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers {
  public class CsvParser {
    public static List<CsvModel> ParseFromFile(string path, string delim) {
      using (var csvParser = new TextFieldParser(path)) {
        csvParser.CommentTokens = new string[] { "#" };
        csvParser.SetDelimiters(new string[] { delim });
        csvParser.HasFieldsEnclosedInQuotes = true;

        // Map variable header strings to a set of variable names needed for the Model
        var indexOf = new Dictionary<string, int>();
        string[] headers = csvParser.ReadFields();
        if (headers == null) return null;


        // loop through all names in the model and match them to headers
        var properties = typeof(CsvModel).GetProperties();
        foreach (var property in properties) {
          var name = property.Name;

          for (int i = 0; i < headers.Length; i++) {
            if (name != headers[i]) continue;
            indexOf.Add(name, i);
            break;
          }
        }



        var csvData = new List<CsvModel>();

        while (!csvParser.EndOfData) {
          // Read current line fields, pointer moves to the next line.
          string[] fields = csvParser.ReadFields();
          csvData.Add(new CsvModel() {
            Item = fields[indexOf["Item"]],
            Quantity = fields[indexOf["Quantity"]],
            StaffName = fields[indexOf["StaffName"]],
            TimeInOut = fields[indexOf["TimeInOut"]],
            ServiceDate = fields[indexOf["ServiceDate"]],
            Rate = fields[indexOf["Rate"]],
          });
        }
        return csvData;
      }
    }
  }
}

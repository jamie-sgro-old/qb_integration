using MCBusinessLogic.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers {
  public class CsvParser {
    public static List<CsvModel> ParseFromFile(string path, string delim) {
      using (TextFieldParser csvParser = new TextFieldParser(path)) {
        csvParser.CommentTokens = new string[] { "#" };
        csvParser.SetDelimiters(new string[] { delim });
        csvParser.HasFieldsEnclosedInQuotes = true;

        // Skip the row with the column names
        csvParser.ReadLine();

        List<CsvModel> csvData = new List<CsvModel>();

        while (!csvParser.EndOfData) {
          // Read current line fields, pointer moves to the next line.
          string[] fields = csvParser.ReadFields();
          csvData.Add(new CsvModel() {
            Item = fields[0],
            Quantity = fields[1],
            StaffName = fields[2],
            TimeInOut = fields[3],
            ServiceDate = fields[4]
          });
        }
        return csvData;
      }
    }
  }
}

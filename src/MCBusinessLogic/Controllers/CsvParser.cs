using MCBusinessLogic.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBusinessLogic.Controllers {
  public class CsvParser {
    public static void ParseFromFile(string path, string delim) {
      //var path = @"C:\Users\Jamie\Nextcloud\Sangwa\Clients\NX - Nexim Healthcare\01 - Invoicing\Sample Documents\test.csv";
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
          csvData.Add(new CsvModel() { FirstName = fields[0], LastName = fields[1] });
        }
        Console.WriteLine(csvData);
      }
    }
  }
}

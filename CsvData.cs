using System.Text.Json;
using System.Globalization;
using System.ComponentModel;

class CsvData
{
 public string Number { get; set; }
 public string ClientCode { get; set; }
 public string ClientName { get; set; }
 public DateTime OrderDate { get; set; }
 public DateTime? ShipmentDate { get; set; }
 public int Quantity { get; set; }
 public bool Confirmed { get; set; }
 public float Value { get; set; }

 public CsvData() {
  Number = "";
  ClientCode = "";
  ClientName = "";
 }

 public static CsvData FromCsv(string csvLine)
 {
  string[] values = csvLine.Split(',');

  CsvData csvData = new CsvData();

  try {
   csvData.Number = values[0].Trim('"');
   csvData.ClientCode = values[1].Trim('"');
   csvData.ClientName = values[2].Trim('"');
   csvData.OrderDate = Convert.ToDateTime(values[3]);

   if (values[4] != "") {
    csvData.ShipmentDate = Convert.ToDateTime(values[4]);
   }

   csvData.Quantity = Convert.ToInt16(values[5]);
   csvData.Confirmed = (Convert.ToInt16(values[6]) > 0 ? true : false);
   csvData.Value = float.Parse(values[7], CultureInfo.InvariantCulture.NumberFormat);
  } catch (Exception e) {
   Console.WriteLine(e);
  }

  return csvData;
 }
}

using Microsoft.AspNetCore.Mvc;

namespace Netland.Controllers;

[ApiController]
[Route("api/clients")]
public class HomeController : ControllerBase
{
    private readonly IConfiguration Configuration;

    public HomeController(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    [HttpGet]
    public ActionResult Get(string? Number, string? DataRange, string? Clients)
    {
        Console.WriteLine("Number: " + Number);
        Console.WriteLine("DataRange: " + DataRange);
        Console.WriteLine("Clients: " + Clients);

        List<CsvData> values;

        try {
            values = System.IO.File.ReadAllLines(Configuration["DataFile"]).Skip(1).Select(v => CsvData.FromCsv(v)).ToList();
        } catch (Exception e) {
            return Ok(new {status="error", message=e});
        }

        if (Number != null) {
            List<CsvData> filtered_values = values.Where(x => x.Number == Number).ToList();

            values = filtered_values;
        }

        if (DataRange != null) {
            List<string> dates = new List<string>(DataRange.Split('-'));

            DateTime sdate, edate;

            try {
             sdate = Convert.ToDateTime(dates[0]);
             edate = Convert.ToDateTime(dates[1]);
            } catch (FormatException e) {
             return Ok(new {status="error", message=e.ToString()});
            }

            List<CsvData> filtered_values = values.Where(x => x.OrderDate >= sdate && x.OrderDate <= edate).ToList();

            values = filtered_values;
        }

        if (Clients != null) {
            List<string> clientCodes = new List<string>(Clients.Split(','));

            List<CsvData> filtered_values = values.Where(x => clientCodes.Contains(x.ClientCode)).ToList();

            values = filtered_values;
        }

        return Ok(values);
    }
}

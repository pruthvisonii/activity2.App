using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using activity2.Models;

public interface IHolidayService
{
    Task<List<Holiday>> GetHolidaysAsync(int year, bool optional);
}

public class HolidayService : IHolidayService
{
    private const string BaseUrl = "https://canada-holidays.ca/api/v1/holidays";

    public async Task<List<Holiday>> GetHolidaysAsync(int year, bool optional)
    {
        using (var httpClient = new HttpClient())
        {
            var url = $"{BaseUrl}?year={year}&optional={optional}";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var holidaysResponse = JsonConvert.DeserializeObject<HolidaysResponse>(json);
                return holidaysResponse.Holidays;
            }

            // Handle error cases here, throw an exception, or return an empty list
            return new List<Holiday>();
        }
    }
}

public class HolidaysResponse
{
    public List<Holiday> Holidays { get; set; }
}

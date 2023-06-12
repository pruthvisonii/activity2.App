using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using activity2.Models;

namespace activity2
{
    public partial class HolidaysPage : ContentPage
    {
        private const string ApiUrl = "https://canada-holidays.ca/api/v1/holidays?year={0}&optional=false";

        public HolidaysPage()
        {
            InitializeComponent();

            YearPicker.SelectedIndexChanged += OnYearPickerSelectedIndexChanged;
            ProvincePicker.SelectedIndexChanged += OnProvincePickerSelectedIndexChanged;
        }

        private async void OnYearPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (YearPicker.SelectedIndex == -1)
                return;

            int selectedYear = 2020 + YearPicker.SelectedIndex;
            string apiUrl = string.Format(ApiUrl, selectedYear);

            List<Holiday> holidays = await GetHolidays(apiUrl);

            // Display holidays in the ListView
            HolidaysListView.ItemsSource = holidays;
        }

        private async void OnProvincePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProvincePicker.SelectedIndex == -1)
                return;

            string selectedProvince = ProvincePicker.SelectedItem.ToString();
            int selectedYear = 2020 + YearPicker.SelectedIndex;
            string apiUrl = string.Format(ApiUrl, selectedYear);

            List<Holiday> holidays = await GetHolidays(apiUrl);

            // Filter holidays by the selected province
            List<Holiday> filteredHolidays = FilterHolidaysByProvince(holidays, selectedProvince);

            // Display filtered holidays in the ListView
            HolidaysListView.ItemsSource = filteredHolidays;
        }

        private async Task<List<Holiday>> GetHolidays(string apiUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        HolidaysResponse holidaysResponse = JsonConvert.DeserializeObject<HolidaysResponse>(json);
                        return holidaysResponse.Holidays;
                    }
                }
                catch (Exception ex)
                {
                    // Handle error
                    Console.WriteLine(ex.Message);
                }
            }

            return new List<Holiday>();
        }

        private List<Holiday> FilterHolidaysByProvince(List<Holiday> holidays, string province)
        {
            List<Holiday> filteredHolidays = new List<Holiday>();

            foreach (Holiday holiday in holidays)
            {
                foreach (Province holidayProvince in holiday.Provinces)
                {
                    if (holidayProvince.NameEn.Equals(province, StringComparison.OrdinalIgnoreCase))
                    {
                        filteredHolidays.Add(holiday);
                        break;
                    }
                }
            }

            return filteredHolidays;
        }
    }
}

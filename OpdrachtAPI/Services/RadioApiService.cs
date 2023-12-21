using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpdrachtAPI.Services
{
    public class RadioApiService
    {
        private readonly HttpClient _httpClient;

        public RadioApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetRadioDataAsync()
        {
            // Replace the URL with your actual API endpoint
            var apiUrl = "https://api.radiofomix.nl/api/nowplaying/0";

            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);
                return response;
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions, log, or return a default value
                Console.WriteLine($"Error fetching data from API: {ex.Message}");
                return string.Empty;
            }
        }
    }
}

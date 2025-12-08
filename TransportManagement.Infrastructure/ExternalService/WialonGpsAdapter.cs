using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Vehicles;
using System.Net.Http.Json;
using TransportManagement.Application.Interfaces;


namespace TransportManagement.Infrastructure.ExternalService
{
    public class WialonGpsAdapter : IGpsTrackingService
    {
        private readonly HttpClient _httpClient;

        public  WialonGpsAdapter(HttpClient httpClient)
        
            {
        _httpClient = httpClient;
        }
        public  async Task<VehicleLocationDto?> GetLatestLocationAsync(string plateNumber)
        {
            var response = await _httpClient.GetAsync($"/posts/1");
           
           if( !response.IsSuccessStatusCode)
                return null;
                var data = await response.Content.ReadFromJsonAsync<WialonResponse>();
            if (data is null) return null;
            return new VehicleLocationDto
            {
                PlateNumber = data.plate,
                Latitude = data.lat,
                Longitude = data.lng,
                Timestamp = data.time
            };

            throw new NotImplementedException();
        }
    }
    public class WialonResponse
    {
        public string plate { get; set; } = default!;
        public double lat { get; set; }
        public double lng { get; set; }
        public DateTime time { get; set; }
    }
}

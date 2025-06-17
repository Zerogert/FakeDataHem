using FakerHEM.Models.Responses;
using HemConverter.Models.Responses.CoordSystem;
using System.Net.Http;
using System.Net.Http.Json;

namespace FakeDataHem.FetchData
{
    public class HemClient
    {
        public string host = "http://127.0.0.1:5000";
        //public string host = "http://172.25.22.247:5000";
        public async Task<CoordSystemResponse?> GetActiveCoordsAsync()
        {
            HttpClient client = new HttpClient();

            return await client.GetFromJsonAsync<CoordSystemResponse>($"{host}/active-coords-system");
        }

        public async Task<Project?> GetActiveProjectAsync()
        {
            HttpClient client = new HttpClient();
            return await client.GetFromJsonAsync<Project>($"{host}/projects/active");
        }

        public async Task<FakeData?> GetFakeDataAsync()
        {
            HttpClient client = new HttpClient();
            return await client.GetFromJsonAsync<FakeData>($"{host}/fakedata");
        }

        public async Task UpdateFakeDataAsync(FakeData fakeData)
        {
            HttpClient client = new HttpClient();
            await client.PutAsJsonAsync($"{host}/fakedata", fakeData);
        }

        //public async Task<List<Sector>> GetActiveSectorsAsync()
        //{
        //    HttpClient client = new HttpClient();

        //    return await client.GetFromJsonAsync<List<Sector>>($"{host}/sectors/0") ?? new List<Sector>();
        //}
    }

}

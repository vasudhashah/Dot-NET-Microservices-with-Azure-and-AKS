using BussinessLogicLayer.DTO;
using System.Net.Http.Json;

namespace BussinessLogicLayer.HttpClients
{
    public class UsersMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        public UsersMicroserviceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDTO?> GetUserById(Guid userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Users/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException("Bad request", null, System.Net.HttpStatusCode.BadRequest);
                }
                else
                {
                    throw new HttpRequestException($"An error occurred while fetching the user with status code {response.StatusCode}");
                }                    
            }
            
            UserDTO? user = await response.Content.ReadFromJsonAsync<UserDTO>();
            if(user == null)
            {
                throw new HttpRequestException("User not found");
            }
            return user;
        }
    }
}

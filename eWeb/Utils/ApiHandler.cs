using System.Text.Json;

namespace eBookStore.Utils
{
   
    public class ApiHandler
    {
        /// <summary>
        /// eqwrweqqwer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <param name="method"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<T?> DeserializeApiResponse<T>(string apiUrl, HttpMethod method, T value)
        {
            using (var client = new HttpClient())
            {
                string stringData = JsonSerializer.Serialize(value);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (method == HttpMethod.Post)
                {
                    response = await client.PostAsync(apiUrl, contentData);
                }
                else if (method == HttpMethod.Put)
                {
                    response = await client.PutAsync(apiUrl, contentData);
                }
                else
                {
                    throw new Exception("Invalid HTTP method.");
                }
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (responseData != "")
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        T? result = JsonSerializer.Deserialize<T>(responseData, options);
                        return result;
                    }
                    else
                    {
                        return default;
                    }
                }
                else
                {
                    throw new Exception($"API request failed with status code: {response.StatusCode}");
                }
            }
        }

        public static async Task<T?> DeserializeApiResponse<T>(string apiUrl, HttpMethod method )
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = null;
                if (method == HttpMethod.Get)
                {
                    response = await client.GetAsync(apiUrl);
                }
                else if (method == HttpMethod.Delete)
                {
                    response = await client.DeleteAsync(apiUrl);
                }
                else
                {
                    throw new Exception("Invalid HTTP method.");
                }
                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();
                    if (strData != "")
                    {
                        
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        T? result = JsonSerializer.Deserialize<T>(strData, options);
                        return result;
                    }
                    else
                    {
                        return default;
                    }
                }
                else
                {
                    return default;
                    //throw new Exception($"API request failed with status code: {response.StatusCode}");
                }
            }
        }
    }
}

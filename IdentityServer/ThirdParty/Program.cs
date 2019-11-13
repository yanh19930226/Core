using System;
using System.Net.Http;
using IdentityModel.Client;
using System.Threading;

namespace ThirdParty
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpclient = new HttpClient();
            //访问授权服务器获取Token
            var disco = httpclient.GetDiscoveryDocumentAsync("http://localhost:5000").Result;
            if (!disco.IsError)
            {
                System.Console.WriteLine(disco.Error);
            }
            var tokenResponse = httpclient.RequestClientCredentialsTokenAsync(
           new ClientCredentialsTokenRequest
           {
               Address = disco.TokenEndpoint, // Token 端点
               ClientId = "client",
               ClientSecret = "secret",
               Scope = "api"
           }).Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
#warning 返回了未授权401 
            var response = httpclient.GetAsync("http://localhost:5001/api/values").Result;
            if (!response.IsSuccessStatusCode)
            {
                System.Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                System.Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            Console.ReadLine();
        }
    }
}

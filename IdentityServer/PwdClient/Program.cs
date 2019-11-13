using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading;

namespace PwdClient
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

            var tokenResponse = httpclient.RequestPasswordTokenAsync(new PasswordTokenRequest {
                Address = disco.TokenEndpoint, // Token 端点
                ClientId = "pwdclient",
                ClientSecret = "secret",
                Scope = "api",
                UserName="yanh",
                Password="123456"
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

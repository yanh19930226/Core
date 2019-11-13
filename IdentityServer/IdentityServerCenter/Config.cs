using System.Collections;
using System.Collections.Generic;
using IdentityServer4.Models;
//IdentityServer测试时使用的用户
using IdentityServer4.Test;

namespace IdentityServerCenter
{
    public class Config
    {
        //获取资源
        public static IEnumerable<ApiResource> GetResource()
        {
            return new List<ApiResource>{
               new ApiResource("api","MyApi")
           };
        }
        //获取客户端
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>{
               new Client{
                   ClientId="client",
                   //Auth客户端模式
                   AllowedGrantTypes=GrantTypes.ClientCredentials,
                   ClientSecrets={
                       new Secret("secret".Sha256())
                       },
                   AllowedScopes={ "api" }
               },
               //Auth密码端模式
                new Client{
                   ClientId="pwdclient",
                   AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                   ClientSecrets={
                       new Secret("secret".Sha256())
                       },
                   AllowedScopes={"api"}
               }
           };
        }
        //测试用户
        public static List<TestUser>GetTestUsers()
        {
            return new List<TestUser>()
            {
                new TestUser(){
                    SubjectId="1",
                    Username="yanh",
                    Password="123456"
                }
            };
        }
    }
}

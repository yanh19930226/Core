using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DataMigrate.Controllers
{
    /// <summary>
    /// 测试服务
    /// </summary>
    [Route("api/test")]
    [ApiController]
    public class TestsController : Controller
    {
        /// <summary>
        /// 这是一个get请求
        /// </summary>
        /// <remarks>
        /// 例子:
        /// Get api/Test/1
        /// </remarks>
        /// <param name="id">主键</param>
        /// <returns>测试字符串</returns> 
        /// <response code="201">返回value字符串</response>
        /// <response code="400">如果id为空</response>  
        [HttpGet]
        public string Get(int id)
        {
            return "api测试";
        }
        /// <summary>
        /// 这是一个post请求
        /// </summary>
        /// <remarks>
        /// 例子:
        /// Get api/Post/1
        /// </remarks>
        /// <param name="id">主键</param>
        /// <returns>测试字符串</returns> 
        /// <response code="201">返回value字符串</response>
        /// <response code="400">如果id为空</response>  
        [HttpPost]
        public string Post(int id)
        {
            return "api测试";
        }
        /// <summary>
        /// 这是一个put请求
        /// </summary>
        /// <remarks>
        /// 例子:
        /// Get api/Put/1
        /// </remarks>
        /// <param name="id">主键</param>
        /// <returns>测试字符串</returns> 
        /// <response code="201">返回value字符串</response>
        /// <response code="400">如果id为空</response>  
        [HttpPut]
        public string Put(int id)
        {
            return "api测试";
        }
        /// <summary>
        /// 这是一个delete请求
        /// </summary>
        /// <remarks>
        /// 例子:
        /// Get api/Delete/1
        /// </remarks>
        /// <param name="id">主键</param>
        /// <returns>测试字符串</returns> 
        /// <response code="201">返回value字符串</response>
        /// <response code="400">如果id为空</response>  
        [HttpDelete]
        public string Delete(int id)
        {
            return "api测试";
        }
    }
}
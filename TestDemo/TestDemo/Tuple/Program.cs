using System;

namespace Tuple
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 利用构造函数创建元组
            //利用构造函数创建元组
            //var testTuple6 = new Tuple<int, int, int, int, int, int>(1, 2, 3, 4, 5, 6);
            //Console.WriteLine($"Item 1: {testTuple6.Item1}, Item 6: {testTuple6.Item6}");

            //var testTuple10 = new Tuple<int, int, int, int, int, int, int, Tuple<int, int, int>>(1, 2, 3, 4, 5, 6, 7, new Tuple<int, int, int>(8, 9, 10));
            //Console.WriteLine($"Item 1: {testTuple10.Item1}, Item 10: {testTuple10.Rest.Item3}"); 
            #endregion

            #region 利用Tuple静态方法构建元组
            //var primes = System.Tuple.Create(2, 3, 5, 7, 11, 13, 17, 19);
            //Console.WriteLine("Prime numbers less than 20: " +
            //                  "{0}, {1}, {2}, {3}, {4}, {5}, {6}, and {7}",
            //                  primes.Item1, primes.Item2, primes.Item3,
            //                  primes.Item4, primes.Item5, primes.Item6,
            //                  primes.Item7, primes.Rest.Item1);
            #endregion

            #region 从方法返回多个值
            //RunTest(); 
            #endregion

        }

        #region 从方法返回多个值
        static Tuple<string, int, uint> GetStudentInfo(string name)
        {
            return new Tuple<string, int, uint>("Bob", 28, 175);
        }

        static void RunTest()
        {
            var studentInfo = GetStudentInfo("Bob");
            Console.WriteLine($"Student Information: Name [{studentInfo.Item1}], Age [{studentInfo.Item2}], Height [{studentInfo.Item3}]");
        } 
        #endregion
    }
}

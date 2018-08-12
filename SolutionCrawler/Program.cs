using SolutionCrawler.Interfaces;
using System;

namespace SolutionCrawler
{
    class Program
    {
        public static void Main(string[] args)
        {
            CompositionRoot.Wire(new ApplicationModule());

            var solutionCrawler = CompositionRoot.Resolve<IApp>();
            Console.WriteLine("Solution Crawler in progress ... ");
            try
            {
                solutionCrawler.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("Solution Crawler Closing.");
                Console.ReadLine();
            }
        }
    }
}

using System;
using System.Diagnostics;

namespace Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ExampleWithExceptionFilters();
            }
            catch (FileProcessingException otherEx)
            {
                Console.WriteLine("Other exception occured!");
                Console.WriteLine(otherEx.Message);
                Console.WriteLine(otherEx.StackTrace);
                if (otherEx.InnerException is not null)
                {
                    Console.WriteLine("Inner exception:");
                    Console.WriteLine(otherEx.InnerException.Message);
                    Console.WriteLine(otherEx.InnerException.StackTrace);
                }
            }
        }

        private static int[] ConvertToIntArray_Normal(string[] stringArray)
        {
            int[] result = new int[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
            {
                result[i] = ConversionHelper.ToInt(stringArray[i], 0);
            }

            return result;
        }

        private static int[] ConvertToIntArray_Faster(string[] stringArray)
        {
            int[] result = new int[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
            {
                result[i] = ConversionHelper.ToInt_Faster(stringArray[i], 0);
            }

            return result;
        }


        private static void ExampleWithExceptionFilters()
        {
            try
            {
                // TODO: Run in VS because DotNetFiddle doesn't handle console reads
                Console.Write("Enter directory path=");
                var directory = Console.ReadLine();

                Console.Write("Enter file name=");
                var fileName = Console.ReadLine();

                Console.Write("Lookup text=");
                var lookupText = Console.ReadLine();

                Console.Write("Replace with text=");
                var replaceWithText = Console.ReadLine();

                FileProcessor.ReplaceText(directory, fileName, lookupText, replaceWithText);
            }
            catch (Exception otherEx)
            {
                Console.WriteLine("An error has occured!");
                Console.WriteLine(otherEx.Message);
                Console.WriteLine(otherEx.StackTrace);

                throw new FileProcessingException(
                    "Something went wrong with the file processing",
                    otherEx);
            }
        }

        private static void ExampleWithExceptionPerformance()
        {
            string[] list = StringArrayGenerator.RepeatString(10_000, "abc");

            Stopwatch watch1 = Stopwatch.StartNew();
            int[] integerArray1 = ConvertToIntArray_Normal(list);
            watch1.Stop();
            Console.WriteLine($"(1) List converted in {watch1.ElapsedMilliseconds} ms");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuMakerCSharp
{
    class Program
    {
        public static bool retail = false;
        public static bool hire = false;
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.Contains("retail"))
                    retail = true;
                else if (arg.Contains("hire"))
                    hire = true;
            }

            ProcessDirectory(Utils.GetLocalPath(retail && !hire ? "retail" : "hire"));
            Console.ReadLine();
        }

        public static void ProcessDirectory(string targetDirectory)
        {
            Console.WriteLine("Entering Directory '{0}'. ", targetDirectory);
            List<string> fileNames = new List<string>();
            int count = 0;
            if (Directory.Exists(targetDirectory))
            {
                fileNames = Directory.GetFiles(targetDirectory, "*.cs", SearchOption.AllDirectories).ToList();
                int fileCount = fileNames.Count();
                long fileSize = fileNames.Select(file => new FileInfo(file).Length).Sum();
            }

            foreach (var file in fileNames)
            {
                ProcessFile(file);
                count++;
            }

            Console.WriteLine("Processed {0} files", count.ToString());
            Console.ReadKey();
        }

        public static void ProcessFile(string path)
        {
            List<string> lines = File.ReadAllLines(path, Encoding.UTF8).ToList();

            foreach (var line in lines.Where(li => li.Contains("/// <summary>")))
            {
                int index = lines.IndexOf(line);
                Entities.DocuFunc functionObj = new Entities.DocuFunc();

                functionObj.summary = lines[index + 1].CleanString();
                functionObj.functionName = GetFunctionName(lines, index);
                functionObj.@params = GetParams(lines, index);
                functionObj.returns = GetReturns(lines, index);

                Console.WriteLine("Found function: {0}", functionObj.functionName);
            }

            Console.WriteLine("Processed file '{0}'.", path);
        }

        public static List<string> GetParams(List<string> lines, int index)
        {
            int paramsIndex = index + 3;
            bool isNotParam = false;
            List<string> @params = new List<string>();

            while (isNotParam == false)
            {
                try
                {
                    if (lines[paramsIndex].StartsWith("/// <param"))
                    {
                        @params.Add(lines[paramsIndex].CleanString());
                        paramsIndex++;
                    }
                    else
                    {
                        isNotParam = true;
                    }
                }
                catch (System.ArgumentOutOfRangeException e)
                {

                    Console.WriteLine("Index out of range: {0}", paramsIndex);
                    return @params;
                }


            }

            return @params;
        }

        public static string GetFunctionName(List<string> lines, int index)
        {
            int functionIndex = index + 3;
            bool isNotParam = false;
            string functionName = "";

            while (isNotParam == false)
            {
                try
                {
                    if (!lines[functionIndex].StartsWith("/// <param") && !lines[functionIndex].StartsWith("/// <remarks"))
                    {
                        isNotParam = true;
                        functionName = lines[functionIndex].CleanFunctionName();
                    }
                    else
                    {
                        functionIndex++;
                    }
                }
                catch (System.ArgumentOutOfRangeException e)
                {

                    Console.WriteLine("Index out of range: {0}", functionIndex);
                    return functionName;
                }
            }

            return functionName;
        }

        public static string GetReturns(List<string> lines, int index)
        {
            int returnsIndex = index + 3;
            bool isNotParam = false;
            string returnsString = "";

            while (isNotParam == false)
            {
                try
                {
                    if (lines[returnsIndex].StartsWith("/// <returns"))
                    {
                        isNotParam = true;
                        returnsString = lines[returnsIndex].CleanString();
                    }
                    else if (returnsIndex > index + 10)
                    {
                        isNotParam = true;
                    }
                    else
                    {
                        returnsIndex++;
                    }
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Index out of range: {0}", returnsIndex);
                    return returnsString;
                }
            }

            return returnsString;
        }
    }
}

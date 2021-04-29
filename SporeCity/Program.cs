using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SporeCity.Buildings;

namespace SporeCity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ICollection<(bool nearCenter, int[] neighboursIndexes)>? configuration;

            Console.WriteLine("Welcome to SporeCity emulator");
            Console.WriteLine("=============================");

            #region GetLayoutConfiguration

            while (true)
            {
                Console.WriteLine("Please enter your city layout or select from available");
                Console.WriteLine("=============================");
                Console.WriteLine("Layout format {[index of neighbours | 'C' for near center]}");
                Console.WriteLine("Example: {1,2,3};{2,3,C}");
                Console.WriteLine("=============================");
                Console.WriteLine("Available layouts:");
                Console.WriteLine("(s), Space Stage");
                Console.WriteLine("=============================");
                Console.Write(">:");
                var input = Console.ReadLine() ?? "";
                if (input.StartsWith("s"))
                {
                    configuration = new List<(bool nearCenter, int[] neighboursIndexes)>
                    {
                        (true, new[] {1, 5}),
                        (true, new[] {0, 6, 7}),
                        (true, new[] {7, 8}),
                        (true, new[] {8, 9}),
                        (true, new[] {5, 10}),
                        (false, new[] {0, 4, 10}),
                        (false, new[] {1}),
                        (false, new[] {1, 2}),
                        (false, new[] {2, 3, 9}),
                        (false, new[] {3, 8, 10}),
                        (false, new[] {4, 5, 9})
                    };
                    break;
                }

                if (!new Regex(@"^(?:{(?:[0-9Cc]+,)*[0-9Cc]+};)*(?:{(?:[0-9Cc]+,)*[0-9Cc]+})$").IsMatch(input ?? ""))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: layout is not valid");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    configuration = input!.Split(";")
                        .Select(place => place.Substring(1, place.Length - 2))
                        .Select(place =>
                        {
                            var parts = place.Split(",");
                            var indexes = parts.Where(i => int.TryParse(i, out _)).Select(int.Parse).ToArray();
                            var nearCenter = parts.Any(i => i.ToLower().Equals("c"));

                            return (nearCenter, indexes!);
                        })
                        .ToList();

                    break;
                }
            }

            int? minWork = null;
            int? minFun = null;
            CalculateMode mode;

            #endregion

            #region GetMode

            while (true)
            {
                Console.WriteLine("Please enter mode number of leave blank for default (0)");
                Console.WriteLine("=============================");
                Console.WriteLine("Available modes:");
                Console.WriteLine("(0), Maximize work, keeping moral > 0");
                Console.WriteLine("(1), Maximize work 24/7 :(");
                Console.WriteLine("(2), Maximize moral, keeping work > 0");
                Console.WriteLine("(3), No working at all :'))");
                Console.WriteLine("(4), Input minimum values for work and moral");
                Console.WriteLine("=============================");
                Console.Write(">:");
                var input = Console.ReadLine() ?? "0";

                input = input.Length > 0 ? input : "0";
                
                if (CalculateMode.TryParse(input, out mode))
                {
                    break;
                }

                if (mode == CalculateMode.MinValues)
                {
                    while (true)
                    {
                        Console.WriteLine("Please enter min value for work or leave blank");
                        Console.Write(">:");
                        input = Console.ReadLine() ?? "0";

                        if (int.TryParse(input, out var _minWork))
                        {
                            minWork = _minWork;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("Please enter min value for moral or leave blank");
                        Console.Write(">:");
                        input = Console.ReadLine() ?? "0";

                        if (int.TryParse(input, out var _minFun))
                        {
                            minFun = _minFun;
                            break;
                        }
                    }
                }
            }

            #endregion

            #region GetMaxBuildingCount

            var maxBuildingsCount = configuration.Count;

            while (false)
            {
                Console.WriteLine("Please max buildings count or leave for blank");
                Console.Write(">:");
                var input = Console.ReadLine() ?? configuration.Count.ToString();
                if (int.TryParse(input, out maxBuildingsCount))
                {
                    break;
                }
            }

            #endregion

            try
            {
                var generator = new Generator(configuration);
                // var (score, layout) = generator.TestLayout(new []
                // {
                //     BuildingType.House,
                //     BuildingType.Work,
                //     BuildingType.Work,
                //     BuildingType.Fun,
                //     BuildingType.Work,
                //     BuildingType.Work,
                //     BuildingType.House,
                //     BuildingType.House,
                //     BuildingType.House,
                //     BuildingType.Fun,
                //     BuildingType.House,
                // });
                var (score, layout) = generator.GetBestLayouts(mode, minWork, minFun);

                Console.WriteLine(score);
                Console.WriteLine(string.Join("\n", layout.Take(20).Select(tuple => tuple.layout + $"| {tuple.price}$$")));
            }
            catch (IndexOutOfRangeException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "Error: layout contains wrong indexes, check and try again. Remark: indexes starts from 0, not 1");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("");
            Console.WriteLine("To exit press any key...");
            Console.ReadLine();
        }
    }
}
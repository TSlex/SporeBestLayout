using System;
using System.Collections.Generic;
using SporeCity.Buildings;

namespace SporeCity
{
    class Program
    {
        static void Main(string[] args)
        {
            GetBestLayout();
            GetBestLayout(CalculateMode.MaximizeWorkForce);
            GetBestLayout(CalculateMode.MaximizeFun);
            GetBestLayout(CalculateMode.MaximizeExtraFun);
        }

        private static void GetBestLayout(CalculateMode mode = CalculateMode.MaximizeWork, int? minWork = null,
            int? minMoral = null, bool logCount = false)
        {
            ((int work, int moral) score, CityConfiguration? configuration) maxScore = ((0, 0), null);

            const int first = 1;
            const int second = 4;

            var counter = 0;

            for (var n1 = first; n1 < second; n1++)
            {
                for (var n2 = first; n2 < second; n2++)
                {
                    for (var n3 = first; n3 < second; n3++)
                    {
                        for (var n4 = first; n4 < second; n4++)
                        {
                            for (var n5 = first; n5 < second; n5++)
                            {
                                for (var n6 = first; n6 < second; n6++)
                                {
                                    for (var n7 = first; n7 < second; n7++)
                                    {
                                        for (var n8 = first; n8 < second; n8++)
                                        {
                                            for (var n9 = first; n9 < second; n9++)
                                            {
                                                for (var n10 = first; n10 < second; n10++)
                                                {
                                                    for (var n11 = first; n11 < second; n11++)
                                                    {
                                                        var configuration = new CityConfiguration(new List<BuildingType>
                                                        {
                                                            (BuildingType) n1,
                                                            (BuildingType) n2,
                                                            (BuildingType) n3,
                                                            (BuildingType) n4,
                                                            (BuildingType) n5,
                                                            (BuildingType) n6,
                                                            (BuildingType) n7,
                                                            (BuildingType) n8,
                                                            (BuildingType) n9,
                                                            (BuildingType) n10,
                                                            (BuildingType) n11,
                                                        });

                                                        var result = new City(configuration).Calculate();
                                                        var save = false;

                                                        switch (mode)
                                                        {
                                                            case CalculateMode.MaximizeWorkForce:
                                                                save = result.work > maxScore.score.work;
                                                                break;
                                                            case CalculateMode.MaximizeFun:
                                                                save = result.work > 0 &&
                                                                       result.moral > maxScore.score.moral;
                                                                break;
                                                            case CalculateMode.MaximizeExtraFun:
                                                                save = result.moral > maxScore.score.moral;
                                                                break;
                                                            case CalculateMode.MinValues:
                                                                if (minWork.HasValue && minMoral.HasValue)
                                                                {
                                                                    save = result.moral >= minMoral.Value &&
                                                                           result.work >= minWork.Value &&
                                                                           result.work > maxScore.score.work;
                                                                }
                                                                else if (minWork.HasValue)
                                                                {
                                                                    save = result.work >= minWork.Value &&
                                                                           result.work > maxScore.score.work;
                                                                }
                                                                else if (minMoral.HasValue)
                                                                {
                                                                    save = result.moral >= minMoral.Value &&
                                                                           result.moral > maxScore.score.moral;
                                                                }

                                                                break;
                                                            default:
                                                                save = result.moral > 0 &&
                                                                       result.work > maxScore.score.work;
                                                                break;
                                                        }

                                                        if (save)
                                                        {
                                                            maxScore.score = result;
                                                            maxScore.configuration = configuration;
                                                        }

                                                        counter++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine(maxScore.configuration);
            Console.WriteLine(maxScore.score);

            if (logCount)
            {
                Console.WriteLine(counter);
            }
        }
    }
}
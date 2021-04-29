using System.Collections.Generic;
using System.Linq;
using SporeCity.Buildings;

namespace SporeCity
{
    public class Generator
    {
        private readonly int _buildingCount;
        public readonly City City;

        private (int work, int moral) _bestScore;
        private ICollection<(string layout, int price)> _bestLayouts;

        public int IterationCounter { get; private set; }

        private CalculateMode _mode;
        private int? _minWork;
        private int? _minMoral;

        public Generator(ICollection<(bool nearCenter, int[] neighboursIndexes)> configuration)
        {
            City = new City(new CityConfiguration(configuration));
            _buildingCount = configuration.Count;

            IterationCounter = 0;
            _bestLayouts = new List<(string layout, int price)>();
            _bestScore = (0, 0);
        }

        public ((int work, int moral) score, ICollection<(string layout, int price)>) GetBestLayouts(
            CalculateMode mode = CalculateMode.MaximizeWork,
            int? minWork = null, int? minMoral = null)
        {
            _mode = mode;
            _minWork = minWork;
            _minMoral = minMoral;

            CalculateBestLayoutRecursive(new BuildingType[_buildingCount], _buildingCount);

            return (_bestScore, _bestLayouts);
        }

        public (int work, int moral) TestLayout(IEnumerable<BuildingType> layout)
        {
            return City.Calculate(layout);
        }

        private void CalculateBestLayoutRecursive(BuildingType[] layout, int depth)
        {
            if (depth <= 0)
            {
                var score = TestLayout(layout);
                var save = false;

                switch (_mode)
                {
                    case CalculateMode.MaximizeWorkForce:
                        save = score.work > _bestScore.work;
                        break;
                    case CalculateMode.MaximizeFun:
                        save = score.work > 0 &&
                               score.moral > _bestScore.moral;
                        break;
                    case CalculateMode.MaximizeExtraFun:
                        save = score.moral > _bestScore.moral;
                        break;
                    case CalculateMode.MinValues:
                        if (_minWork.HasValue && _minMoral.HasValue)
                        {
                            save = score.moral >= _minMoral.Value &&
                                   score.work >= _minWork.Value &&
                                   score.work > _bestScore.work;
                        }
                        else if (_minWork.HasValue)
                        {
                            save = score.work >= _minWork.Value &&
                                   score.work > _bestScore.work;
                        }
                        else if (_minMoral.HasValue)
                        {
                            save = score.moral >= _minMoral.Value &&
                                   score.moral > _bestScore.moral;
                        }

                        break;
                    default:
                        save = score.moral > 0 &&
                               score.work > _bestScore.work;
                        break;
                }

                if (score.work == _bestScore.work && score.moral == _bestScore.moral)
                {
                    _bestLayouts.Add((City.ToString(), City.Price));
                }

                if (save)
                {
                    _bestScore = score;
                    _bestLayouts = new List<(string layout, int price)>
                    {
                        (City.ToString(), City.Price)
                    };
                }

                _bestLayouts = _bestLayouts.OrderBy(tuple => tuple.price).ToList();

                IterationCounter++;
            }
            else
            {
                for (var n = 1; n < 4; n++)
                {
                    var layoutCopy = new BuildingType[layout.Length];

                    for (var i = 0; i < layout.Length; i++)
                    {
                        layoutCopy[i] = layout[i];
                    }

                    layoutCopy[layout.Length - depth] = (BuildingType) n;

                    CalculateBestLayoutRecursive(layoutCopy, depth - 1);
                }
            }
        }
    }
}
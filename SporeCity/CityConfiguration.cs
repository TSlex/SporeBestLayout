using System;
using System.Collections.Generic;
using System.Linq;
using SporeCity.Buildings;

namespace SporeCity
{
    public class CityConfiguration
    {
        public CityConfiguration(IEnumerable<BuildingType> configuration)
        {
            var placement = new List<Building?>();

            //create placement
            foreach (var type in configuration)
            {
                Building? building = type switch
                {
                    BuildingType.Home => new Home(),
                    BuildingType.Work => new Work(),
                    BuildingType.Fun => new Fun(),
                    _ => null
                };

                placement.Add(building);
            }

            //configure buildings
            for (var i = 0; i < placement.Count; i++)
            {
                var building = placement[i];
                if (building == null) continue;

                switch (i + 1)
                {
                    case 1:
                        building.NearCenter = true;
                        building.Neighbors = new List<Building?>()
                        {
                            placement[1], placement[5]
                        };
                        break;
                    case 2:
                        building.NearCenter = true;
                        building.Neighbors = new List<Building?>()
                        {
                            placement[0], placement[6], placement[7]
                        };
                        break;
                    case 3:
                        building.NearCenter = true;
                        building.Neighbors = new List<Building?>()
                        {
                            placement[7], placement[8]
                        };
                        break;
                    case 4:
                        building.NearCenter = true;
                        building.Neighbors = new List<Building?>()
                        {
                            placement[8], placement[9]
                        };
                        break;
                    case 5:
                        building.NearCenter = true;
                        building.Neighbors = new List<Building?>()
                        {
                            placement[10], placement[5]
                        };
                        break;
                    case 6:
                        building.Neighbors = new List<Building?>()
                        {
                            placement[0], placement[4]
                        };
                        break;
                    case 7:
                        building.Neighbors = new List<Building?>()
                        {
                            placement[1]
                        };
                        break;
                    case 8:
                        building.Neighbors = new List<Building?>()
                        {
                            placement[1], placement[2]
                        };
                        break;
                    case 9:
                        building.Neighbors = new List<Building?>()
                        {
                            placement[2], placement[3]
                        };
                        break;
                    case 10:
                        building.Neighbors = new List<Building?>()
                        {
                            placement[8], placement[3], placement[10]
                        };
                        break;
                    case 11:
                        building.Neighbors = new List<Building?>()
                        {
                            placement[4], placement[5], placement[9]
                        };
                        break;
                }
            }

            Placement = placement;
        }

        public IEnumerable<Building?> Placement { get; }

        public override string ToString()
        {
            var result = new List<string>();

            foreach (var (building, index) in Placement.Select((building, i) => (building, i)))
            {
                result.Add($"[{index + 1}]: {building}");
            }

            return string.Join(", ", result);
        }
    }
}
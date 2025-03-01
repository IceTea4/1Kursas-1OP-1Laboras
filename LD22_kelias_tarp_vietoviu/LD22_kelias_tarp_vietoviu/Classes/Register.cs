using System.Collections.Generic;

namespace LD22_kelias_tarp_vietoviu
{
    /// <summary>
    /// Register class in which the main 
    /// information is stored
    /// </summary>
    public class Register
    {
        private List<Road> allRoads = new List<Road>();
        private List<string> cities { get; }
        public string start { get; }
        public string ending { get; }

        /// <summary>
        /// Gets cities, start and ending
        /// </summary>
        /// <param name="miestai"></param>
        /// <param name="start"></param>
        /// <param name="ending"></param>
        public Register(List<string> miestai, 
            string start, string ending)
        {
            cities = new List<string>();

            foreach (string city in miestai)
            {
                cities.Add(city);
            }

            this.start = start;
            this.ending = ending;
        }

        /// <summary>
        /// Method adds road to the allRoads list
        /// </summary>
        /// <param name="road"></param>
        public void Add(Road road)
        {
            allRoads.Add(road);
        }

        /// <summary>
        /// Method returns the cities list
        /// </summary>
        /// <returns></returns>
        public List<string> GetCities()
        {
            return cities;
        }

        /// <summary>
        /// Method returns the roads list
        /// </summary>
        /// <returns></returns>
        public List<Road> GetRoads()
        {
            return allRoads;
        }
    }
}
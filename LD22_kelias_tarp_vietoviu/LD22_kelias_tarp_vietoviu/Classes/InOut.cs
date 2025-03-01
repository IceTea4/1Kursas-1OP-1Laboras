using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace LD22_kelias_tarp_vietoviu
{
    /// <summary>
    /// Reading and printing class
    /// </summary>
    public static class InOut
    {
        /// <summary>
        /// Reads the data from the given file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Register ReadTxt(string path)
        {
            string[] allLines = File.ReadAllLines(path);
            string pattern = "\\s+";

            string[] parts = allLines[0].Split(' ');
            int cityCount = int.Parse(parts[0]);
            int roadCount = int.Parse(parts[1]);

            List<string> cities = ReadCities(cityCount, 
                allLines);

            string[] matches = 
                Regex.Split(allLines[cityCount + 2], 
                pattern);
            string begining = matches[0];
            string ending = matches[1];

            Register register = new Register(cities, 
                begining, ending);

            for (int i = cityCount + 4; 
                i < roadCount + cityCount + 4; i++)
            {
                string[] line = Regex.Split(allLines[i], 
                    pattern);

                Road road = new Road(line[0], line[1], 
                    int.Parse(line[2]));

                register.Add(road);
            }

            return register;
        }

        /// <summary>
        /// Separately reads cities and returns the list
        /// </summary>
        /// <param name="cityCount"></param>
        /// <param name="allLines"></param>
        /// <returns></returns>
        private static List<string> ReadCities(int cityCount, 
            string[] allLines)
        {
            List<string> cities = new List<string>();

            for (int i = 1; i < cityCount + 1; i++)
            {
                cities.Add(allLines[i]);
            }

            return cities;
        }

        /// <summary>
        /// Prints a table to txt file with all the cities
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="register"></param>
        public static void PrintCitiesTxt(string fileName, 
            Register register)
        {
            File.AppendAllText(fileName, 
                "Pradiniai duomenys:\r\n", Encoding.UTF8);

            List<string> lines = new List<string>();

            lines.Add(new string('-', 21));
            lines.Add(String.Format($"| " +
                $"{"Galimos vietovės",-17} |"));
            lines.Add(new string('-', 21));

            foreach (string city in register.GetCities())
            {
                lines.Add(String.Format($"| {city,-17} |"));
                lines.Add(new string('-', 21));
            }

            File.AppendAllLines(fileName, lines, 
                Encoding.UTF8);
        }

        /// <summary>
        /// Prints the table to txt file of all possible roads
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="register"></param>
        public static void PrintRoadsTxt(string fileName, 
            Register register)
        {
            List<string> lines = new List<string>();

            lines.Add("");
            lines.Add(new string('-', 43));
            lines.Add(String.Format($"| {"Pradžia",-11} " +
                $"| {"Pabaiga",-11} | {"Atstumas",-8} km |"));
            lines.Add(new string('-', 43));

            foreach (Road road in register.GetRoads())
            {
                lines.Add(road.ToString());
                lines.Add(new string('-', 43));
            }

            lines.Add("");

            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Prints the rezults to txt file in a table
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="road"></param>
        public static void PrintRezultTxt(string fileName, 
            List<Road> road)
        {
            File.AppendAllText(fileName, "Rezultatai:\r\n", 
                Encoding.UTF8);

            List<string> lines = new List<string>();

            lines.Add(new string('-', 43));
            lines.Add(String.Format("| {0,-39} |", "Minimalus atstumas tarp vietovių"));
            lines.Add(new string('-', 43));
            lines.Add(String.Format($"| {"Pradžia",-11} " +
                $"| {"Pabaiga",-11} | {"Atstumas",-8} km |"));
            lines.Add(new string('-', 43));

            lines.Add(String.Format($"| {road[0].Start,-11} " +
                $"| {road[road.Count - 1].End,-11} | " +
                $"{TaskUtils.Distance(road),8} km |"));
            lines.Add(new string('-', 43));

            lines.Add(String.Format("| {0,-39} |", "Trasa eina per vietoves"));
            lines.Add(new string('-', 43));

            lines.Add(String.Format($"| {road[0].Start,-39} |"));
            lines.Add(new string('-', 43));

            foreach (Road r in road)
            {
                lines.Add(String.Format($"| {r.End,-39} |"));
                lines.Add(new string('-', 43));
            }

            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }
    }
}
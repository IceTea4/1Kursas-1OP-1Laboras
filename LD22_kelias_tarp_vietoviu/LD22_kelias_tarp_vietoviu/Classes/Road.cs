using System;

namespace LD22_kelias_tarp_vietoviu
{
    /// <summary>
    /// Constructor class
    /// </summary>
    public class Road
    {
        public string Start {  get; }
        public string End { get; }
        public int Distance { get; }

        public Road(string start, 
            string end, int distance)
        {
            this.Start = start;
            this.End = end;
            this.Distance = distance;
        }

        public override string ToString()
        {
            string line;

            line = String.Format($"| {this.Start,-11} " +
                    $"| {this.End,-11} | {this.Distance,8} km |");

            return line;
        }
    }
}
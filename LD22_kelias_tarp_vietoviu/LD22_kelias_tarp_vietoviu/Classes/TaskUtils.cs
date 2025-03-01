using System.Collections.Generic;

namespace LD22_kelias_tarp_vietoviu
{
    /// <summary>
    /// Class contains calculations and recursion
    /// </summary>
    public class TaskUtils
    {
        /// <summary>
        /// Method with recursion to solve the task
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="register"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static List<Road> Travel(string start, 
            string end, Register register, int length)
        {
            if (length > 5)
            {
                return null;
            }

            List<Road> path = null;
            int distance = -1;

            foreach (Road kelias in register.GetRoads())
            {
                List<Road> subpath;

                if (kelias.Start == start 
                    && kelias.End == end)
                {
                    subpath = new List<Road>();
                    subpath.Add(kelias);
                }
                else if (kelias.Start == start)
                {
                    subpath = Travel(kelias.End, end, 
                        register, length + 1);

                    if (subpath == null)
                    {
                        continue;
                    }

                    subpath.Insert(0, kelias);
                }
                else
                {
                    continue;
                }

                int subdistance = Distance(subpath);

                if (distance < 0 || distance 
                    > subdistance)
                {
                    distance = subdistance;
                    path = subpath;
                }
            }

            return path;
        }

        /// <summary>
        /// Method calculates the distance 
        /// between all given roads
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int Distance(List<Road> path)
        {
            int distance = 0;

            foreach (Road road in path)
            {
                distance += road.Distance;
            }

            return distance;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brownian_motion
{
    class StaticItems
    {
        public static double[] staticX_arr;
        public static double[] staticY_arr;
        public static int time;


        public StaticItems(double[] x_arr, double[] y_arr)
        {
            staticX_arr = x_arr;
            staticY_arr = y_arr;
           
        }

    }
}

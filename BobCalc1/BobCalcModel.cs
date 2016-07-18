using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobCalc1
{
    public static class BobCalcModel
    {
        public static double FirstOperator { get; set; }
        public static double SecondOperator { get; set; }

        public static double Total { get; set; }

        public static int Operation { get; set; }

        enum currentOperation
        {
            Add, Subtract, Equals
        }
    }
}

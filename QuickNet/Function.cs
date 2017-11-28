using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNet
{
    public static class Function
    {
        public static double Sigmoid(double value) => 1 / (1 + Math.Exp(-value));

        public static double SigmoidDerivative(double value)
        {
            double s = Sigmoid(value);
            return s * (1 - s);
        }
    }
}

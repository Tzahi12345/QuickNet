using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNet
{
    public class Run
    {
        public List<double> results;
        public List<double> expected;
        public Network network;
        public List<double> input;

        public Run(Network theNetwork, List<double> theInput)
        {
            network = theNetwork;
            input = theInput;
        }
    }
}

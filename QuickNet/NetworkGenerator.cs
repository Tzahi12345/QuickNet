using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNet
{
    public static class NetworkGenerator
    {
        static void Gen()
        {
            Network network = MakeNetwork();
            GenerateWeights(network); // generates weights
        }


        static Network MakeNetwork()
        {
            Network network = new Network();
            network.AddLayer(10);
            network.AddLayer(5);
            network.AddLayer(2);
            return network;
        }

        public static void GenerateWeights(Network network)
        {
            for (int i = 0; i < network.Layers.Count; i++)
            {
                Layer layer = network.Layers[i];
                Layer nextLayer = network.Layers[i + 1];
                if (layer.IsLast== false) // if its not end, go through nodes and set weights
                {
                    for (int j = 0; j < layer.Nodes.Count; j++)
                    {
                        Node node = layer.Nodes[j];
                        node.GenerateRandomWeights(nextLayer.Nodes.Count);
                    }
                }
            }
        }

        public static double Cost_Single(Run run)
        {
            double sum = 0;
            for (int i = 0; i < run.results.Count; i++)
            {
                sum += (1/2) * Math.Pow(run.results[i]-run.expected[i],2); // adds individual cost
            }
            return sum;
        }

        public static double Cost_Multiple(List<Run> runs)
        {
            double sums = 0;
            for (int i = 0; i < runs.Count; i++)
            {
                sums += Cost_Single(runs[i]);
            }
            sums = sums / runs.Count; // gets average run cost
            return sums;
        }
    }
}

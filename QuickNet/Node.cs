using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNet
{
    public class Node : Layer
    {
        public Layer layer;
        public float bias;
        new public Network network;
        public List<double> weights;
        public double Z;
        public double A;
        public bool compressed = true;
        public new int index;
        public Node(Layer theLayer, int nodeAmount, Network theNetwork, float theBias = 0) : base(nodeAmount, theNetwork)
        {
            layer = theLayer;
            bias = theBias;
            network = theNetwork;
            List<double> weights = new List<double>();
            index = layer.Nodes.Count;
        }

        public static Node New(Layer layer)
        {
            return new Node(layer, 0, layer.network);
        }

        public Layer GetLayer()
        {
            return layer;
        }

        public void GenerateRandomWeights(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Random ran = new Random();
                double val = ran.NextDouble();
                val = val * 2 - 1; // value is between -1 and 1
                weights.Add(val);
            }
        }

        public void CalculateValue() // calculates value of the node
        {
            if (layer.IsFirst == false)
            {
                Layer previousLayer = network.Layers[layer.index - 1];
                double sum = 0;
                for (int i = 0; i < previousLayer.Nodes.Count; i++)
                {
                    sum += previousLayer.Nodes[i].weights[index];
                }
                sum += bias;
                Z = sum;
                A = Function.Sigmoid(Z); 
            }
        }
    }
}

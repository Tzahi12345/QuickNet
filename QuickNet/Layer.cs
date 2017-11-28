using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNet
{
    public class Layer : Network
    {
        public List<Node> Nodes;
        public Network network;
        public int index;
        public Layer(int nodeAmount, Network theNetwork) : base()
        {
            Nodes = new List<Node>();
            for (int i = 0; i < nodeAmount; i++)
                Nodes.Add(Node.New(this)); // adds nodes to new layer
            network = theNetwork;
        }

        public void AddNode() => Nodes.Add(Node.New(this));

        public void AddNodes(int amount)
        {
            for (int i = 0; i < amount; i++)
                AddNode();
        }

        public bool IsLast
        {
            get
            {
                if (this == network.Layers.Last())
                    return true;
                else
                    return false;
            }
        }

        public bool IsFirst
        {
            get
            {
                if (this == network.Layers.First())
                    return true;
                else
                    return false;
            }
        }

        public static void UpdateIndeces(Network network)
        {
            for (int i = 0; i < network.Layers.Count; i++)
            {
                network.Layers[i].index = i;
            }
        }
    }
}

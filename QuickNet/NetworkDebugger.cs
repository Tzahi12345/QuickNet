using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace QuickNet
{
    public static class NetworkDebugger
    {

        public static void FullSuite()
        {
            NetworkEmpty();
            NetworkSingleLayer();
            NetworkMultiLayer();
            AddNodeSingle();
            AddNodeMulti();
        }


        public static void NetworkEmpty()
        {
            Network TestNetwork = new Network();
            if (TestNetwork.Layers.Count == 0)
            {
                Debug.WriteLine("GOOD: Network Empty");
            }
            else
            {
                Debug.WriteLine("BAD: Network Empty");
            }
        }

        public static void NetworkSingleLayer()
        {
            Network TestNetwork = new Network();
            TestNetwork.AddLayer(32, 0);
            bool test1 = TestNetwork.Layers.Count == 1;
            bool test2 = TestNetwork.Layers[0].Nodes.Count == 32;
            if (test1 && test2)
            {
                Debug.WriteLine("GOOD: Network Single Layer");
            }
            else
            {
                Debug.WriteLine("BAD: Network Single Layer");
            }
        }

        public static void NetworkMultiLayer()
        {
            Network TestNetwork = new Network();
            TestNetwork.AddLayer(32);
            TestNetwork.AddLayer(64);
            TestNetwork.AddLayer(96);
            bool test1 = TestNetwork.Layers.First().Nodes.Count == 32;
            bool test2 = TestNetwork.Layers[1].Nodes.Count == 64;
            bool test3 = TestNetwork.Layers.Count == 3;
            if (test1 && test2 && test3)
            {
                Debug.WriteLine("GOOD: Network Multi Layer");
            }
            else
            {
                Debug.WriteLine("BAD: Network Multi Layer");
            }
        }

        public static void AddNodeSingle()
        {
            Network TestNetwork = new Network();
            TestNetwork.AddLayer();
            TestNetwork.Layers[0].AddNode();
            bool test1 = TestNetwork.Layers[0].Nodes.Count == 1;
            if (test1)
            {
                Debug.WriteLine("GOOD: Add Node Single");
            }
            else
            {
                Debug.WriteLine("BAD: Add Node Single");
            }
        }

        public static void AddNodeMulti()
        {
            Network TestNetwork = new Network();
            TestNetwork.AddLayer();
            TestNetwork.Layers[0].AddNodes(30);
            bool test1 = TestNetwork.Layers[0].Nodes.Count == 30;
            if (test1)
            {
                Debug.WriteLine("GOOD: Add Node Multi");
            }
            else
            {
                Debug.WriteLine("BAD: Add Node Multi");
            }
        }
    }
}

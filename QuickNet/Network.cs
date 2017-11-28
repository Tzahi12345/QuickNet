using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNet
{
    public class Network
    {
        public static int Count;
        public int number;
        public List<Layer> Layers = new List<Layer>();
        public Network()
        {
            Count++;
            number = Count;
        }

        public void AddLayer(int nodeAmount = 0, int index = -1) // type can be input, hidden, output
        {
            if (index < 0 || index > Layers.Count) // if its out of bounds
            {
                index = Layers.Count;
            }
            Layers.Insert(index, new Layer(nodeAmount, this));
            Layer.UpdateIndeces(network: this);
        }

        public int GetLayerIndex(Layer layer)
        {
            int index = -1;
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i] == layer)
                    index = i;
            }
            return index;
        }
        
    }
}

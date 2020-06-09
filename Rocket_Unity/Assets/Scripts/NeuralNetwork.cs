using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork
{
    System.Random rand = new System.Random();
    public List<Layer> layers;

    public NeuralNetwork()
    {
        this.layers = new List<Layer>();
    }

    public NeuralNetwork DeepCopy()
    {
        NeuralNetwork other = (NeuralNetwork)this.MemberwiseClone();
        other.layers = this.layers;
        return other;
    }


    public double[] Feedforward(double[] inputArr)
    {
        Matrix n1 = Matrix.FromArray(inputArr);
        for (int i = 0; i < this.layers.Count; i++)
        {
            Matrix n2 = layers[i].Weights;
            Matrix output = Matrix.Multiply(n2, n1);
            output.Add(layers[i].Bias); // Apply Bias
            output.Map(Matrix.Sigmoid); // Map Sigmoid function to each element
            n1 = output;
        }
        return Matrix.ToArray(n1);
    }

    public void AddLayer(Layer Object)
    {
        if (Object is Layer)
        {
            if (!Convert.ToBoolean(Object.InputDim) && this.layers.Count == 0)
            {
                Debug.LogException(new Exception("ERROR: first layer has to have 'InputDim=*int*' argument"));
            }
            else if (this.layers.Count > 0 && Convert.ToBoolean(Object.InputDim))
            {
                Debug.LogException(new Exception("ERROR: only the first layer should have 'InputDim=*int*' argument"));
            }
            else if (Convert.ToBoolean(Object.InputDim) && this.layers.Count == 0)
            {
                Object.Weights = new Matrix(Object.Nodes, Object.InputDim);
                Object.Bias = new Matrix(Object.Nodes, 1);
                this.layers.Add(Object);
            }
            else
            {
                Object.Weights = new Matrix(Object.Nodes, this.layers[this.layers.Count - 1].Nodes);
                this.layers.Add(Object);
            }
        }
         else
        {
            Debug.LogException(new Exception("You can only Add Layer type objects"));
        }
    }

    public void Mutate(double rate)
    {
        double mutate(double n)
        {
            if (rand.NextDouble() * 2 - 1 < rate)
            {
                return rand.NextDouble() * 2 - 1;
            }
            else
            {
                return n;
            }
        }

        for (int i = 0; i < this.layers.Count; i++)
        {
            this.layers[i].Weights.Map(mutate(0.1));
            this.layers[i].Bias.Map(mutate(0.1));
        }
    }
}

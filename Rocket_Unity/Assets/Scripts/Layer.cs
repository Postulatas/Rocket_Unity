using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer
{
    public int Nodes, InputDim;
    public Matrix Bias, Weights;
    public string str;

    public Layer(int Nodes, bool UseBias = true)
    {
        this.Nodes = Nodes;
        if (UseBias)
        {
            this.Bias = new Matrix(this.Nodes, 1);
        }
        else Bias = null;
    }
    public Layer(int Nodes, int InputDim)
    {
        this.Nodes = Nodes;
        this.InputDim = InputDim;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix
{
    static System.Random rand = new System.Random();
    public int rows, cols;
    public double[,] data;

    public Matrix(int rows, int cols, double start = -1, double end = 1)
    {
        this.rows = rows;
        this.cols = cols;
        this.data = new double[rows, cols];
        FillData(start, end);
    }

    public void MatrixDisplay()
    {
        int rowLength = this.data.GetLength(0);
        int colLength = this.data.GetLength(1);

        List<double> rowList = new List<double>();

        for (int row = 0; row < rowLength; row++)
        {
            for (int col = 0; col < colLength; col++)
            {
                rowList.Add(Math.Round(this.data[row, col], 3));
            }
            Debug.Log(String.Join(" | ", rowList));
            rowList.Clear();
        }
    }

    // STATIC METHODS

    public static Matrix Multiply(Matrix a, Matrix b)
    {
        Matrix result = new Matrix(a.rows, b.cols);
        if (a.cols == b.rows)
        {
            for (int row = 0; row < result.rows; row++)
            {
                for (int col = 0; col < result.cols; col++)
                {
                    double sum = 0;
                    for (int i = 0; i < a.cols; i++)
                    {
                        sum += a.data[row, i] * b.data[i, col];
                    }
                    result.data[row, col] = sum;
                }
            }
        }
        return result;
    }

    public Matrix Add(Matrix mtrx)
    {
        for (int row = 0; row < mtrx.rows; row++)
        {
            for (int col = 0; col < mtrx.cols; col++)
            {
                this.data[row, col] += mtrx.data[row, col];
            }
        }
        return this;
    }

    public Matrix Map(Func<double, double> func)
        {
            for (int row = 0; row < this.rows; row++)
            {
                for (int col = 0; col < this.cols; col++)
                {
                    this.data[row, col] = func(this.data[row, col]);
                }
            }
            return this;
        }

    public static Matrix FromArray(double[] arr)
    {
        Matrix result = new Matrix(arr.Length, 1);
        for (int i = 0; i < arr.Length; i++)
        {
            result.data[i, 0] = arr[i];
        }
        return result;
    }

    public static double[] ToArray(Matrix mtrx)
    {
        double[] result = new double[mtrx.data.GetLength(0)];
        for (int i = 0; i < mtrx.data.GetLength(0); i++)
        {
            result[i] = mtrx.data[i, 0];
        }
        return result;
    }

    // CONSTRUCTOR FUNCTION
    private void FillData(double start, double end)
    {
        for (int row = 0; row < this.rows; row++)
        {
            for (int col = 0; col < this.cols; col++)
            {
                this.data[row, col] = (rand.NextDouble() * (end - start)) + start; // NextDouble() by default returns between 0 and 1
            }
        }
    }

    // OUTER FUNCTIONS
    public static double Sigmoid(double n)
    {
        return 1.0 / (1.0 + Math.Exp(-n));
    }

    public static double Mutate(double n)
    {
        return n * rand.NextDouble() * 2 - 1;
    }
}

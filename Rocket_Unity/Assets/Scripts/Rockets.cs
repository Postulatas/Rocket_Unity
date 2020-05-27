using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockets : MonoBehaviour
{
    System.Random random = new System.Random();
    public GameObject Prefab;
    public float timeframe;
    public float GameSpeed = 1f;
    public List<Genetic> rockets;
    public List<NeuralNetwork> neural;
    public int pop = 50;
    public NeuralNetwork dn = new NeuralNetwork();
    public bool dead = false;


    void Start()
    {
        CreateNet();
        InvokeRepeating("CreateRocket", 0.1f, timeframe);
        dn.Mutate();
    }

    public void CreateNet()
    {    
        neural = new List<NeuralNetwork>();
            for (int i = 0; i < pop; i++)
            {
                NeuralNetwork nn = new NeuralNetwork();
                nn.AddLayer(new Layer(7, 14));
                nn.AddLayer(new Layer(2));
                neural.Add(nn);
            }
        }

    public void CreateRocket()
    {
        Time.timeScale = GameSpeed;
        if (rockets.Count > 0)
        {
            for (int i = 0; i < rockets.Count; i++)
            {
                if (rockets[i].collided==false)
                {
                    GameObject.Destroy(rockets[i].gameObject);
                }                
            }
            EditFit();
        }
 
        rockets = new List<Genetic>();
        for (int i = 0; i < pop; i++)
        {
            Vector3 pos = new Vector3(0, 39, 0);
            Genetic rocket = (Instantiate(Prefab, pos, Quaternion.identity)).GetComponent<Genetic>();
            rocket.nn = neural[i];
            rockets.Add(rocket);
        }
    }
    public void EditFit()
    {
        for (int i = 0; i < pop; i++)
        {
            rockets[i].UpdateFit();
            rockets[i].fitness = i;
        }

        double temp, check = 1;
        int p = pop;
        while (check == 1 || (p > 1))
        {
            check = 0;
            p = (p + 1) / 2;
            for (int i = 0; i < (pop - p); i++)
            {
                if (rockets[i + p].fitness > rockets[i].fitness)
                {
                    temp = rockets[i + p].fitness;
                    rockets[i + p].fitness = rockets[i].fitness;
                    rockets[i].fitness = temp;
                    check = 1;
                }
            }
        }

        Crossover();
        for (int i = 0; i < pop; i++)
        {
            NeuralNetwork newNetwork = dn.DeepCopy();
            dn.Mutate();
        }      
    }

    public void Crossover()
    {
        double[][][][] NewGenes = new double[][][][] { };
        double[][][][] ReverseGenes = new double[][][][] { }; ;

        for (int i = 0; i < 50 / 2; i++)
        {
            for (int j = 0; j < dn.layers.Count; j++)
            {
                int rows = dn.layers[j].Weights.data.GetLength(0);
                int cols = dn.layers[j].Weights.data.GetLength(1);
                for (int k = 0; k < rows; k++)
                {
                    if (random.NextDouble() <= 0.5)
                    {
                        for (int L = 0; L < cols; L++)
                        {
                            NewGenes[i][j][k][L] = rockets[i].nn.layers[j].Weights.data[k, L];
                            ReverseGenes[i][j][k][L] = rockets[i + 1].nn.layers[j].Weights.data[k, L];
                        }
                    }

                    else
                    {
                        for (int L = 0; L < cols; L++)
                        {
                            NewGenes[i][j][k][L] = rockets[i].nn.layers[j].Weights.data[k, L];
                            ReverseGenes[i][j][k][L] = rockets[i + 1].nn.layers[j].Weights.data[k, L];
                        }
                    }
                }

            }
        }
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < dn.layers.Count; j++)
            {
                int rows = dn.layers[j].Weights.data.GetLength(0);
                int cols = dn.layers[j].Weights.data.GetLength(1);
                for (int k = 0; k < rows; k++)
                {
                    for (int L = 0; L < cols; L++)
                    {
                        if (i < 50 / 2)
                        {
                            rockets[i].nn.layers[j].Weights.data[k, L] = NewGenes[i][j][k][L];
                        }
                        if (i >= 50 / 2)
                        {
                            rockets[i].nn.layers[j].Weights.data[k, L] = ReverseGenes[i][j][k][L];
                        }
                    }                    
                    
                }
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockets : MonoBehaviour
{
    System.Random random = new System.Random();
    public GameObject Prefab;
    public NeuralNetwork dn = new NeuralNetwork();
    public List<Genetic> rockets;
    public List<NeuralNetwork> neural;
    public List<Genetic> temp;
    public List<NeuralNetwork> cop;
    public float timeframe;
    public float GameSpeed = 1f;
    public int pop;
    

    void Start()
    {
        CreateNet();
        InvokeRepeating("CreateRocket", 0.1f, timeframe);
    }

    public void CreateNet()
    {    
        neural = new List<NeuralNetwork>();
        cop = new List<NeuralNetwork>();
        for (int i = 0; i < pop; i++)
        {               
            NeuralNetwork nn = new NeuralNetwork();
            nn.AddLayer(new Layer(7, 14));
            nn.AddLayer(new Layer(2));
            neural.Add(nn);
            cop.Add(nn);
            //sukuria list neuroniniu tinklu ir taip pat kopija ju
        }
    }

    public void CreateRocket()
    {
        Time.timeScale = GameSpeed;
        if (rockets.Count > 0)
        {
            EditFit();
            Crossover();
            for (int i = 0; i < pop; i++)
            {
                cop[i]=neural[i].DeepCopy();
            }
            for (int i = 0; i < rockets.Count; i++)
            {
                if (rockets[i].dead==false)
                {
                    GameObject.Destroy(rockets[i].gameObject);
                    //sunaikina visus objektus po tam tikro laiko
                }                
            }
            for (int i = 0; i < pop; i++)
            {
                neural[i] = cop[i];
                neural[i].Mutate();
            }
            //EditFit();
        }
 
        rockets = new List<Genetic>();
        temp = new List<Genetic>();
        for (int i = 0; i < pop; i++)
        {
            Vector3 pos = new Vector3(0, 39, 0);
            Genetic rocket = (Instantiate(Prefab, pos, Quaternion.identity)).GetComponent<Genetic>();
            rocket.nn = neural[i];
            rockets.Add(rocket);
            temp.Add(rocket);
            //sukuria list raketu ir kiekvienai priskiria tinkla, taip pat sukuriama kopija
        }    
    }
    public void EditFit()
    {
        int check = 1;
        int p = pop;
        while (check == 1 || (p > 1))
        {
            check = 0;
            p = (p + 1) / 2;
            for (int i = 0; i < (pop - p); i++)
            {
                if (rockets[i + p].fitness > rockets[i].fitness)
                {
                    temp[i] = rockets[i + p];
                    rockets[i + p] = rockets[i];           
                    rockets[i] = temp[i];
                    check = 1;
                    //surusiojami tinklai mazejimo tvarka
                }
            }
        }
        //Crossover();
        //for (int i = 0; i < pop; i++)
        //{
            //NeuralNetwork newNetwork = dn.DeepCopy();
            //neural[i].DeepCopy();
            //neural[i].Mutate();
        //}      
    }

    public void Crossover()
    {
        double[][][][] NewGenes = new double[][][][] { };
        double[][][][] ReverseGenes = new double[][][][] { }; ;

        for (int i = 0; i < pop / 2; i++)
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
                            NewGenes[i][j][k][L] = rockets[i+1].nn.layers[j].Weights.data[k, L];
                            ReverseGenes[i][j][k][L] = rockets[i].nn.layers[j].Weights.data[k, L];
                        }
                        //naudoja arba vienos raketos weight grupe pirma ir kitos raketos grupe antra arba atvirksciai
                    }
                }

            }
        }
        for (int i = 0; i < pop; i++)
        {
            for (int j = 0; j < dn.layers.Count; j++)
            {
                int rows = dn.layers[j].Weights.data.GetLength(0);
                int cols = dn.layers[j].Weights.data.GetLength(1);
                for (int k = 0; k < rows; k++)
                {
                    for (int L = 0; L < cols; L++)
                    {
                        if (i < pop / 2)
                        {
                            rockets[i].nn.layers[j].Weights.data[k, L] = NewGenes[i][j][k][L];
                        }
                        if (i >= pop / 2)
                        {
                            rockets[i].nn.layers[j].Weights.data[k, L] = ReverseGenes[i][j][k][L];
                        }
                        print(rockets[i].nn.layers[j].Weights.data[k, L]);
                    }                    
                    
                }
            }
        }
    }

}

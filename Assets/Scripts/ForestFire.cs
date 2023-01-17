using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestFire : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64; //-1024 pixels
    private static int SCREEN_HEIGHT = 48;//-768 pixels
    public float speed = 0.1f;
    private float timer = 0;
    Tree[,] grid = new Tree[SCREEN_WIDTH, SCREEN_HEIGHT];
    public Tree prefab;
    public bool run;
    public float chanceToStartAlive = 45;

    int ToCatchMin;
    int ToCatchMax;
    int ToBurnDownMin;
    int ToBurnDownMax;

    public InputField IFspeed;
    public InputField IFchance;

    public InputField IFToCatchMin;
    public InputField IFToCatchMax;

    public InputField IFToBurnDownMin;
    public InputField IFToBurnDownMax;

   

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            if (timer >= speed)
            {
                timer = 0;
                //CountNeighbors();
                CountBurningNeighbours();
                PopulationControl();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    public void Play()
    {
        speed = float.Parse(IFspeed.text);
        run = true;
    }

    public void Pause()
    {
        run = false;
    }

    public void Reset()
    {
        ToCatchMin = int.Parse(IFToCatchMin.text);
        ToCatchMax = int.Parse(IFToCatchMax.text);
        ToBurnDownMin = int.Parse(IFToBurnDownMin.text);
        ToBurnDownMax = int.Parse(IFToBurnDownMax.text);

        run = false;
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                grid[x, y].SetAlive(true);
                grid[x, y].SetOnFire(false);
                grid[x, y].burningTime = UnityEngine.Random.Range(50, 100);
                grid[x, y].timeNeededToCatchOnFire = UnityEngine.Random.Range(0, 100);
            }
        }
    }

    public void Random()
    {
        run = false;
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                //grid[x, y].SetAlive(false);
                grid[x, y].SetOnFire(RandomAliveCell());
            }
        }
    }

    bool RandomAliveCell()
    {
        chanceToStartAlive = float.Parse(IFchance.text);
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand < chanceToStartAlive)
        {
            return true;
        }
        return false;
    }

    public void PlaceCells()
    {
         ToCatchMin= int.Parse(IFToCatchMin.text);
         ToCatchMax = int.Parse(IFToCatchMax.text);
         ToBurnDownMin = int.Parse(IFToBurnDownMin.text);
         ToBurnDownMax = int.Parse(IFToBurnDownMax.text);
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Tree tree = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity) as Tree;
                grid[x, y] = tree;
                grid[x, y].SetAlive(true);
                grid[x, y].SetOnFire(false);
                grid[x, y].burningTime = UnityEngine.Random.Range(ToBurnDownMin, ToBurnDownMax);
                grid[x, y].timeNeededToCatchOnFire= UnityEngine.Random.Range(ToCatchMin, ToCatchMax);

                //grid[x, y].SetAlive(RandomAliveCell());
            }
        }
    }

    void PopulationControl()
    {

        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                //-Rules
                //-if the tree has 1 burning neighbours it starts burning

                if (grid[x, y].isAlive)
                {
                    grid[x, y].timeNeededToCatchOnFire--;
                    //Cell is Alive
                    if (grid[x, y].burningNeighbours >= 1 && !grid[x,y].isBurning && grid[x,y].timeNeededToCatchOnFire<1)
                    {
                        grid[x, y].SetOnFire(true);
                
                    }
                }
                else if (grid[x, y].isBurning)
                {
                    grid[x, y].burningTime--;
                }
                
                if (grid[x, y].burningTime < 1)
                {
                    grid[x, y].SetDead();
                }



            }
        }
    }

    void CountBurningNeighbours()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                int burningNeighbors = 0;

                //North
                if (y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x, y + 1].isBurning)
                    {
                        burningNeighbors++;
                    }
                }

                //East
                if (x + 1 < SCREEN_WIDTH)
                {
                    if (grid[x + 1, y].isBurning)
                    {
                        burningNeighbors++;
                    }
                }

                //South
                if (y - 1 >= 0)
                {
                    if (grid[x, y - 1].isBurning)
                    {
                        burningNeighbors++;
                    }
                }

                //West
                if (x - 1 >= 0)
                {
                    if (grid[x - 1, y].isBurning)
                    {
                        burningNeighbors++;
                    }
                }

                //NorthEast
                if (x + 1 < SCREEN_WIDTH && y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x + 1, y + 1].isBurning)
                    {
                        burningNeighbors++;
                    }
                }

                //NorthWest
                if (x - 1 >= 0 && y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x - 1, y + 1].isBurning)
                    {
                        burningNeighbors++;
                    }
                }

                //SouthEast
                if (x + 1 < SCREEN_WIDTH && y - 1 >= 0)
                {
                    if (grid[x + 1, y - 1].isBurning)
                    {
                        burningNeighbors++;
                    }
                }

                //SouthWest
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    if (grid[x - 1, y - 1].isBurning)
                    {
                        burningNeighbors++;
                    }
                }

                grid[x, y].burningNeighbours = burningNeighbors;
            }

        }
      
    }

    void CountNeighbors()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                int numNeighbors = 0;

                //North
                if (y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x, y + 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //East
                if (x + 1 < SCREEN_WIDTH)
                {
                    if (grid[x + 1, y].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //South
                if (y - 1 >= 0)
                {
                    if (grid[x, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //West
                if (x - 1 >= 0)
                {
                    if (grid[x - 1, y].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //NorthEast
                if (x + 1 < SCREEN_WIDTH && y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x + 1, y + 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //NorthWest
                if (x - 1 >= 0 && y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x - 1, y + 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //SouthEast
                if (x + 1 < SCREEN_WIDTH && y - 1 >= 0)
                {
                    if (grid[x + 1, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //SouthWest
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    if (grid[x - 1, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                grid[x, y].numNeighbors = numNeighbors;
            }
        }
    }
}

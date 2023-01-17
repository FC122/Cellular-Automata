using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64; //-1024 pixels
    private static int SCREEN_HEIGHT = 48;//-768 pixels
    public float speed = 0.1f;
    private float timer = 0;
    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];
    public Cell prefab;
    public bool run;
    public float chanceToStartAlive = 45;

    // Start is called before the first frame update
    void Start()
    {
        PlaceCells();
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            if (timer >= speed)
            {
                timer = 0;
                CountNeighbors();
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
        run = true;
    }

    public void Pause()
    {
        run = false;
    }

    public void Reset()
    {
        run = false;
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                grid[x, y].SetAlive(false);
  
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
                grid[x, y].SetAlive(RandomAliveCell());
            }
        }
    }

    bool RandomAliveCell()
    {
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand < chanceToStartAlive)
        {
            return true;
        }
        return false;
    }

    private void PlaceCells()
    {
        for (int y = 0; y < SCREEN_HEIGHT;y++)
        {
            for(int x=0;x<SCREEN_WIDTH;x++)
            {
                Cell cell = Instantiate(prefab, new Vector3(x, y,0), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(false);
                //grid[x, y].SetAlive(RandomAliveCell());
            }
        }
    }
  
    void PopulationControl()
    {
        for(int y=0;y<SCREEN_HEIGHT;y++)
        {
            for(int x=0;x<SCREEN_WIDTH;x++)
            {
                //-Rules
                //-Any live cell with 2 or 3 live neighbours survives
                //-Any dead cell with 3 live neighbors becomes a live cell
                //-All other live cells die in the next generation and all other dead cells stay dead
               
                if(grid[x,y].isAlive)
                {
                    //Cell is Alive
                    if(grid[x,y].numNeighbors !=2 && grid[x,y].numNeighbors!=3)
                    {
                        grid[x, y].SetAlive(false);
                    }
                }
                else
                {
                    //Cell is Dead
                    if(grid[x,y].numNeighbors==3)
                    {
                        grid[x, y].SetAlive(true);
                    }
                }
            }
        }
    }

    void CountNeighbors()
    {
        for(int y=0;y<SCREEN_HEIGHT;y++)
        {
            for(int x=0;x<SCREEN_WIDTH;x++)
            {
                int numNeighbors = 0;

                //North
                if(y+1<SCREEN_HEIGHT)
                {
                    if(grid[x,y+1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //East
                if(x+1<SCREEN_WIDTH)
                {
                    if(grid[x+1,y].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //South
                if(y-1>=0)
                {
                    if(grid[x,y-1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //West
                if(x-1>=0)
                {
                    if(grid[x-1,y].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //NorthEast
                if(x+1<SCREEN_WIDTH && y+1<SCREEN_HEIGHT)
                {
                    if(grid[x+1,y+1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //NorthWest
                if(x-1>=0 && y+1<SCREEN_HEIGHT)
                {
                    if(grid[x-1,y+1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //SouthEast
                if(x+1<SCREEN_WIDTH && y-1>=0)
                {
                    if(grid[x+1,y-1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //SouthWest
                if(x-1>=0 && y-1>=0)
                {
                    if(grid[x-1,y-1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                grid[x, y].numNeighbors = numNeighbors;
            }
        }
    }

 
}

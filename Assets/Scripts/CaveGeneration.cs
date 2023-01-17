using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaveGeneration : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64; //-1024 pixels
    private static int SCREEN_HEIGHT = 48;//-768 pixels
    public float speed = 0.1f;
    private float timer = 0;
    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];
    public Cell prefab;
    public bool run;
    public float chanceToStartAlive = 45;
    public int deathLimit = 4;
    public int birthLimit = 3;
    public InputField IFdeathLimit;
    public InputField IFbirthLimit;
    public InputField IFchance;


    Cell[,] newGrid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];



    private void Start()
    {
        PlaceCells();
    }
    public void Update()
    {
       
    }

    public void Reset()
    {
        SceneManager.LoadScene("CaveGeneration");
    }

    private void PlaceCells()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(false);
                //grid[x, y].SetAlive(RandomAliveCell());
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

    public void GenerateCave()//na temelju generiranog random polja generira peæinu
    {
        chanceToStartAlive = float.Parse(IFchance.text);
        deathLimit = int.Parse(IFdeathLimit.text);
        birthLimit = int.Parse(IFbirthLimit.text);
        Random();
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity) as Cell;
                newGrid[x, y] = cell;
                int nbs = CountAliveNeighbours(x, y);
                if (grid[x, y].isAlive)
                {
                    if (nbs < deathLimit)
                    {
                        newGrid[x, y].SetAlive(false);
                    }
                    else
                    {
                        newGrid[x, y].SetAlive(true);
                    }
                }
                else
                {
                    if (nbs > birthLimit)
                    {
                        newGrid[x, y].SetAlive(true);
                    }
                    else
                    {
                        newGrid[x, y].SetAlive(false);
                    }
                }
            }
        }

        DestroyCells(grid);
    }

    public void DestroyCells(Cell[,] cells)
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                DestroyImmediate(cells[x, y]);
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

    int CountAliveNeighbours(int x, int y)
    {
        int count = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int neighbour_x = x + i;
                int neighbour_y = y + j;
                //If we're looking at the middle point
                if (i == 0 && j == 0)
                {
                    //Do nothing, we don't want to add ourselves in!
                }
                //In case the index we're looking at it off the edge of the map
                else if (neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= SCREEN_WIDTH || neighbour_y >= SCREEN_HEIGHT)
                {
                    count = count + 1;
                }
                //Otherwise, a normal check of the neighbour
                else if (grid[neighbour_x, neighbour_y].isAlive)
                {
                    count = count + 1;
                }
            }
        }
        return count;
    }
}

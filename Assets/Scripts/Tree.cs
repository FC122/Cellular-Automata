using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public bool isAlive = false;
    public int numNeighbors;
    public int burningNeighbours;
    Collider2D col;
    public int burningTime;
    public int timeNeededToCatchOnFire;
    public bool isBurning = false;

    
    public void SetAlive(bool alive)
    {
        isAlive = alive;
        if(alive)
        {
            GetComponent<SpriteRenderer>().color=Color.green;
        }
    }

    public void SetOnFire(bool onFire)
    {
        isBurning = onFire;
        if (isBurning)
        {
            isAlive = false;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void SetDead()
    {
        isBurning = false;
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void Start()
    {
        col = GetComponent<Collider2D>();
    }
    public void Update()
    {
       //ChangeState();
    }

    private void OnMouseDown()
    {
        if (!isBurning)
        {
            SetOnFire(true);
        }
        else
        {
            SetAlive(true);
        }
    }



    public void ChangeState()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Touch touch = Input.GetTouch(0);
            Collider2D touchColider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if(col==touchColider)
           
                    if (!isBurning)
                    {
                        SetOnFire(true);
                    }
                    else
                    {
                        SetAlive(true);
                    }
        }
    }
}

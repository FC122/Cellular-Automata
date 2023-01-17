using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive = false;
    public int numNeighbors;
    Collider2D col;
    public void SetAlive(bool alive)
    {
        isAlive = alive;
        if(alive)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
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
        if (!isAlive)
        {
            SetAlive(true);
        }
        else
        {
            SetAlive(false);
        }
    }

    public void ChangeState()
    {
        if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            Collider2D touchColider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position));
            if(col==touchColider)
            if(touch.phase==TouchPhase.Ended && Input.GetTouch(0).deltaTime <0.04)
            {
                    if (!isAlive)
                    {
                        SetAlive(true);
                    }
                    else
                    {
                        SetAlive(false);
                    }
                
            }
        }
    }
}

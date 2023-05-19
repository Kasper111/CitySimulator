using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public int x;
    public int y;
    public float angle;

    //public GameObject point;

    public Point(int x, int y, float angle)
    {
        this.x = x;
        this.y = y;
        this.angle = angle;

    }


    public int getX
    {
        get { return x; }
        set { x = value; }
    }

    public int getY
    {
        get { return y; }
        set { y = value; }
    }

    

    public float getAngle
    {
        get { return angle; }
    }
}

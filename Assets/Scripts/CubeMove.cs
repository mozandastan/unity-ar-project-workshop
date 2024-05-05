using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public int speed;

    public void moveRight()
    {
        transform.Translate(speed * Time.deltaTime,0,0);
    }
    public void moveLeft()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
}

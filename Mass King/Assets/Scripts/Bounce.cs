using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private float bounceHeight = 0.5f;
    private float speed = 1f;
    private float maxY;
    private float minY;
    private float yPos;

    private Vector3 pos;

    private bool goingUp = true;

    private void Awake()
    {
        pos = transform.position;
        yPos = transform.position.y;
        maxY = transform.position.y + bounceHeight;
        minY = transform.position.y - bounceHeight;
    }

    private void Update()
    {
        if(yPos >= maxY)
        {
            goingUp = false;
        }
        if(yPos <= minY)
        {
            goingUp = true;
        }

        if (goingUp)
        {
            yPos += speed * Time.deltaTime;
        }
        else
        {
            yPos -= speed * Time.deltaTime;
        }

        pos.y = yPos;
        transform.position = pos;



    }
}

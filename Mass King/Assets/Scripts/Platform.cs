using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformColor color;

    private void Awake()
    {
        CheckColor();
        gameObject.tag = color.ToString();
        Debug.Log(color.ToString());
    }

    private void CheckColor()
    {
        Color platColor;
        
        switch (color)
        {
            case PlatformColor.White:
                platColor = Color.white;
                break;
            case PlatformColor.Red:
                platColor = Color.red;
                break;
            case PlatformColor.Blue:
                platColor = Color.blue;
                break;
            case PlatformColor.Green:
                platColor = Color.green;
                break;
            case PlatformColor.Checkpoint:
                platColor = Color.yellow;
                break;
            default:
                Debug.Log("Error with platform color");
                platColor = Color.white;
                break;
        }
        GetComponent<MeshRenderer>().material.color = platColor;
    }
}

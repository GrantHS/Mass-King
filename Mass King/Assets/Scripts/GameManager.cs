using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    //Player Parameters
    private Color playerColor;    

    private float maxEnergy = 50;
    private float minEnergy = 0;
    private float energy = 100;
    private float energyDrain = 5f;

    public GameObject player;

    public Color normalColor = Color.white;

    //UI Parameters
    public GameObject slider;

    public float Energy { get => energy; private set => energy = value; }

    private void Update()
    {
        playerColor = player.GetComponent<MeshRenderer>().material.color;
        if (playerColor != normalColor)
        {
            Energy -= energyDrain * Time.deltaTime;
            if (Energy < minEnergy)
            {
                Energy = minEnergy;
                playerColor = normalColor;
                player.GetComponent<MeshRenderer>().material.color = normalColor;
            }
        }
        else
        {
            Energy += energyDrain * Time.deltaTime;
            if (Energy > maxEnergy) Energy = maxEnergy;
        }

        //Debug.Log("Energy Level: " + energy);
        slider.GetComponent<Slider>().value = energy;
    }
}

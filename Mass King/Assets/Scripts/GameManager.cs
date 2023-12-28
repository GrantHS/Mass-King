using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    //Player Parameters
    private Color playerColor;

    private Vector3 respawnPos;

    private float respawnTime = 2;
    private float maxEnergy = 50;
    private float minEnergy = 0;
    private float energy = 100;
    private float energyDrain = 5f;

    public GameObject player;

    public Color normalColor = Color.white;

    public bool isDrained;


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
                player.GetComponent<PlayerMovement>().playerColor = PlatformColor.White;
                isDrained = true;
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

    private void Start()
    {
        respawnPos = player.transform.position;
    }

    private IEnumerator RespawnPlayer()
    {
        Debug.Log("Respawning Player");
        float blinkTime = (respawnTime / 3);

        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<MeshRenderer>().material.color = normalColor;

        player.GetComponent<MeshRenderer>().enabled = false;
        player.transform.position = respawnPos;
        yield return new WaitForSeconds(blinkTime);
        player.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(blinkTime);
        player.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(blinkTime);
        player.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(blinkTime);
        player.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(blinkTime);
        player.GetComponent<MeshRenderer>().enabled = true;

        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    public void CheckPlatform(PlatformColor platColor, PlatformColor playerColor)
    {
        if (platColor != PlatformColor.White && platColor != playerColor)
        {
            Debug.Log("Wrong Color");
            StartCoroutine(RespawnPlayer());
        }
    }
}

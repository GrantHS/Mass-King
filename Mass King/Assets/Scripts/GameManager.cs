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
    private float energy = 50;
    private float energyDrain = 5f;

    private float maxHumility = 50;
    private float minHumility = 0;
    private float humility = 0;
    private float humilityGain = 40f;

    public GameObject player;

    public Color normalColor = Color.white;

    public bool isDrained;

    //Collectable Parameters
    private float egoBoost = 20;

    //UI Parameters
    public GameObject energySlider;
    public GameObject humilitySlider;

    //Properties
    public float Energy { get => energy; private set => energy = value; }
    public float Humility { get => humility; private set => humility = value; }

    private void Update()
    {
        playerColor = player.GetComponent<MeshRenderer>().material.color;
        if (playerColor != normalColor)
        {
            DrainEnergy();
        }
        else
        {
            AddEnergy();
        }

        //Debug.Log("Energy Level: " + energy);
        energySlider.GetComponent<Slider>().value = Energy;
        humilitySlider.GetComponent<Slider>().value = Humility;
    }

    private void Start()
    {
        respawnPos = player.transform.position;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        Debug.Log("Respawning Player");
        float blinkTime = (respawnTime / 3);

        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<MeshRenderer>().material.color = normalColor;

        Energy = maxEnergy;
        Humility = minHumility;

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

            if(platColor == PlatformColor.Checkpoint)
            {
                Debug.Log("Checkpoint Reached!");
                respawnPos = player.transform.position;
            }
            else
            {
                Debug.Log("Wrong Color");
                AddHumility();
            }
            
        }
    }

    private void AddEnergy()
    {
        Energy += energyDrain * Time.deltaTime;
        if (Energy > maxEnergy) Energy = maxEnergy;
    }

    private void DrainEnergy()
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

    private void AddHumility()
    {
        Humility += humilityGain * Time.deltaTime;
        if( Humility >= maxHumility)
        {
            RespawnPlayer();
        }
    }

    private void DrainHumility()
    {
        Humility -= egoBoost;
        if(Humility <= minHumility) Humility = minHumility;
    }

    public void EgoBoost()
    {
        DrainHumility();
    }
}

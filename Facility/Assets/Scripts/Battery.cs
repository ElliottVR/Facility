using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public bool grabbed;
    public GameObject Player;
    private bool drainBattery;
    public GameObject generator;
    public float batteryLife;
    public int multiplier;
    public bool startTimer;
    private bool dead;
    public bool charging;
    public GameObject generatorBattery;
    private bool thisBattery;

    void Start()
    {
        
    }

    
    void Update()
    {
        grabbed = Player.GetComponent<PickupObject>().inHands; // Is the player holding object.
        generatorBattery = generator.GetComponent<Generator>().battery;
        drainBattery = generator.GetComponent<Generator>().drainBattery;

        if (generatorBattery == gameObject)
        {
            thisBattery = true;
        }
        if (generatorBattery != gameObject)
        {
            thisBattery = false;
        }
        // Turns on/off countdown when it enters/exits generator. 
        if (drainBattery == true && thisBattery == true)
        {
            startTimer = true;
        }
        if (drainBattery == false && thisBattery == true)
        {
            startTimer = false;
        }

        // Countdown for the battery.
        if (startTimer == true && dead == false)
        {
            batteryLife -= Time.deltaTime * multiplier;
            charging = true;
        }
        if (startTimer == false)
        {
            charging = false;
        }
        if (batteryLife < 0)
        {
            batteryLife = 0;
            dead = true;
            //Destroy(gameObject);
        }
    }

       
}

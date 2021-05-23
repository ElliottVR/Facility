using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject snapPoint;
    public GameObject battery;
    private bool canSnap;
    public bool grabbed;
    public GameObject Player;
    private bool snapped;
    public bool drainBattery;
    public bool facilityOn;

    void Start()
    {
        canSnap = false;
    }

    void Update()
    {
        grabbed = Player.GetComponent<PickupObject>().inHands; // Is player holding object.
        

        // Snaps battery to point in generator.
        if (canSnap == true && grabbed == false)
        {
            snapped = true;
        }
        if (grabbed == true && canSnap)
        {
            snapped = false;
        }
        if (snapped == true)
        {
            facilityOn = battery.GetComponent<Battery>().charging; // Does the facility have power.
            battery.transform.position = snapPoint.transform.position;
            battery.transform.rotation = Quaternion.Euler(0, 0, 0);
            drainBattery = true;
        }
        if (snapped == false)
        {
            drainBattery = false;
        }

        
        if (facilityOn == true)
        {

        }
    }

    // Checks for battery entering/exiting generator's trigger.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Battery")
        {
            battery = other.gameObject;
            canSnap = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Battery")
        {
            battery = null;
            canSnap = false;
        }
    }
}

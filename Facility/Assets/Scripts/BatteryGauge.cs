using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryGauge : MonoBehaviour
{
    //float t;
    public GameObject myBattery;
    public GameObject orgin;
    public float batteryLife;
    //public float turnDeg;
    public float startPos;
    public Quaternion endPos;
    private float goal;
    //private float needleRot;
    private bool startTimer;
    private Vector3 eulers;
    public float degreesPerSecond;
    public float totalRotationTime;
    private bool canRotate;
    

    void Start()
    {
        canRotate = true;
        goal = endPos.eulerAngles.z;
        
    }


    void Update()
    {       
        //Debug.Log(canRotate);
        batteryLife = myBattery.GetComponent<Battery>().batteryLife;
        startTimer = myBattery.GetComponent<Battery>().startTimer;
        //totalRotationTime = batteryLife;
        
        if (startTimer == true && canRotate == true)
        {
            degreesPerSecond = goal / totalRotationTime - 0.65f;
            
            orgin.transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime);
            
        }
        if (batteryLife == 0)
        {
            canRotate = false;
            //transform.rotation = Quaternion.Euler(0, 0, 80);
        }
        if (transform.eulerAngles.z == 80)
        {
            canRotate = false;
            degreesPerSecond = 0;
            transform.rotation = Quaternion.Euler(0, 0, 80);
        }
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryGauge : MonoBehaviour
{
    float t;
    public GameObject myBattery;
    public GameObject orgin;
    public float batteryLife;
    public float turnDeg;
    public float startPos;
    public Quaternion endPos;
    private float goal;
    private float needleRot;
    private bool startTimer;
    private Vector3 eulers;
    public float degreesPerSecond;
    public float totalRotationTime;
    private bool canRotate;

    void Start()
    {
        //eulers = transform.rotation.eulerAngles;
        //orgin.transform.rotation = startPos;
        canRotate = true;
        goal = endPos.eulerAngles.z;
    }


    void Update()
    {
        batteryLife = myBattery.GetComponent<Battery>().batteryLife;
        startTimer = myBattery.GetComponent<Battery>().startTimer;
        //totalRotationTime = batteryLife;
        degreesPerSecond = goal / totalRotationTime;
        if (orgin.transform.eulerAngles.z >= 80 && startTimer == true)
        {
            orgin.transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime);
        }
        if (batteryLife == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 80);
        }
        /*
        if (orgin.transform.eulerAngles.z == 180)
        {
            canRotate = false;
        }
        */
        /*if (startTimer == true)
        {
            StartCoroutine(NeedleRotate(eulers));
        }
        if (startTimer == false)
        {
            StopCoroutine(NeedleRotate(eulers));
        }
        orgin.transform.rotation = Quaternion.Euler(eulers.x, eulers.y, needleRot);
        */
    }

    /*IEnumerator NeedleRotate(Vector3 eulers)
    {
        while (startTimer == true)
        {
            
            if (startTimer == false)
            {
                break;
            }
            yield return null;
        }
        
    }
    */
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{

    

    public GameObject mainCamera;  // The camera the player sees through.
    public GameObject Player;
    static public GameObject CarriedObject;
    private GameObject ObjectCarried;
    //public int numberOfObjects;
    //public GameObject[] PickupableObjects;
    public GameObject ItemHoldPosition;
    public GameObject VrHoldPosition;


    //public GameObject SingleCrosshair;
    public GameObject DoubleCrosshairInstall;

    public float throwForce = 0.2f;
    private Rigidbody rb;
    public LayerMask SnapZone;

    // Customise the rotation of the object that you pick up.
    public int RotX;
    public int RotY;
    public int RotZ;

    static public bool Carrying;
    public bool inHands;
    public bool remotecarry;

    public float distance;  // Distance from the object you must be closer than to pick up.
    public float PositionForward;  // How far the object is carried in front of you.
    public float PositionUp;  // How far the object is carried above or below the camera.
    private float smooth;

    private GameObject InstallPoint;

    //  Information Panels describing computer components

    //public AudioSource VrSoundEffect;

    void Start()  // Set the default settings of carrying an object to prevent bugs

    {
        Carrying = false;
        CarriedObject = null;
        GameVariables.CrossHairActive = false;
        remotecarry = false;
        remotecarry = false;
    }


    void Update()
    {
        ObjectCarried = CarriedObject;

        if (Carrying == true)  // If you are carrying an object.
        {
            Carry();
            checkDrop();
        }

        else
        {
            Pickup();  // If you are not carrying anything.
        }

        if (remotecarry == true)  // Drop the object you are carrying.
        {
            remotecarry = false;
            dropObject();
        }

        // Raycast for Single Crosshair
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (/*hit.collider.GetComponent<Pickupable>() && Carrying == false
                && Vector3.Distance(transform.position, hit.transform.position) < distance*/
                hit.collider.GetComponent<Battery>() && Carrying == false
                && Vector3.Distance(transform.position, hit.transform.position) < distance)
            {
                GameVariables.CrossHairActive = true;
            }

            else
            {
                GameVariables.CrossHairActive = false;
            }
        }
    }



    void Carry()  // Place the object in front of your camera while you are carrying it.

    {
        CarriedObject.transform.parent = Player.transform;
        string ObjectName = CarriedObject.name;
        inHands = true;

        if (inHands == true)
        {
            /*if (ObjectName == "Non-VR - VR Headset")
            {
                CarriedObject.transform.position = VrHoldPosition.transform.position;

                //if (GameVariables.VRReady == true)
                //{
                //    infoVRHeadset.SetActive(true);
                //}
            */
                //else
                //{
                //    infoVRHeadset.SetActive(true);
                //    GameObject.Find("Non-VR Motherboard").GetComponent<GhostGrabNonVr>().enabled = false;
                //}
            }

            else
            {
                CarriedObject.transform.position = ItemHoldPosition.transform.position;
            }
        


        //if (ObjectName == "Non-VR Motherboard")
        //{
        //    infoMotherboard.SetActive(true);
        //    Debug.Log(ObjectName + " is Carried");
        //    GameObject.Find("Non-VR Motherboard").GetComponent<GhostGrabNonVr>().enabled = true;
        //}



        CarriedObject.transform.position = Vector3.Lerp(CarriedObject.transform.position, mainCamera.transform.position +
            mainCamera.transform.forward * PositionForward + mainCamera.transform.up * PositionUp, Time.deltaTime * smooth);

        CarriedObject.transform.rotation = mainCamera.transform.rotation;
        CarriedObject.transform.Rotate(RotX, RotY, RotZ);


        // Raycast for placing object in specific location = install component into the logic gate
        if (inHands == true && gameObject.tag == "Battery"
            /*|| inHands == true && ObjectName == "Binary 1 Sphere 2" || inHands == true && ObjectName == "Binary 0 Sphere 2"*/)
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                /*
                if (hit.collider.GetComponent<InstallComponent>() && Carrying == true
                    && Vector3.Distance(transform.position, hit.transform.position) < distance)

                {
                    InstallPoint = hit.collider.gameObject;
                    DoubleCrosshairInstall.SetActive(true);
                    //Install();

                    //GameVariables.CrossHairActive = true;
                    Debug.Log("Selected Installation Point: " + hit.collider.gameObject.name);
                */
             }
                
                else
                {
                    DoubleCrosshairInstall.SetActive(false);
                    //GameVariables.CrossHairActive = false;
                }
                
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 2, Color.blue);
            }

        }

    



    void Pickup()  // Raycast script to detect and pick up the game object of choice.
    {
        if (Input.GetMouseButtonDown(0))  // The method used to pick up an object (Right-Click).
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Battery p = hit.collider.GetComponent<Battery>();
                //Interactable i = hit.collider.GetComponent<Interactable>();
                //SingleCrosshair.SetActive(true);

                if (p != null && Vector3.Distance(transform.position, p.transform.position) <= distance)
                {
                    Carrying = true;
                    //SingleCrosshair.SetActive(false);
                    CarriedObject = p.gameObject;
                    //p.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    //p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    //p.gameObject.GetComponent<Collider>().isTrigger = true;
                    //CarriedObject.layer = LayerMask.NameToLayer("DepthLayer");
                    p.gameObject.transform.parent = null;
                    //p.gameObject.transform.parent = Player.transform;
                }
                /*
                if (i != null && Vector3.Distance(transform.position, i.transform.position) <= distance)
                {
                    Carrying = true;
                    //SingleCrosshair.SetActive(false);
                    CarriedObject = i.gameObject;
                    //i.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    //p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    //i.gameObject.GetComponent<Collider>().isTrigger = true;
                    //CarriedObject.layer = LayerMask.NameToLayer("DepthLayer");
                    i.gameObject.transform.parent = null;
                    //p.gameObject.transform.parent = Player.transform;
                }
                */
            }
        }
    }



    void checkDrop()
    {
        //check key and check if carrying?
        if (Input.GetMouseButtonDown(0))  // Right click again to drop the object.
        {

            dropObject();
            inHands = false;

            /*
                        rb = ObjectCarried.GetComponent<Rigidbody>();
                        {
                            rb.AddForce(transform.forward * throwForce);
                        }
            */


        }
    }


    /*void Install()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CarriedObject.transform.position = InstallPoint.transform.position;
            DoubleCrosshairInstall.SetActive(false);
            Debug.Log("Installed: " + CarriedObject.name);
        }
    }
*/

    public void dropObject()  // Restore the object picked up to its original characteristics.
    {
        Carrying = false;
        CarriedObject.transform.parent = null;
        //CarriedObject.GetComponent<Rigidbody>().isKinematic = false;
        //CarriedObject.GetComponent<Rigidbody>().useGravity = true;
        CarriedObject.GetComponent<Collider>().isTrigger = false;
        //CarriedObject.transform.rotation = mainCamera.transform.rotation;
        CarriedObject.transform.Rotate(0, 0, 0);
        //CarriedObject.layer = LayerMask.NameToLayer("Default");
        CarriedObject = null;

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject target;
    private bool rayHitting;
    public float maxSight;
    private Collider rayCollide;
    private RaycastHit hit;
    public float maxPeripheral;
    public NavMeshAgent enemyNav;
    private bool inSight;
    private Transform offset;


    void Start()
    {
        //enemyNav = GetComponent<NavMeshAgent>();
        rayCollide = GetComponent<Collider>();
        target = GameObject.FindWithTag("Player");
        
    }

    
    void Update()
    {
        // Checks if enemy has something in sight.
        rayHitting = Physics.BoxCast(rayCollide.bounds.center, transform.localScale, transform.forward, out hit, transform.rotation, maxSight);
        


        if (inSight == true)
        {
            enemyNav.destination = target.transform.position;
        }
    }

    // Draws raycast to make a field of vision for the enemy.
    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;
        // If something is in the sight of the enemy the raycast collides with it.
        if (rayHitting)
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, new Vector3(1, 2, maxPeripheral));
            if (hit.collider.gameObject.tag == "Player")
            {
                inSight = true;
            }
            
            
        }
        // If nothing is in the sight of the enemy the raycast goes to the maximum distance.
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * maxSight);
            Gizmos.DrawWireCube(transform.position + transform.forward * maxSight, new Vector3(1, 2, maxPeripheral));
        }

    }
}

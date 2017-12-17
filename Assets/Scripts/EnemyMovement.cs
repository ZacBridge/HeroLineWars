
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private Enemy enemy;

    //private Transform target;
    private int wavepointIndex = 0;

    public Transform target; // used for storing current target 
    public bool upgradeTarget = false;

    public Transform currentWaypoint;
    public Transform previousWaypoint;
    // public Transform secondPreviousWaypoint;

    public List<GameObject> waypoints;
    public GameObject toRemoveWaypoint;


    public float range = 50f; //range of how far turrent can attack.


    void Start()
    {
        foreach (GameObject Waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            waypoints.Add(Waypoint); //Stores all objects with the tag (Waypoints) in here
        }
       
        enemy = GetComponent<Enemy>();
        UpdateTarget();
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position; // figures out that location to go to.
            transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World); // makes sure it always has a fixed speed.
        }


        if (Vector3.Distance(transform.position, target.position) <= 5f) // If enemy is within a set proximity of next waypoint, set next waypoint
        {
            // secondPreviousWaypoint = previousWaypoint;
            waypoints.Remove(toRemoveWaypoint); // takes the saved waypoint from before to remove it on next run through
            previousWaypoint = target;
            UpdateTarget(); // Calls the next waypoint
        }

        enemy.speed = enemy.startSpeed; // resets the speed, thus allowing the object to go back to it's normal speed again if not slowed again.

    }

    void UpdateTarget() // searches for new target/current target
    {
       

        float shortestDistance = Mathf.Infinity; // math.infinity used so the distance is infnite while no enemy is found
         GameObject nearestWaypoint = null;


        foreach (GameObject Waypoints in waypoints)
        {

            float distanceToWaypoint = Vector3.Distance(transform.position, Waypoints.transform.position); // get distance from current turret to enemy

            //if (upgradeTarget)
            //{
            //    if (distanceToWaypoint > 5 && distanceToWaypoint < 2000 &&  ) //currentWaypoint != previousWaypoint)
            //    {
            //        //if (secondPreviousWaypoint != previousWaypoint)
            //       // {
            //            shortestDistance = distanceToWaypoint;
            //            nearestWaypoint = Waypoints;
            //       // }

            //    }
            //}else

            if (distanceToWaypoint < shortestDistance) // if distance is shorter then current shortest distance, change the shortest distance to the new one and set this enemy as closest new enemy
            {
                shortestDistance = distanceToWaypoint;
                nearestWaypoint = Waypoints;
            }

            SetNextLocation(nearestWaypoint, shortestDistance); // sends over the variables, sets the next batch.
            currentWaypoint = target;

        }
    }

    private Transform SetNextLocation(GameObject nearestWaypoint, float shortestDistance)
    {
        if (nearestWaypoint != null && shortestDistance <= range) // we have found a enemy and it is within our range.
        {
            target = nearestWaypoint.transform; // sets the next location to go to 
            toRemoveWaypoint = nearestWaypoint;
        }
        else
        {
            target = null; // if target is no longer in distance change it.
        }

        return target;
    }

    void LockOnTarget()
    {
        //target lock-on
        Vector3 dir = target.position - transform.position; // figures out distance between target and ourselves
        //Quaternion lookRotation = Quaternion.LookRotation(dir); // set its look destination to be the direction to target
        //Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; // converted to eulerangles & we want to smoothly rotate to new target over time.
        //partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); // rotate only around the Y axis
    }

    

   }

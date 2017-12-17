using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour

{
    // script used for controlling the spawn locations of flying and ground
    // allowing it to be randomized
    [Header("Team One SpawnPoint Information")]
    public static Transform[] spawnLocationsTeamOne; // contains all the current spawnPoints on the map
    private int spawnPointNumberTeamOne = 0;
    private int spawnPointCounterTeamOne = 0;

    [Header("Team Two SpawnPoint Information")]
    public static Transform[] spawnLocationsTeamTwo; // contains all the current spawnPoints on the map
    private int spawnPointNumberTeamTwo = 0;
    private int spawnPointCounterTeamTwo = 0;


    void Awake() //Finds all the spawns points the spawnpoint holder, once found counts how many of each.
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.tag == "SpawnPointTeamOnePortalOne" || child.tag == "SpawnPointTeamOnePortalTwo")
            {
                spawnPointCounterTeamOne++;
            }
            if (child.tag == "SpawnPointTeamTwoPortalOne" || child.tag == "SpawnPointTeamTwoPortalTwo")
            {
                spawnPointCounterTeamTwo++;
            }
        }

        //creates the correct amount of array holders due to the previous method
        spawnLocationsTeamOne = new Transform[spawnPointCounterTeamOne];
        spawnLocationsTeamTwo = new Transform[spawnPointCounterTeamTwo];
        

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.tag == "SpawnPointTeamOnePortalOne" || child.tag == "SpawnPointTeamOnePortalTwo")
            {
                spawnLocationsTeamOne[spawnPointNumberTeamOne] = transform.GetChild(i);
                spawnPointNumberTeamOne++;
            }
            if (child.tag == "SpawnPointTeamTwo")
            {
                spawnLocationsTeamTwo[spawnPointNumberTeamTwo] = transform.GetChild(i);
                spawnPointNumberTeamTwo++;
            }
            
        }
    }


}

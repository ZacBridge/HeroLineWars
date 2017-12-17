using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is used for checking if a player is able to build in a location and if they have the money required for the correct object to buold there.
public class BuildManager : MonoBehaviour {

    public static BuildManager instance; // used for storing a refernce to itself

    public playerStats player;

    private EnemyBlueprint enemyToSpawn; // variable that stores what turret will be set.

    public bool canSpawn { get { return enemyToSpawn != null; } } // variable can never be set, if turret to build returns not equal to null it'll return true, else it'll return false
    // public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } } // if we have enough money, return true.

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one buildmanger in scene!");
            return;
        }

        instance = this; // this build manager right here is going to be put into the refernce to instance, allowing a refernce to it to happen
    }


    public void SpawnSelectedEnemy(EnemyBlueprint enemy) //used for selecting turrets to build
    {
        enemyToSpawn = enemy;
        
        if (player.Money < enemy.cost)
        {
            Debug.Log("You don't have enough money friend");
            return;
        }

        player.MoneyGenerationIncrease(enemy.incomeUpgrade); // increases the amount of money per 5 seconds the player gains by the amount the minion is worth
        player.Money -= enemy.cost; // deducts money to the price of the enemy


        int i = Random.Range(0,2); // the second number is exclusive, meaning it wont ever be called so it will call 0 or 1 in this case
        Debug.Log("I have spawned at SpawnPoint: " + i);
        
        // TODO - Make it so it can differenciate between which players spawned them and send in the right spawners
        // Currently just spawns in both portals of team 1

        GameObject enemySpwaned = (GameObject)Instantiate(enemyToSpawn.prefab, SpawnPoints.spawnLocationsTeamOne[i].position, Quaternion.identity);
  


        
    }

    // simple method to which calles the not enough money fader in the nodeUI allowing it show a visual representive of not having enough money
    public void notEnoughMoney()
    {

    }

    public EnemyBlueprint getEnemyToSpawn()
    {
        return enemyToSpawn;
    }


}

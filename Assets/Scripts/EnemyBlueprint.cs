using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// seriazble script that is used to store a object along with the price of it.
[System.Serializable]
public class EnemyBlueprint
{
    //Maybe attach a range indicator to a turret blueprint as a seperate object.

    public GameObject prefab;
    public int cost;
    public int incomeUpgrade;

}
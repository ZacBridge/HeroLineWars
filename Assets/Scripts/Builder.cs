using System.Collections.Generic;
using UnityEngine;

// used for creating shop bullings in the GUI to which call the buildmanager to create the objects.
// used for sending over the premade prefab turret that has been selected to the build manager.
public class Builder : MonoBehaviour {

    public EnemyBlueprint Enemy1;
    public EnemyBlueprint Enemy2;

    BuildManager buildManger; // sets buildmanager

    void Start()
    {
       buildManger = BuildManager.instance; // sets the buildmanager to the refernce of itself in buildmanager.
    }

    public void SpawnEnemy1()
    {
        Debug.Log("Enemy 1 Spawned");
        buildManger.SpawnSelectedEnemy(Enemy1);
        
    }

    public void SpawnEnemy2()
    {
        Debug.Log("Enemy 2 Spawned");
        buildManger.SpawnSelectedEnemy(Enemy2);
    }

}

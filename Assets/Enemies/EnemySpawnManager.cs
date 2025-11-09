using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    [SerializeField] public GameObject elementEnemy;
    List<GameObject> activeEnemies;
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     /*
     when player causes damage to an activeEnemy
     invoke takeDamage() on that enemy
     if takeDamage == false, remove from activeEnemies
     */

    GameObject spawnEnemy(enemyTypes variant, Vector2 position)
    {
        GameObject spawnedEnemy = Instantiate(elementEnemy, position, Quaternion.identity);
        EnemyScript spawnedEnemyScript = spawnedEnemy.GetComponent<EnemyScript>();
        switch (variant)
        {
            case enemyTypes.FIRE:
                spawnedEnemyScript.spawnAsFire();
                break;
            case enemyTypes.WATER:
                spawnedEnemyScript.spawnAsWater();
                break;
            case enemyTypes.LIGHTNING:
                spawnedEnemyScript.spawnAsLightning();
                break;
            case enemyTypes.WIND:
                spawnedEnemyScript.spawnAsFire();
                break;
        } 

        activeEnemies.Add(spawnedEnemy);
        return spawnedEnemy;
    }
}

using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 6f;

    Vector2 direction = Vector2.zero;
  


public void Init(Vector2 dir)
    {
        direction = dir.normalized;
        
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}

//TODO: add this script to fireballprefab as a component
//TODO: add fireball prefab into EnemySpawnManager as a component
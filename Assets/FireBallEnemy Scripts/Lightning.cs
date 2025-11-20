using UnityEngine;
using UnityEngine.UIElements;

public class Lightning : MonoBehaviour
{
    Vector3 positionToStrike;
    const float maxHeight = 8f;
    const float strikeSpeed = 35;

    public void Init(Vector3 location)
    {

        transform.position = new Vector3(positionToStrike[0], maxHeight, positionToStrike[2]);
        positionToStrike = new Vector3(location[0], location[1] + 1.7f, location[2]);

    }

    void Update()
    {
        float delta = strikeSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, positionToStrike, delta);
    }
}

//TODO: add this script to fireballprefab as a component
//TODO: add fireball prefab into EnemySpawnManager as a component
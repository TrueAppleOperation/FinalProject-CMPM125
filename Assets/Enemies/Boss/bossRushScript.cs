using UnityEngine;
using UnityEngine.UIElements;

public class bossRushScript : MonoBehaviour
{
    Vector3 positionToStrike;
    const float maxHeight = 8f;
    const float strikeSpeed = 35;

    public void Init(Vector3 location)
    {

        transform.position = location;

    }

}


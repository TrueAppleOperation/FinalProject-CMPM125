using UnityEngine;
using UnityEngine.UIElements;

public class shockScript : MonoBehaviour
{
    Vector3 positionToStrike;
    const float maxHeight = 8f;
    const float strikeSpeed = 35;

    public void Init(Vector3 location)
    {

        transform.position = location;

    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 30f) * Time.deltaTime);
    }
}


using Unity.VisualScripting;
using UnityEngine;

public class stompScript : MonoBehaviour
{
    public void Init(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;
        Destroy(gameObject, 1f);

    }
}

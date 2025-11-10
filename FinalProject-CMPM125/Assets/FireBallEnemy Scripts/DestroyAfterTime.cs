using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}

// this deletes objects after 3secs to avoid having too many on screen
//TODO: add into fireball prefab as a component
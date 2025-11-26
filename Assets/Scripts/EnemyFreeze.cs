using UnityEngine;
using System.Collections;


public class EnemyFreeze : MonoBehaviour
{
    bool isFrozen = false; 
    public float speed = 10f;

    void Update()
    {
        if (!isFrozen)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

    }

    public void Freeze(float duration)
    {
        if (!isFrozen)
        {
            StartCoroutine(FreezeCoroutine(duration));
        }
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;
        yield return new WaitForSeconds(duration);
        isFrozen = false;
    }
}


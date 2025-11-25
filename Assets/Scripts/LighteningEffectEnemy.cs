using UnityEngine;

public class LighteningEffectEnemy : MonoBehaviour
{
    Public float speed = 5f;
    private bool isStunned = false;

    void Update()
    {
        if (!isStunned)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

    }

    public void Stun(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(duration));
        }
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }
}

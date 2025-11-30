using UnityEngine;

public class Lightning : MonoBehaviour
{
    Vector3 positionToStrike;
    const float maxHeight = 8f;
    const float strikeSpeed = 35;
    private bool isLanded = false;

    public void Init(Vector3 location)
    {
        positionToStrike = new Vector3(location.x, location.y + 1.7f, location.z);
        transform.position = new Vector3(positionToStrike.x, maxHeight, positionToStrike.z);
    }

    void Update()
    {
        if (!isLanded)
        {
            float delta = strikeSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, positionToStrike, delta);
            if (transform.position == positionToStrike)
            {
                isLanded = true;
                ApplyLightningDamage();
            }
        }
    }

    private void ApplyLightningDamage()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("Player struck by lightning!");
                PlayerController playerScript = collider.GetComponent<PlayerController>();

                if (playerScript != null)
                {
                    playerScript.takeDamage(10f);
                    Debug.Log("Lightning damage applied!");
                }
            }
        }
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isLanded)
        {
            Debug.Log("Player entered lightning trigger!");
            PlayerController playerScript = other.GetComponent<PlayerController>();

            if (playerScript != null)
            {
                playerScript.takeDamage(10f);
            }
        }
    }
}
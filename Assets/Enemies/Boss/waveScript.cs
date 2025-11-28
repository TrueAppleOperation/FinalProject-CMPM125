using UnityEngine;
using UnityEngine.UIElements;

public class waveScript : MonoBehaviour
{

    GameObject bossSelf;
    GameObject PLAYER;
    Vector2 playerPosition;
    const float SPEED = 0.75f;

    void Start()
    {
        bossSelf = GameObject.FindWithTag("Boss");
        PLAYER = GameObject.FindWithTag("Player");

        playerPosition = PLAYER.transform.position;
        transform.position = bossSelf.transform.position;

    }

    void Update()
    {
        Vector2 toPlayer = (Vector3)playerPosition - bossSelf.transform.position;
        float distance = toPlayer.magnitude;

        Vector2 dir = toPlayer.normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, SPEED * Time.fixedDeltaTime);    
        bossSelf.transform.position = Vector2.MoveTowards(bossSelf.transform.position, playerPosition, SPEED * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("playe took damage from wave!");
            PlayerController playerScript = PLAYER.GetComponent<PlayerController>();
            BossAI bossAIScript = bossSelf.GetComponent<BossAI>();

            playerScript.takeDamage(5f);
            bossAIScript.resetPlayerDMGTimer();
        }
    }

}


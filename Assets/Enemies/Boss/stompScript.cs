using Unity.VisualScripting;
using UnityEngine;

public class stompScript : MonoBehaviour
{
    GameObject bossSelf;
    GameObject PLAYER;
    public void Init(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;
        Destroy(gameObject, 1f);
        bossSelf = GameObject.FindWithTag("Boss");
        PLAYER = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("playe took damage from fire!");
            PlayerController playerScript = PLAYER.GetComponent<PlayerController>();
            BossAI bossAIScript = bossSelf.GetComponent<BossAI>();

            playerScript.takeDamage(6f);
            bossAIScript.resetPlayerDMGTimer();
        }
    }
}

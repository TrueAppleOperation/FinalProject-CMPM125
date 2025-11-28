using UnityEngine;
using UnityEngine.UIElements;

public class windScript : MonoBehaviour
{
    Vector3 positionToStrike;
    const float maxHeight = 8f;
    const float strikeSpeed = 35;

    GameObject bossSelf;
    GameObject PLAYER;

    public void Init(Vector3 location)
    {

        transform.position = location;
        bossSelf = GameObject.FindWithTag("Boss");
        PLAYER = GameObject.FindWithTag("Player");

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("playe took damage from wind!");
            PlayerController playerScript = PLAYER.GetComponent<PlayerController>();
            BossAI bossAIScript = bossSelf.GetComponent<BossAI>();

            playerScript.takeDamage(4f);
            bossAIScript.resetPlayerDMGTimer();
        }
    }

}


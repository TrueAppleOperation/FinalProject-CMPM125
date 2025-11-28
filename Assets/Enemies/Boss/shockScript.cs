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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject bossSelf = GameObject.FindWithTag("Boss");
            GameObject PLAYER = GameObject.FindWithTag("Player");
            Debug.Log("playe took damage from shock!");
            PlayerController playerScript = PLAYER.GetComponent<PlayerController>();
            BossAI bossAIScript = bossSelf.GetComponent<BossAI>();

            playerScript.takeDamage(8f);
            bossAIScript.resetPlayerDMGTimer();
        }
    }
}


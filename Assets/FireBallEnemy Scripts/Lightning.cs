using UnityEngine;
using UnityEngine.UIElements;

public class Lightning : MonoBehaviour
{
    Vector3 positionToStrike;
    const float maxHeight = 8f;
    const float strikeSpeed = 35;
    private bool isLanded = false;



    public void Init(Vector3 location)
    {

        transform.position = new Vector3(positionToStrike[0], maxHeight, positionToStrike[2]);
        positionToStrike = new Vector3(location[0], location[1] + 1.7f, location[2]);

    }

    void Update()
    {
        float delta = strikeSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, positionToStrike, delta);
        if (transform.position == positionToStrike)
        {
            isLanded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isLanded)
        {
            Debug.Log("player got struck!");
            PlayerController playerScript = other.GetComponent<PlayerController>();
            playerScript.takeDamage(4f * Time.deltaTime);
        }
    }
}


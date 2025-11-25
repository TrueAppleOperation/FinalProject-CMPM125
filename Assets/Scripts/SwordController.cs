using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Sword sword;
    void Awake()
    {
        sword = GetComponent<Sword>();
    }
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        if(Input.GetMouseButtonDown(0))
        {
            if(sword.currentType =! SwordType.Sun)
            sword.SetSwordType(direction);
            Debug.Log("Clicked");
        }
        if(Input.GetMouseButton(0))
        {
            if(sword.currentType == SwordType.Sun)
            sword.SetSwordType(direction);
            Debug.Log("Holding Click");
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class SwordController : MonoBehaviour
{
    private Sword sword;
    void Awake()
    {
        sword = GetComponent<Sword>();
    }
    
    void Update()
    {       
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            sword.SetSwordType(direction);
            Debug.Log("Clicked");
        }
    }

}

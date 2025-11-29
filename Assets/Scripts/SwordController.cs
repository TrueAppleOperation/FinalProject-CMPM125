using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Sword sword;
     void Awake()
    {
        sword = GetComponent<Sword>();
        if (sword == null)
         Debug.LogError("SwordController: no Sword component found on this GameObject!");
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        if(Input.GetMouseButtonDown(0))
        {
            if(sword.currentType != SwordType.Sun)
            sword.SetSwordType(direction);
            Debug.Log("Clicked");
        }
        if(Input.GetMouseButton(0))
        {
            if(sword.currentType == SwordType.Sun)
            Debug.Log("Holding Click w Sun");
            sword.SetSwordType(direction);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("DEBUG: P pressed, forcing Sun spawn");
            sword.DebugSpawnSun(direction);
        }
    }
}

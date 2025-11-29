using UnityEngine;

public class PowerSwitcher : MonoBehaviour
{
    public Sword sword;

    void Awake()
    {
        sword = GetComponent<Sword>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sword.currentType = SwordType.Wind;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sword.currentType = SwordType.Sun;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            sword.currentType = SwordType.Snow;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            sword.currentType = SwordType.Rain;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            sword.currentType = SwordType.None;
        }

    }
}

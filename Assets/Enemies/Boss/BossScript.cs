using UnityEngine;

public class BossScript : MonoBehaviour
{
    const float maxHP = 400;
    float HP = maxHP;
    private Rigidbody2D rb;

    [SerializeReference] public Sprite bossTexture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool takeDamage(float DMG)
    {
        if (DMG >= HP)
        {
            Destroy(this);
            return false;
        }
        else
        {
            HP -= DMG;
            return true;
        }
    }




}

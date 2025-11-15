using UnityEngine;

public class BossScript : MonoBehaviour
{
    const float maxHP = 400;
    float HP = maxHP;
    const float SPEED = 1.5f;
    private Rigidbody2D rb;
    [SerializeReference] public Sprite bossTexture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        preventHPOverflow();
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

    void preventHPOverflow()
    {
        if (HP > maxHP)
        {
            Debug.LogError("HP Oveflow - dected an overflow of allowed max HP on BOSS, setting to half of allowed max");
            HP = maxHP / 2;

        }
    }

    /*
     me thinks behavior tree?
    
     fire: ground slam aoe if player is close to boss
     water: rides on water and charge attacks in playerDirection if player has not taken any recent damage
     thunder: calls lightning if player is too far from boss
     wind: 1/4 chance for wind cone attack at the end of a fire/water/thunder attack

     */
}

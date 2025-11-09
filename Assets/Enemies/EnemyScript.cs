using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
public class EnemyScript : MonoBehaviour
{
    
    //include behvaior
    float HP;
    float SPEED;
    float maxHP;
    [SerializeReference] public Sprite windSprite;
    [SerializeReference] public Sprite fireSprite;
    [SerializeReference] public Sprite lightningSprite;
    [SerializeReference] public Sprite waterSprite;

    //Sprite selfSprite = GetComponent<SpriteRenderer>();  <---- UNCOMMENT ONCE SPRITES ARE IMPLIMENTED
    enemyTypes TYPE;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        preventHPOverflow();
        
    }


    bool takeDamage(float DMG) 
    { 
        if (DMG >= HP)
        {
            Destroy(this);
            return false;
        } else
        {
            HP -= DMG;
            return true;
        }
    }

    void preventHPOverflow()
    {
        if (HP > maxHP)
        {
            Debug.LogError("HP Oveflow - dected an overflow of allowed max HP on " + TYPE + ", setting to half of allowed max");
            HP = maxHP / 2;

        }
    }

    //custom init functions since unity doesnt have built in functions for this
    public void spawnAsFire()
    {
        HP = 1f;
        SPEED = 1f;
        maxHP = HP;
        TYPE = enemyTypes.FIRE;
        //selfSprite = fireSprite;  <---- UNCOMMENT ONCE SPRITES ARE IMPLIMENTED
        //behavior = behavior yada yada
    }
    public void spawnAsWater()
    {
        HP = 1f;
        SPEED = 1f;
        maxHP = HP;
        TYPE = enemyTypes.WATER;
        //selfSprite = waterSprite;  <---- UNCOMMENT ONCE SPRITES ARE IMPLIMENTED
        //behavior = behavior yada yada
    }
    public void spawnAsLightning()
    {
        HP = 1f;
        SPEED = 1f;
        maxHP = HP;
        TYPE = enemyTypes.LIGHTNING;
        //selfSprite = lightningSprite;  <---- UNCOMMENT ONCE SPRITES ARE IMPLIMENTED
        //behavior = behavior yada yada
    }
    public void spawnAsWind()
    {
        HP = 1f;
        SPEED = 1f;
        maxHP = HP;
        TYPE = enemyTypes.WIND;
        //selfSprite = windSprite;  <---- UNCOMMENT ONCE SPRITES ARE IMPLIMENTED
        //behavior = behavior yada yada
    }
}

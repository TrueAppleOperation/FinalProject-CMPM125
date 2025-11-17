using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
public class EnemyScript : MonoBehaviour
{

    //include behvaior
    float HP;
    float SPEED;
    float maxHP;
    private Rigidbody2D rb;
    private float originalY;

    [SerializeReference] public Sprite windSprite;
    [SerializeReference] public Sprite fireSprite;
    [SerializeReference] public Sprite lightningSprite;
    [SerializeReference] public Sprite waterSprite;

    //Sprite selfSprite = GetComponent<SpriteRenderer>();  <---- UNCOMMENT ONCE SPRITES ARE IMPLIMENTED
    enemyTypes TYPE;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalY = transform.position.y;
    }

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
            Debug.LogError("HP Oveflow - dected an overflow of allowed max HP on " + TYPE + ", setting to half of allowed max");
            HP = maxHP / 2;

        }
    }

    public IEnumerator BringBackDown(Rigidbody2D rb, float delay)
    {
        Debug.Log("Bringing enemy back down");
        yield return new WaitForSeconds(delay);
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
            if (transform.position.y > (originalY))
            {
                Vector3 pos = transform.position;
                pos.y = originalY;
                transform.position = pos;
            }
            Debug.Log("Gravity reset to 0");
        }

    }
    public void KnockUp(float force, float duration)
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 1;
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            StartCoroutine(BringBackDown(rb, duration));
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
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    const float maxHP = 8000;
    float HP = maxHP;
    private Rigidbody2D rb;
    private float originalY;
    private bool isFrozen = false;
    private bool isStunned = false;
    private BossAI bossAI;
    public event Action<float> OnHealthChanged;
    public event Action<float> OnMaxHealthChanged;

    [SerializeReference] public Sprite bossTexture;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalY = transform.position.y;
        bossAI = GetComponent<BossAI>();

        // Initialize health events
        OnMaxHealthChanged?.Invoke(maxHP);
        OnHealthChanged?.Invoke(HP);
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
            bossAI.enableAI();
        }
    }

    public void KnockUp(float force, float duration)
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 1;
            // disable AI while in the air
            if (bossAI != null) bossAI.disableAI();
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            StartCoroutine(BringBackDown(rb, duration));
        }
    }

    public void Freeze(float duration)
    {
        if (!isFrozen)
        {
            StartCoroutine(FreezeCoroutine(duration));
        }
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;
        // stop immediate movement
        if (rb != null) rb.linearVelocity = Vector2.zero;
        if (bossAI != null) bossAI.disableAI();
        yield return new WaitForSeconds(duration);
        isFrozen = false;
        if (bossAI != null) bossAI.enableAI();
    }

    public void Stun(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(duration));
        }
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        if (rb != null) rb.linearVelocity = Vector2.zero;
        if (bossAI != null) bossAI.disableAI();
        yield return new WaitForSeconds(duration);
        isStunned = false;
        if (bossAI != null) bossAI.enableAI();
    }

    public bool takeDamage(float DMG)
    {
        if (DMG >= HP)
        {
            Destroy(gameObject);
            NextScene();
            return false;
        }
        else
        {
            HP -= DMG;
            OnHealthChanged?.Invoke(HP);
            return true;
        }
    }
    private void NextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    public float GetCurrentHP()
    {
        return HP;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

    public void ModifyHealth(float amount)
    {
        HP = Mathf.Clamp(HP + amount, 0, maxHP);
        OnHealthChanged?.Invoke(HP);
    }
}
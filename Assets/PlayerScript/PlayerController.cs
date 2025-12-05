using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private const float maxHP = 30;
    private float HP = maxHP;
    private const float hpRecoveryTimer = 10f;
    private Sword sword;

    private float timeSinceLastDMG = 0;

    public System.Action<float> OnHealthChanged;
    public System.Action<float> OnMaxHealthChanged;

    public InputActionReference moveAction;

    public AudioSource shootSFX;
    public AudioClip shootSoundClip;

    Rigidbody2D rb;
    Vector2 input;

    public Vector2 LastMoveDir { get; private set; } = Vector2.up;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        sword = GetComponent<Sword>();
    }

    void Start()
    {
        OnMaxHealthChanged?.Invoke(maxHP);
        OnHealthChanged?.Invoke(HP);
    }

    void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
    }

    void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
    }

    void Update()
    {
        input = moveAction != null ? moveAction.action.ReadValue<Vector2>() : ReadKeyboard();

        if (input.sqrMagnitude > 0.01f)
            LastMoveDir = input.normalized;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

            PlayShootSound();

            if (Input.GetMouseButtonDown(0))
            {
                if (sword.currentType != SwordType.Sun)
                    sword.SetSwordType(direction);
                Debug.Log("Clicked");
            }
            if (Input.GetMouseButton(0))
            {
                if (sword.currentType == SwordType.Sun)
                    sword.SetSwordType(direction);
                Debug.Log("Holding Click");
            }
        }
    }

    private void PlayShootSound()
    {
        if (SoundFXManager.instance != null && shootSoundClip != null)
        {
            SoundFXManager.instance.PlaySoundFXClip(shootSoundClip, transform);
        }
        else if (shootSFX != null && shootSFX.clip != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
            shootSFX.volume = savedVolume;
            shootSFX.Play();
        }
    }

    public void takeDamage(float dmg)
    {
        Debug.Log($"Taking {dmg} damage. Current HP: {HP}");

        if (dmg >= HP)
        {
            HP = 0;
            OnHealthChanged?.Invoke(HP);
            Debug.Log("Player died! Restarting scene...");
            RestartScene();
        }
        else
        {
            timeSinceLastDMG = 0;
            HP -= dmg;
            OnHealthChanged?.Invoke(HP);
            Debug.Log($"New HP: {HP}");
        }
    }

    private void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public float GetCurrentHP() => HP;
    public float GetMaxHP() => maxHP;

    void FixedUpdate()
    {
        if (HP != maxHP) timeSinceLastDMG += Time.fixedDeltaTime;
        rb.MovePosition(rb.position + input.normalized * moveSpeed * Time.fixedDeltaTime);

        if (timeSinceLastDMG >= hpRecoveryTimer && HP != maxHP)
        {
            HP += 3 * Time.fixedDeltaTime;
            OnHealthChanged?.Invoke(HP);
            if (HP > maxHP)
            {
                HP = maxHP;
                OnHealthChanged?.Invoke(HP);
            }
        }
    }

    static Vector2 ReadKeyboard()
    {
        var k = Keyboard.current;
        if (k == null) return Vector2.zero;

        float x = (k.dKey.isPressed ? 1 : 0) - (k.aKey.isPressed ? 1 : 0);
        float y = (k.wKey.isPressed ? 1 : 0) - (k.sKey.isPressed ? 1 : 0);

        if (x == 0 && y == 0)
        {
            x = (k.rightArrowKey.isPressed ? 1 : 0) - (k.leftArrowKey.isPressed ? 1 : 0);
            y = (k.upArrowKey.isPressed ? 1 : 0) - (k.downArrowKey.isPressed ? 1 : 0);
        }

        return new Vector2(x, y);
    }
}
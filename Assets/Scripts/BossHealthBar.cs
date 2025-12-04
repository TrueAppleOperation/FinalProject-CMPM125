using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthBar : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Colors")]
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color mediumHealthColor = Color.yellow;
    [SerializeField] private Color lowHealthColor = Color.red;

    [Header("Boss Reference")]
    [SerializeField] private BossScript boss;

    private void Start()
    {
        if (boss == null)
        {
            boss = FindObjectOfType<BossScript>();
        }

        if (boss != null)
        {
            boss.OnHealthChanged += UpdateHealthBar;
            boss.OnMaxHealthChanged += UpdateMaxHealth;
            UpdateMaxHealth(boss.GetMaxHP());
            UpdateHealthBar(boss.GetCurrentHP());
        }
        else
        {
            Debug.LogWarning("BossHealthBar: No boss assigned or found!");
            StartCoroutine(FindBossDelayed());
        }
    }

    private System.Collections.IEnumerator FindBossDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        boss = FindObjectOfType<BossScript>();

        if (boss != null)
        {
            boss.OnHealthChanged += UpdateHealthBar;
            boss.OnMaxHealthChanged += UpdateMaxHealth;
            UpdateMaxHealth(boss.GetMaxHP());
            UpdateHealthBar(boss.GetCurrentHP());
        }
    }

    private void UpdateHealthBar(float currentHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (healthText != null && boss != null)
        {
            healthText.text = $"{Mathf.Ceil(currentHealth)}/{boss.GetMaxHP()}";
        }

        // Update color based on health percentage
        if (fillImage != null && boss != null)
        {
            float healthPercent = currentHealth / boss.GetMaxHP();

            if (healthPercent > 0.6f)
                fillImage.color = fullHealthColor;
            else if (healthPercent > 0.3f)
                fillImage.color = mediumHealthColor;
            else
                fillImage.color = lowHealthColor;
        }
    }

    private void UpdateMaxHealth(float maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
        }
    }

    private void OnDestroy()
    {
        if (boss != null)
        {
            boss.OnHealthChanged -= UpdateHealthBar;
            boss.OnMaxHealthChanged -= UpdateMaxHealth;
        }
    }

    public void AssignBoss(BossScript targetBoss)
    {
        if (boss != null)
        {
            boss.OnHealthChanged -= UpdateHealthBar;
            boss.OnMaxHealthChanged -= UpdateMaxHealth;
        }

        boss = targetBoss;

        if (boss != null)
        {
            boss.OnHealthChanged += UpdateHealthBar;
            boss.OnMaxHealthChanged += UpdateMaxHealth;
            UpdateMaxHealth(boss.GetMaxHP());
            UpdateHealthBar(boss.GetCurrentHP());
        }
    }
}
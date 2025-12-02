using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Colors")]
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color mediumHealthColor = Color.yellow;
    [SerializeField] private Color lowHealthColor = Color.red;

    [Header("Enemy Reference")]
    [SerializeField] private EnemyScript enemy;

    private void Start()
    {
        if (enemy == null)
        {
            enemy = FindObjectOfType<EnemyScript>();
        }

        if (enemy != null)
        {
            enemy.OnHealthChanged += UpdateHealthBar;
            enemy.OnMaxHealthChanged += UpdateMaxHealth;
            UpdateMaxHealth(enemy.GetMaxHP());
            UpdateHealthBar(enemy.GetCurrentHP());
        }
        else
        {
            Debug.LogWarning("EnemyHealthBar: No enemy assigned or found!");
            StartCoroutine(FindEnemyDelayed());
        }
    }

    private System.Collections.IEnumerator FindEnemyDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        enemy = FindObjectOfType<EnemyScript>();

        if (enemy != null)
        {
            enemy.OnHealthChanged += UpdateHealthBar;
            enemy.OnMaxHealthChanged += UpdateMaxHealth;
            UpdateMaxHealth(enemy.GetMaxHP());
            UpdateHealthBar(enemy.GetCurrentHP());
        }
    }

    private void UpdateHealthBar(float currentHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (healthText != null && enemy != null)
        {
            healthText.text = $"{Mathf.Ceil(currentHealth)}/{enemy.GetMaxHP()}";
        }

        // Update color based on health percentage
        if (fillImage != null && enemy != null)
        {
            float healthPercent = currentHealth / enemy.GetMaxHP();

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
        if (enemy != null)
        {
            enemy.OnHealthChanged -= UpdateHealthBar;
            enemy.OnMaxHealthChanged -= UpdateMaxHealth;
        }
    }

    public void AssignEnemy(EnemyScript targetEnemy)
    {
        if (enemy != null)
        {
            enemy.OnHealthChanged -= UpdateHealthBar;
            enemy.OnMaxHealthChanged -= UpdateMaxHealth;
        }

        enemy = targetEnemy;

        if (enemy != null)
        {
            enemy.OnHealthChanged += UpdateHealthBar;
            enemy.OnMaxHealthChanged += UpdateMaxHealth;
            UpdateMaxHealth(enemy.GetMaxHP());
            UpdateHealthBar(enemy.GetCurrentHP());
        }
    }
}
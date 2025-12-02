using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Colors")]
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color mediumHealthColor = Color.yellow;
    [SerializeField] private Color lowHealthColor = Color.red;

    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player != null)
        {
            player.OnHealthChanged += UpdateHealthBar;
            player.OnMaxHealthChanged += UpdateMaxHealth;
            UpdateMaxHealth(player.GetMaxHP());
            UpdateHealthBar(player.GetCurrentHP());
        }
    }

    private void UpdateHealthBar(float currentHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{Mathf.Ceil(currentHealth)}/{player.GetMaxHP()}";
        }

        // Update color based on health percentage
        if (fillImage != null)
        {
            float healthPercent = currentHealth / player.GetMaxHP();

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
        if (player != null)
        {
            player.OnHealthChanged -= UpdateHealthBar;
            player.OnMaxHealthChanged -= UpdateMaxHealth;
        }
    }
}
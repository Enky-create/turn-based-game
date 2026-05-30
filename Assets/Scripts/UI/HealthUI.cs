using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private Image image;
    void Start()
    {
        healthComponent.OnHealthChanged += HealthComponent_OnHealthChanged;
    }
    private void HealthComponent_OnHealthChanged(object sender, HealthComponent.OnHealthChangedEventArgs e)
    {
        var maxHealth = healthComponent.GetMaxHealth();
        var currentHealth = e.health;
        image.fillAmount = (float)currentHealth/maxHealth;
    }
    void OnDestroy()
    {
        healthComponent.OnHealthChanged -= HealthComponent_OnHealthChanged;
    }
}

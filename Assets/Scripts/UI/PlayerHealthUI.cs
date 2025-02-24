using Health;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private RectTransform healthbarRectTransform;
    
        [Space]
        [SerializeField] float widthPerHealthUnit = 10f;
    
        private Vector2 initialPosition;
        private void Start()
        {
            initialPosition = new Vector2(
                healthbarRectTransform.anchoredPosition.x - healthbarRectTransform.sizeDelta.x/2,
                healthbarRectTransform.anchoredPosition.y);
        }

        // Updates the max health in the slider, and resizes and repositions the health bar,
        // ensuring it expands to the right while maintaining its left-aligned position.
        private void UpdateMaxHealth(float maxHealthValue)
        {
            Debug.Log(initialPosition.ToString());
            healthSlider.maxValue = maxHealthValue;
            float newWidth = widthPerHealthUnit * maxHealthValue;
            healthbarRectTransform.sizeDelta = new Vector2(newWidth, healthbarRectTransform.sizeDelta.y);
            healthbarRectTransform.anchoredPosition = new Vector2(initialPosition.x + newWidth/2, initialPosition.y);
        }

        private void UpdateHealth(float currentHealth)
        {
            healthSlider.value = currentHealth;
        }
    
    
        private void OnEnable()
        {
            PlayerHealth.OnChangePlayerHealth += UpdateHealth;
            PlayerHealth.OnChangePlayerMaxHealth += UpdateMaxHealth;
        }

        private void OnDisable()
        {
            PlayerHealth.OnChangePlayerHealth -= UpdateHealth;
            PlayerHealth.OnChangePlayerMaxHealth -= UpdateMaxHealth;
        }
    }
}

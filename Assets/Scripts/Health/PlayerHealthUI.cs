using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    private Vector2 position;
    private float widthPerUnit = 10f;
    [SerializeField]private RectTransform rectTransform;

    private void Start()
    {
        position = rectTransform.position;
    }


    private void UpdateMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        float size = widthPerUnit * maxHealth;
        rectTransform.sizeDelta = new Vector2(size, rectTransform.sizeDelta.y);
        rectTransform.anchoredPosition = new Vector2(position.x + size/2, position.y);
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

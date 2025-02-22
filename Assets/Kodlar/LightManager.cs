using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal; // Light2D için gerekli!

public class LightManager : MonoBehaviour
{
    public float maxLight = 100f;
    public float currentLight;
    public float lightDecayRate = 5f;
    public Slider lightSlider;
    public Light2D playerLight; // Light2D ışık bileşeni
    public GameObject deathScreen; // Ölüm ekranı paneli

    private bool isTalking = false; // NPC ile konuşma durumu

    private void Start()
    {
        currentLight = maxLight;
        UpdateUI();
        UpdateLight();

        if (deathScreen != null)
            deathScreen.SetActive(false); // Başlangıçta kapalı olmalı
    }

    private void Update()
    {
        if (!isTalking && currentLight > 0) // NPC ile konuşulmuyorsa ışık azalsın
        {
            currentLight -= lightDecayRate * Time.deltaTime;
            if (currentLight <= 0)
            {
                currentLight = 0;
                Die();
            }
            UpdateUI();
            UpdateLight();
        }
    }

    public void TakeDamage(float amount)
    {
        currentLight -= amount;
        if (currentLight <= 0)
        {
            currentLight = 0;
            Die();
        }
        UpdateUI();
        UpdateLight();
    }

    public void CollectCrystal(float amount)
    {
        currentLight += amount;
        if (currentLight > maxLight)
            currentLight = maxLight;
        UpdateUI();
        UpdateLight();
    }

    void UpdateUI()
    {
        if (lightSlider != null)
            lightSlider.value = currentLight / maxLight;
    }

    void UpdateLight()
    {
        if (playerLight != null)
        {
            playerLight.intensity = Mathf.Lerp(0, 2, currentLight / maxLight);
        }
    }

    void Die()
    {
        Debug.Log("Işık söndü, karakter öldü!");

        if (playerLight != null)
            playerLight.enabled = false;

        if (deathScreen != null)
        {
            deathScreen.SetActive(true); // Ölüm ekranını aç
            Time.timeScale = 0f; // Oyunu durdur
        }
    }

    // NPC ile konuşmaya başlandığında ışık azalmayı durdur
    public void StartTalking()
    {
        isTalking = true;
    }

    // NPC ile konuşma bittiğinde ışık tekrar azalmaya başlasın
    public void StopTalking()
    {
        isTalking = false;
    }
}

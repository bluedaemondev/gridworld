using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class EnemiesRemainingUI : MonoBehaviour
{
    [SerializeField] Text displayRemaining;
    [SerializeField] Slider sliderRemaining;

    string originalText;
    private void Awake()
    {
        originalText = displayRemaining.text;
    }
    public void UpdateUI(int countRemaining, int maxEnemies)
    {
        this.displayRemaining.text = originalText + countRemaining;
        this.sliderRemaining.maxValue = maxEnemies;
        this.sliderRemaining.value = Mathf.Clamp(countRemaining, 0, maxEnemies);
    }
}

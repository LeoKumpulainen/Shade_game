using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCounterDisplay : MonoBehaviour
{
    public TextMeshProUGUI deathCounterText; // Drag your UI Text here in the Inspector

    private void Update()
    {
        if (DeathCounter.Instance != null)
        {
            deathCounterText.text = "Deaths: " + DeathCounter.Instance.deathCount;
        }
        else
        {
            deathCounterText.text = "Deaths: (No Counter)";
        }
    }
}

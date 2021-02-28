using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text healthText;

    private void Update()
    {
        healthText.text = "Health: " + PlayerHealth.playerHealth.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsText : MonoBehaviour
{
    [SerializeField]
    TMP_Text resultsText;

    private void Awake()
    {
        if (PlayerHealth.playerHealth > 0)
        {
            resultsText.text = "GAME COMPLETE";
        }
        else
        {
            resultsText.text = "GAME OVER";
        }
    }
}

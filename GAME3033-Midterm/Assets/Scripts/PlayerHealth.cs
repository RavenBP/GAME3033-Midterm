using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int playerHealth = 100;

    public void SetHealth(int newHealth)
    {
        playerHealth = newHealth;
    }

    public void ModifyHealth(int amountModified)
    {
        playerHealth += amountModified;
    }
}

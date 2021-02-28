using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private Property property;

    private BoxCollider boxCollider;
    private AudioSource audioSource;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();

        if (property == Property.Safe)
        {
            boxCollider.isTrigger = false;
        }
        else
        {
            boxCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (property)
        {
            case Property.Safe:
                break;
            case Property.Dangerous:
                PlayerHealth.playerHealth += -15;
                audioSource.Play();
                break;
            case Property.Pickup:
                PlayerHealth.playerHealth = Mathf.Clamp((PlayerHealth.playerHealth + 20), 0, 100);
                Destroy(this.gameObject);
                break;
        }
    }
}

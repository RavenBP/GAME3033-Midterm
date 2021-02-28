using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    private Property property;

    private SphereCollider sphereCollider;
    private AudioSource audioSource;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();

        if (property == Property.Pickup || property == Property.Dangerous)
        {
            sphereCollider.isTrigger = true;
        }
        else
        {
            sphereCollider.isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player touched health pickup");

            if (property == Property.Pickup)
            {
                PlayerHealth.playerHealth = Mathf.Clamp((PlayerHealth.playerHealth + 20), 0, 100);
                Destroy(this.gameObject);
            }
            else
            {
                PlayerHealth.playerHealth -= 15;
                audioSource.Play();
            }
        }
    }
}

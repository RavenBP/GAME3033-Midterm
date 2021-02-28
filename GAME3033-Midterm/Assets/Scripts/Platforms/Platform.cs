using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    Property platformProperty;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch(platformProperty)
            {
                case Property.Safe:
                    //Debug.Log("This platform is safe. :)");
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
}
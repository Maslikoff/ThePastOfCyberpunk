using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int helthRestor = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    private AudioSource _pickupSource;

    private void Awake()
    {
        _pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable && damageable.Health < damageable.MaxHealth)
        {
            bool wasHealth = damageable.Heal(helthRestor);

            if (wasHealth)
            {
                if (_pickupSource)
                    AudioSource.PlayClipAtPoint(_pickupSource.clip, gameObject.transform.position, _pickupSource.volume);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DerectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;

    public List<Collider2D> colliders = new List<Collider2D>();

    Collider2D _col;

    void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliders.Remove(collision);

        if(colliders.Count <= 0)
            noCollidersRemain.Invoke();
    }
}

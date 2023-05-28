using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Collider _collider;
    public float damage = 10;
    public string targetTag;

    void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }


    void Update()
    {
      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Character character = other.GetComponent<Character>();
            character.TakeDamage(damage);
        }
    }
}

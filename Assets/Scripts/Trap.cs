using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Trap : MonoBehaviour
{
    [SerializeField] private Attack _attack;


    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
          _attack.PlayerTakeDamage(5);    
          Invoke(nameof(OnTriggerEnter), 2f); 
        }
    }
}

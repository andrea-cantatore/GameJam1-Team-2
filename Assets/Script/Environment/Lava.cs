using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] float bounceForce;
    private bool _canBounce;
    PlayerController playerController;


    private void Update()
    {
        if (!_canBounce && playerController != null)
        {
            _canBounce = playerController.IsGrounded;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out playerController))
        {
            Rigidbody rb = playerController.GetComponent<Rigidbody>();
            if (playerController.IsGroundPounding && _canBounce)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
                playerController.IsGroundPounding = false;
                _canBounce = false;
            }
            else EventManager.OnPlayerChangeHp(-1);

            
        }
    }

    
}
            
               
                
            
            

                

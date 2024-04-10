using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] float bounceForce;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out var playerController))
        {
            Rigidbody rb = playerController.GetComponent<Rigidbody>();
            if (playerController._isGroundPounding)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
                playerController._isGroundPounding = false;
            }
            else EventManager.OnPlayerChangeHp(-1);

            
        }
    }

    
}
            
               
                
            
            

                

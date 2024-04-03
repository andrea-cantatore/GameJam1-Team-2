using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPickuppable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GroundPoundPickuppable");
        if (other.TryGetComponent(out IPlayer playerInterface))
        {
            playerInterface.DoubleDashUnlock();
            Destroy(gameObject);
        }
    }
}

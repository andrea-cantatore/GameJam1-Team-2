using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPickuppable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPlayer playerInterface))
        {
            Debug.Log("Double dash unlocked!");
            EventManager.OnDoubleDashUnlock?.Invoke(true);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPickuppable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPlayer playerInterface))
        {
            Debug.Log("Double dash unlocked!");
            EventManager.OnDoubleJumpUnlock?.Invoke(true);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class GroundPoundPickuppable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPlayer playerInterface))
        {
            Debug.Log("GroundPound Unlocked!");
            EventManager.OnGroundPoundUnlock?.Invoke(true);
            Destroy(gameObject);
        }
    }
}

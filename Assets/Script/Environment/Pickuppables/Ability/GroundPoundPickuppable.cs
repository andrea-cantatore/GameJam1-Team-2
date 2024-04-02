using UnityEngine;

public class GroundPoundPickuppable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GroundPoundPickuppable");
        if (other.TryGetComponent(out IPlayer playerInterface))
        {
            playerInterface.GroundPoundUnlock();
            Destroy(gameObject);
        }
    }
}

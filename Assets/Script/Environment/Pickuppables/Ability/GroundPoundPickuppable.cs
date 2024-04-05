using UnityEngine;

public class GroundPoundPickuppable : MonoBehaviour
{
    [SerializeField] AudioData AudioData;
    AudioClip clip;

    private void Awake()
    {
        clip = AudioData.sfx_pickupSound;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPlayer playerInterface))
        {
            Debug.Log("GroundPound Unlocked!");
            EventManager.OnGroundPoundUnlock?.Invoke(true);
            AudioManager.instance.PlaySFX(clip, transform);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Player : MonoBehaviour
{
    public static BGM_Player Instance;

    [SerializeField] AudioSource BGM_Music;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}

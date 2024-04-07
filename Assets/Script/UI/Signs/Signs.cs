using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signs : MonoBehaviour , IReadable
{
    [TextArea(10, 14)]
    public string textToShow = "Hello traveler, if you are reading this i'm sorry for you.\r\nYou have been trapped, but do not worry the exit door is right in front of you! \r\nBut first, why don't you do some stretching?\r\nUse WASD to move around, SPACE to jump and Shift to dash!\r\n";
    bool playerCanRead;


    private void OnEnable()
    {
        EventManager.OnInteracting += GiveMessage;
    }

    private void OnDisable()
    {
        EventManager.OnInteracting -= GiveMessage;
    }

    public void GiveMessage()
    {
        if (playerCanRead)
            EventManager.OnEnterDialogue(textToShow);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerCanRead = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerCanRead = false;

        EventManager.OnExitDialogue?.Invoke();
    }
}

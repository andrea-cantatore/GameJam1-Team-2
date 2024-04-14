using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] Animator animator1;
    [SerializeField] Animator animator2;
    [SerializeField] private bool _isDoubleInteraction;
    private int _interactionCount = 0;

    public void interact(bool isActivated)
    {
        if(isActivated)
        {
            if (_isDoubleInteraction)
            {
                if (_interactionCount < 2)
                {
                    _interactionCount++;
                }
                else
                {
                    animator1.SetBool("Open", true);
                    animator2.SetBool("Open", true);
                }
            }
            else
            {
                animator1.SetBool("Open", true);
                animator2.SetBool("Open", true);
            }
        }
        else
        {
            if (_isDoubleInteraction)
            {
                _interactionCount--;
                animator1.SetBool("Open", false);
                animator2.SetBool("Open", false);
            }
            else
            {
                animator1.SetBool("Open", false);
                animator2.SetBool("Open", false);
            }
        }
    }
}

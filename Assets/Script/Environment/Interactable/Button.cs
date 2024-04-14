using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject[] _interactionObject;
    [SerializeField] private int _durationTime = 15;
    private bool _isActivated, _isActivable;
    private float _activationTime = 0.5f;
    private float _activationTimer, _durationTimer;
    [SerializeField] private Animator _animator;
    
    
    private void Update()
    {
        if(!_isActivable)
        {
            _activationTimer += Time.deltaTime;
            if (_activationTimer >= _activationTime)
            {
                _isActivable = true;
                _activationTimer = 0;
            }
        }
        if (_isActivated)
        {
            _durationTimer += Time.deltaTime;
            if(_durationTimer >= _durationTime)
            {
                foreach (var obj in _interactionObject)
                {
                    if(obj.TryGetComponent(out IInteract interactable))
                        interactable.interact(false);
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), 0.1f);
                _isActivated = false;
                _isActivable = false;
                _durationTimer = 0;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out IPlayer player))
        {
            if(player.IsInteracting() && !_isActivated && _isActivable)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), 0.1f);
                foreach (var obj in _interactionObject)
                {
                    if(obj.TryGetComponent(out IInteract interactable))
                        interactable.interact(true);
                }
                _isActivated = true;
                _isActivable = false;
                _animator.SetBool("StartAnimation", true);
                EventManager.OnTimerStarted?.Invoke(_durationTime);
                _animator.SetBool("StartAnimation", false);
            }
            else if(player.IsInteracting() && _isActivated && _isActivable)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), 0.1f);
                foreach (var obj in _interactionObject)
                {
                    if(obj.TryGetComponent(out IInteract interactable))
                        interactable.interact(false);
                }
                _isActivated = false;
                _isActivable = false;
                _durationTimer = 0;
                EventManager.OnTimerCanceled?.Invoke();
            }
        }
    }
}

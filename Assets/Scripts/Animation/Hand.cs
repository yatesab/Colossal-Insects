using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    //Animation
    [SerializeField] private float animationSpeed;
    private Animator _animator;

    [Space]

    [SerializeField] private InputActionProperty gripReference;
    private InputAction gripAction;
    
    private float gripTarget;
    private float gripCurrent;
    private string animatorGripParam = "Grip";

    [Space]

    [SerializeField] private InputActionProperty triggerReference;
    private InputAction triggerAction;
    
    private float triggerTarget;
    private float triggerCurrent;
    private string animatorTriggerParam = "Trigger";
    
    [Space]

    [SerializeField] private InputActionProperty thumbReference;
    private InputAction thumbAction;
    
    private float thumbTarget;
    private float thumbCurrent;
    private string animatorThumbParam = "Thumb";

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        gripAction = gripReference.action;
        triggerAction = triggerReference.action;
        thumbAction = thumbReference.action;

        gripAction.Enable();
        triggerAction.Enable();
        thumbAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        gripTarget = gripAction.ReadValue<float>();
        triggerTarget = triggerAction.ReadValue<float>();
        thumbTarget = thumbAction.ReadValue<float>();

        AnimateHand();
    }

    private void AnimateHand()
    {
        if(gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            _animator.SetFloat(animatorGripParam, gripCurrent);
        } 
        if(triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            _animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
        if (thumbCurrent != thumbTarget)
        {
            thumbCurrent = Mathf.MoveTowards(thumbCurrent, thumbTarget, Time.deltaTime * animationSpeed);
            _animator.SetFloat(animatorThumbParam, thumbCurrent);
        }
    }
}

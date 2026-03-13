using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class FPSInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    public event Action OnAimStarted;
    public event Action OnAimCanceled;

    public event Action OnAlpha1Pressed;
    public event Action OnAlpha2Pressed;

    public event Action OnAttackStarted;
    public event Action OnAttackCanceled;

    public event Action CrouchStarted;
    public event Action CrouchCanceled;

    public event Action JumpEvent;
    public event Action SprintStarted;
    public event Action SprintCanceled;
    
    public event Action InteractEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            JumpEvent?.Invoke();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            SprintStarted?.Invoke();
        if (context.canceled)
            SprintCanceled?.Invoke();
    }

   

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            InteractEvent?.Invoke();
    }

    void OnEnable()
    {
        var playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Aim"].performed += _ => OnAimStarted?.Invoke();
        playerInput.actions["Aim"].canceled += _ => OnAimCanceled?.Invoke();


        playerInput.actions["Sprint"].performed += _ => SprintStarted?.Invoke();
        playerInput.actions["Sprint"].canceled += _ => SprintCanceled?.Invoke();

        playerInput.actions["Alpha1"].performed += _ => OnAlpha1Pressed?.Invoke();
        playerInput.actions["Alpha2"].performed += _ => OnAlpha2Pressed?.Invoke();

        playerInput.actions["Crouch"].performed += _ => CrouchStarted?.Invoke();
        playerInput.actions["Crouch"].canceled += _ => CrouchCanceled?.Invoke();
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;

        playerInput.actions["Look"].performed += OnLook;
        playerInput.actions["Look"].canceled += OnLook;

        playerInput.actions["Attack"].performed += _ => OnAttackStarted?.Invoke();
        playerInput.actions["Attack"].canceled += _ => OnAttackCanceled?.Invoke();


        playerInput.actions["Jump"].performed += OnJump;
        //PlayerStateManager.OnToggleControls += ToggleScript;
    }

    void OnDisable()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Move"].performed -= OnMove;
        playerInput.actions["Move"].canceled -= OnMove;

        playerInput.actions["Look"].performed -= OnLook;
        playerInput.actions["Look"].canceled -= OnLook;

        playerInput.actions["Attack"].performed -= _ => OnAttackStarted?.Invoke();
        playerInput.actions["Attack"].canceled -= _ => OnAttackCanceled?.Invoke();
        playerInput.actions["Aim"].performed -= _ => OnAimStarted?.Invoke();
        playerInput.actions["Aim"].canceled -= _ => OnAimCanceled?.Invoke();


        playerInput.actions["Alpha1"].performed -= _ => OnAlpha1Pressed?.Invoke();
        playerInput.actions["Alpha2"].performed -= _ => OnAlpha2Pressed?.Invoke();
        playerInput.actions["Jump"].performed -= OnJump;

        playerInput.actions["Sprint"].performed -= _ => SprintStarted?.Invoke();
        playerInput.actions["Sprint"].canceled -= _ => SprintCanceled?.Invoke();

        playerInput.actions["Crouch"].performed -= _ => CrouchStarted?.Invoke();
        playerInput.actions["Crouch"].canceled -= _ => CrouchCanceled?.Invoke();
    //    PlayerStateManager.OnToggleControls -= ToggleScript;

    //}
    
    //void ToggleScript(bool state)
    //{
    //    this.enabled = state; // Scripti tamamen kapatýr veya açar
    }
}

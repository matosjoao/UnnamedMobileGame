// GENERATED AUTOMATICALLY FROM 'Assets/Game/InputActions/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""CharacterMovement"",
            ""id"": ""81778d4f-b888-4a7b-9fd1-11069935ae05"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""205ba3f6-2ec9-4e9b-830e-a6f2ac167961"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""63631404-2bbe-4f8c-ab22-9a2654c3ab62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JumpHeld"",
                    ""type"": ""PassThrough"",
                    ""id"": ""71cef123-3603-47f4-bd7a-1d40e1650b3e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis AD"",
                    ""id"": ""5b420ebf-039c-4429-a23d-ce94518d79c1"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1c3e9f2a-6e09-4139-acd0-ab3cceb39b45"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""81050e2a-b367-45b8-8e21-a77a04475d44"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8858cedd-0b34-45f5-8780-87cdfce521a3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85183b74-e3c8-410a-bcfb-e8f2c6b7d01d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JumpHeld"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CharacterActions"",
            ""id"": ""b616b690-7097-4d22-ba43-0e6e6c5ac748"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""f54b5425-886b-4525-8246-08b128bb84d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""516dc194-e5d4-4d75-9ecd-323d8d4dba45"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterMovement
        m_CharacterMovement = asset.FindActionMap("CharacterMovement", throwIfNotFound: true);
        m_CharacterMovement_Movement = m_CharacterMovement.FindAction("Movement", throwIfNotFound: true);
        m_CharacterMovement_Jump = m_CharacterMovement.FindAction("Jump", throwIfNotFound: true);
        m_CharacterMovement_JumpHeld = m_CharacterMovement.FindAction("JumpHeld", throwIfNotFound: true);
        // CharacterActions
        m_CharacterActions = asset.FindActionMap("CharacterActions", throwIfNotFound: true);
        m_CharacterActions_Attack = m_CharacterActions.FindAction("Attack", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // CharacterMovement
    private readonly InputActionMap m_CharacterMovement;
    private ICharacterMovementActions m_CharacterMovementActionsCallbackInterface;
    private readonly InputAction m_CharacterMovement_Movement;
    private readonly InputAction m_CharacterMovement_Jump;
    private readonly InputAction m_CharacterMovement_JumpHeld;
    public struct CharacterMovementActions
    {
        private @InputActions m_Wrapper;
        public CharacterMovementActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CharacterMovement_Movement;
        public InputAction @Jump => m_Wrapper.m_CharacterMovement_Jump;
        public InputAction @JumpHeld => m_Wrapper.m_CharacterMovement_JumpHeld;
        public InputActionMap Get() { return m_Wrapper.m_CharacterMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterMovementActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterMovementActions instance)
        {
            if (m_Wrapper.m_CharacterMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnJump;
                @JumpHeld.started -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnJumpHeld;
                @JumpHeld.performed -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnJumpHeld;
                @JumpHeld.canceled -= m_Wrapper.m_CharacterMovementActionsCallbackInterface.OnJumpHeld;
            }
            m_Wrapper.m_CharacterMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @JumpHeld.started += instance.OnJumpHeld;
                @JumpHeld.performed += instance.OnJumpHeld;
                @JumpHeld.canceled += instance.OnJumpHeld;
            }
        }
    }
    public CharacterMovementActions @CharacterMovement => new CharacterMovementActions(this);

    // CharacterActions
    private readonly InputActionMap m_CharacterActions;
    private ICharacterActionsActions m_CharacterActionsActionsCallbackInterface;
    private readonly InputAction m_CharacterActions_Attack;
    public struct CharacterActionsActions
    {
        private @InputActions m_Wrapper;
        public CharacterActionsActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_CharacterActions_Attack;
        public InputActionMap Get() { return m_Wrapper.m_CharacterActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterActionsActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterActionsActions instance)
        {
            if (m_Wrapper.m_CharacterActionsActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_CharacterActionsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_CharacterActionsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_CharacterActionsActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_CharacterActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public CharacterActionsActions @CharacterActions => new CharacterActionsActions(this);
    public interface ICharacterMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnJumpHeld(InputAction.CallbackContext context);
    }
    public interface ICharacterActionsActions
    {
        void OnAttack(InputAction.CallbackContext context);
    }
}

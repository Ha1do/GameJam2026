using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class PhoneToggle : MonoBehaviour
{
    [SerializeField] private GameObject phoneAnchor;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private FirstPersonController firstPersonController;

    [Header("Phone Movement Slow")]
    [SerializeField] private float slowedMoveSpeed = 1f;
    [SerializeField] private float slowedSprintSpeed = 1.5f;

    private InputAction phoneShow;
    private InputAction phoneHide;

    private float normalMoveSpeed;
    private float normalSprintSpeed;

    public bool IsPhoneOpen { get; private set; }

    void Awake()
    {
        if (playerInput == null)
            playerInput = GetComponent<PlayerInput>();

        if (firstPersonController == null)
            firstPersonController = GetComponent<FirstPersonController>();
    }

    void Start()
    {
        if (phoneAnchor != null)
            phoneAnchor.SetActive(false);

        IsPhoneOpen = false;

        if (firstPersonController != null)
        {
            normalMoveSpeed = firstPersonController.MoveSpeed;
            normalSprintSpeed = firstPersonController.SprintSpeed;
        }
    }

    void OnEnable()
    {
        if (playerInput == null)
            return;

        phoneShow = playerInput.actions["PhoneShow"];
        phoneHide = playerInput.actions["PhoneHide"];
    }

    void Update()
    {
        if (phoneAnchor == null || phoneShow == null || phoneHide == null)
            return;

        if (phoneShow.WasPressedThisFrame())
            OpenPhone();

        if (phoneHide.WasPressedThisFrame())
            ClosePhone();
    }

    void OpenPhone()
    {
        phoneAnchor.SetActive(true);
        IsPhoneOpen = true;

        if (firstPersonController != null)
        {
            firstPersonController.MoveSpeed = slowedMoveSpeed;
            firstPersonController.SprintSpeed = slowedSprintSpeed;
        }
    }

    void ClosePhone()
    {
        phoneAnchor.SetActive(false);
        IsPhoneOpen = false;

        if (firstPersonController != null)
        {
            firstPersonController.MoveSpeed = normalMoveSpeed;
            firstPersonController.SprintSpeed = normalSprintSpeed;
        }
    }
}
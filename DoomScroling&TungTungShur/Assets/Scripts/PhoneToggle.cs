using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneToggle : MonoBehaviour
{
    [SerializeField] private GameObject phoneAnchor;
    [SerializeField] private PlayerInput playerInput;

    private InputAction phoneShow;
    private InputAction phoneHide;

    void Awake()
    {
        if (playerInput == null)
            playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        if (phoneAnchor != null)
            phoneAnchor.SetActive(false);
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
            phoneAnchor.SetActive(true);

        if (phoneHide.WasPressedThisFrame())
            phoneAnchor.SetActive(false);
    }
}
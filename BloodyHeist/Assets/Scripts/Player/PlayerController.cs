using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

    public class PlayerController : MonoBehaviour {

        [SerializeField]
        private float moveSpeed = 5f;

        public Rigidbody2D rb;
        public PlayerVisual visual;
        public PlayerEnergy energy;
        public PlayerInput input;

        private InputAction moveAction;
        private InputAction transformAction;
        private Vector2 moveInput;
        private float transformInput;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;

            moveAction = input.actions["Move"];
            transformAction = input.actions["Transform"];

            if (energy != null)
                energy.Init();
        }

        private void OnEnable()
        {
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
            moveAction.Enable();

            transformAction.performed += OnTransform;
            transformAction.canceled += OnTransform;
            transformAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMove;
            moveAction.Disable();

            transformAction.performed -= OnTransform;
            transformAction.canceled -= OnTransform;
            transformAction.Disable();
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleTransform();
        }

        #region Input Handlers
        private void OnMove(InputAction.CallbackContext ctx)
        {
            moveInput = ctx.ReadValue<Vector2>();
        }

        public void OnTransform(InputAction.CallbackContext ctx)
        {
            transformInput = ctx.ReadValue<float>();
            Debug.Log("Transform input: " + transformInput);
        }
        #endregion

        #region Movement
        private void HandleMovement()
        {
            if (rb == null) return;
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

            //visual?.SetFloat("Speed", Mathf.Abs(moveInput.x));
        }

        private void HandleTransform()
        {
            // TODO - Transformation
        }
        #endregion
    }

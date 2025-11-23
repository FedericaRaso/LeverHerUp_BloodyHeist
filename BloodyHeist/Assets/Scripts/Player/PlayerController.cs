using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

    public class PlayerController : MonoBehaviour {

        [SerializeField]
        private float moveSpeed = 5f;
        [SerializeField]
        private float transformEnergy = 10;

        public Rigidbody2D rb;
        public PlayerVisual visual;
        public PlayerEnergy energy;
        public PlayerInput input;

        private InputAction moveAction;
        private InputAction transformAction;
        private InputAction interactAction;
        private Vector2 moveInput;

        private bool isPlayerFlipped;
        private bool isPlayerBat;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;

            moveAction = input.actions["Move"];
            transformAction = input.actions["Transform"];
            interactAction = input.actions["Interact"];


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

            interactAction.performed += OnInteract;
            interactAction.canceled += OnInteract;
            interactAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMove;
            moveAction.Disable();

            transformAction.performed -= OnTransform;
            transformAction.canceled -= OnTransform;
            transformAction.Disable();

            interactAction.performed -= OnInteract;
            interactAction.canceled -= OnInteract;
            interactAction.Disable();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        #region Input Handlers
        private void OnMove(InputAction.CallbackContext ctx)
        {
            moveInput = ctx.ReadValue<Vector2>();
        }

        public void OnTransform(InputAction.CallbackContext ctx)
        {
            StartTransform();
        }

        public void OnInteract(InputAction.CallbackContext ctx)
        {
            Interact();
        }
        #endregion

        #region Movement
        private void HandleMovement()
        {
            if (rb == null) return;
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

            float speed = rb.linearVelocity.x;
            visual?.FlipX(speed < 0);
            visual?.SetAnimatorParameter("IsMoving", speed != 0);
        }

        private void StartTransform()
        {
            if(!isPlayerBat && energy.HasEnoughEnergy(transformEnergy)){
                isPlayerBat = true;
                energy.Consume(transformEnergy);
                visual?.SetAnimatorParameter("Transform");
                visual?.SetAnimatorParameter("IsBat", true);
                Debug.Log("Transformation into BAT!");
            }
            else if(isPlayerBat) {
                isPlayerBat = false;
                visual?.SetAnimatorParameter("Transform");
                visual?.SetAnimatorParameter("IsBat", false);
                Debug.Log("Transformed back!");
            }
            else{
                Debug.Log("Not enough energy to transform");
            }
        }

        private void Interact(){

        }

        #endregion
    }

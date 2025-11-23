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
        public CapsuleCollider2D capsule;
        public PlayerVisual visual;
        public PlayerEnergy energy;
        public PlayerInput input;
        public SFXManager sfxManager;

        private InputAction moveAction;
        private InputAction transformAction;
        private InputAction interactAction;
        private Vector2 moveInput;

        private bool isPlayerFlipped;
        private bool isPlayerBat;
        private bool isTransforming = false;

        private BaseInteract currentInteractable;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            capsule.size = new Vector2(capsule.size.x, 0.6f);

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
            transformAction.Enable();

            interactAction.performed += OnInteract;
            interactAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMove;
            moveAction.Disable();

            transformAction.performed -= OnTransform;
            transformAction.Disable();

            interactAction.performed -= OnInteract;
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
            if (ctx.performed)
                StartCoroutine(TransformCoroutine());
        }

        public void OnInteract(InputAction.CallbackContext ctx)
        {
            Interact();
        }
        #endregion

        private void OnTriggerEnter2D(Collider2D other)
        {
            BaseInteract interactable = other.GetComponent<BaseInteract>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                Debug.Log("Collided with: " + interactable.name);
                sfxManager.Interact();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            BaseInteract interactable = other.GetComponent<BaseInteract>();
            if (interactable != null && interactable == currentInteractable)
            {
                currentInteractable = null;
            }
        }

        #region Movement
        private void HandleMovement()
        {
            if (rb == null) return;
           
            Vector2 velocity = rb.linearVelocity;
            velocity.x = moveInput.x * moveSpeed;
            if (isPlayerBat)
            {
                velocity.y = moveInput.y * moveSpeed;
            }
            rb.linearVelocity = velocity;

            float speed = rb.linearVelocity.x;
            visual?.FlipX(speed < 0);
            visual?.SetAnimatorParameter("IsMoving", speed != 0);
        }

        private void Interact(){
            visual?.SetAnimatorParameter("Interact");
            if (currentInteractable != null)
            {
                currentInteractable.Interact(gameObject);
            }
        }

        private IEnumerator TransformCoroutine()
        {
            if (isTransforming) yield break;
            isTransforming = true;

            if (!isPlayerBat && energy.HasEnoughEnergy(transformEnergy))
            {
                isPlayerBat = true;
                energy.Consume(transformEnergy);
                visual?.SetAnimatorParameter("Transform");
                sfxManager?.TransformToBat();
                yield return new WaitForSeconds(0.5f);
                visual?.SetAnimatorParameter("IsBat", true);

                capsule.size = new Vector2(capsule.size.x, 0.4f);

                if (rb != null) rb.gravityScale = 0f;

                sfxManager?.StartFlyLoop();
            }
            else if (isPlayerBat)
            {
                isPlayerBat = false;
                visual?.SetAnimatorParameter("Transform");
                sfxManager?.TransformBack();
                yield return new WaitForSeconds(0.5f);
                visual?.SetAnimatorParameter("IsBat", false);

                capsule.size = new Vector2(capsule.size.x, 0.6f);

                if (rb != null) rb.gravityScale = 1f;

                sfxManager?.StopFlyLoop();
            }
            else
            {
                sfxManager?.TransformFailed();
                Debug.Log("Not enough energy to transform");
            }

            isTransforming = false;
        }

        #endregion
    }

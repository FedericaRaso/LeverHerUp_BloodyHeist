using System.Collections;
using UnityEngine;
using System;
using System.Linq;

    public class Player : MonoBehaviour {

        #region StaticMembers
        private static Player instance;

        public static Player Get () {
            if (instance != null) return instance;
            instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
        #endregion //StaticMembers

        #region PlayerReferences
        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private PlayerVisual playerVisual;
        [SerializeField]
        private PlayerEnergy playerEnergy;
        #endregion //PlayerReferences

        #region MonoCallbacks
        private void Awake() {
            if (instance != null && instance != this) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            if (instance != this) return;

            if (playerEnergy == null) playerEnergy = new PlayerEnergy();
            playerEnergy.Init();
        }
        #endregion

        #region PlayerEnergy 
        public bool HasEnoughEnergy(float amount) => playerEnergy.HasEnoughEnergy(amount);

        public void ConsumeEnergy(float amount) => playerEnergy.Consume(amount);

        public void RefillEnergy(float amount) => playerEnergy.Refill(amount);
        #endregion

    }

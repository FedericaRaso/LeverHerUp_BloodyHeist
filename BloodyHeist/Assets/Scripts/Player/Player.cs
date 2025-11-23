using System.Collections;
using UnityEngine;
using System;
using System.Linq;

    public class Player : MonoBehaviour {

        #region StaticMembers
        private static Player instance;

        public static Player Get () {
            if (instance != null) return instance;
            instance = GameObject.FindFirstObjectByType<Player>();
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

        #region PlayerCollectibles
        private int coins = 0;
        private int keys = 0;
        #endregion

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

        #region Collectibles
        public void AddCoins(int amount) => coins += amount;
        public bool RemoveCoins(int amount)
        {
            if (coins >= amount)
            {
                coins -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddKeys(int amount) => keys += amount;
        public bool RemoveKeys(int amount)
        {
            if (keys >= amount)
            {
                keys -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }

using UnityEngine;
using System;

    public class PlayerEnergy : MonoBehaviour
    {
        #region PrivateAttributes
        [SerializeField]
        private float maxEnergy;
        private float currentEnergy;
        #endregion

        #region Public
        public float MaxEnergy => maxEnergy;
        public float CurrentEnergy => currentEnergy;
        public event Action<float> OnEnergyUpdated;
        #endregion

        public void Init()
        {
            currentEnergy = maxEnergy;
            OnEnergyUpdated?.Invoke(currentEnergy);
        }

        public bool HasEnoughEnergy(float amount)
        {
            return currentEnergy >= amount;
        }

        public void Consume(float amount)
        {
            if (amount <= 0) return;
            currentEnergy = Mathf.Max(currentEnergy - amount, 0);
            OnEnergyUpdated?.Invoke(currentEnergy);
        }

        public void Refill(float amount)
        {
            if (amount <= 0) return;
            currentEnergy = Mathf.Min(currentEnergy + amount, MaxEnergy);
            OnEnergyUpdated?.Invoke(currentEnergy);
        }

        public void RefillFull()
        {
            currentEnergy = MaxEnergy;
            OnEnergyUpdated?.Invoke(currentEnergy);
        }
    }
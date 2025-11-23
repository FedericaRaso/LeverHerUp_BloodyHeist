using UnityEngine;

public class BloodVessel : BaseInteract
{
    [SerializeField]
    private float bloodAmount;

    public override void Interact(GameObject player)
    {
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.RefillEnergy(bloodAmount);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Unlockable : BaseInteract
{
    [SerializeField]
    private int amount;
    [SerializeField]
    private string type;
    [SerializeField]
    private string sceneToLoad; 

    public override void Interact(GameObject player)
    {
        amount = Mathf.Max(amount, 0);
        bool success = false;

        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            if(type == "coin"){
                success = playerScript.RemoveCoins(amount);
            }
            else{
                success = playerScript.RemoveCoins(amount);
            }

            if(success){
                playerScript.RefillEnergy(1000);
                Destroy(gameObject);
                SceneManager.LoadScene(sceneToLoad);
            }
            else{
                Debug.Log("Need " + amount + " of " + type);
            }
        }
    }
}

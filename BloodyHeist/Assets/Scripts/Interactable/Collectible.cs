using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private int amount;
    [SerializeField]
    private string type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Interact(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void Interact(GameObject player)
    {
        amount = Mathf.Max(amount, 0);

        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            if(type == "coin"){
                playerScript.AddCoins(amount);
            }
            else{
                playerScript.AddKeys(amount);
            }
        }
    }
}

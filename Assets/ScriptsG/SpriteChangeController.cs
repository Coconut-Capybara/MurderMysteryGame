using UnityEngine;

public class SpriteChangeController : MonoBehaviour
{
    private PlayerInteract player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerInteract>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!player.GetInPrompt() && player.GetInGame() && other.gameObject.GetComponent<ObjectProperties>() != null)
        {
            if(other.gameObject.GetComponent<ObjectProperties>().altSprite2 != null)
            {
                if (other.gameObject.GetComponent<ObjectProperties>().usesAltSprites)
                {
                    print("highlight2");
                    other.GetComponent<SpriteRenderer>().sprite = other.GetComponent<ObjectProperties>().altSprite2;
                }
            }
            if (other.gameObject.GetComponent<ObjectProperties>().highlightSprite != null &&
                !other.gameObject.GetComponent<ObjectProperties>().usesAltSprites)
            {
                print("highlight");
                other.GetComponent<SpriteRenderer>().sprite = other.GetComponent<ObjectProperties>().highlightSprite;
            }
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if (!player.GetInPrompt() && player.GetInGame() && other.gameObject.GetComponent<ObjectProperties>() != null)
        {
            if (other.gameObject.GetComponent<ObjectProperties>().usesAltSprites)
            {
                other.GetComponent<SpriteRenderer>().sprite = other.GetComponent<ObjectProperties>().altSprite1;
            }
            if (other.gameObject.GetComponent<ObjectProperties>().highlightSprite != null)
            {
                other.GetComponent<SpriteRenderer>().sprite = other.GetComponent<ObjectProperties>().itemSprite;
            }
        }
        
    }
}

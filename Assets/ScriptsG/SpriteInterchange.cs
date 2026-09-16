using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteInterchange : MonoBehaviour
{
    [Header("Object Properties Script")]
    [SerializeField] private ObjectProperties objectProperties;

    [SerializeField] private GameObject thisObject;

    private PlayerInteract player;
    private SpriteRenderer spriteRenderer;


    [SerializeField] private Camera playerCam;


    [Header("Play Mode View Variables")]
    public Sprite originalSprite;
    public Sprite hoverSprite;
    public Sprite altSprite1;
    public Sprite altSprite2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerInteract>();
        originalSprite = objectProperties.itemSprite;
        hoverSprite = objectProperties.highlightSprite;
        spriteRenderer = thisObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = originalSprite;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = playerCam.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<SpriteInterchange>() != null)
        {
            spriteRenderer.sprite = hoverSprite;

        }
        else
        {
            spriteRenderer.sprite = originalSprite;   
        }
    }
}

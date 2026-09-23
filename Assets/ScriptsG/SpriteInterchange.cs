/*****************************************************************************
// Script Name : SpriteInterchange
// Author : Bryson Welch
// Additional Author(s) :
// Creation Date: 9/9/26
// Last Modified Date: 9/18/26
//
// Summary : Swaps the sprite to be whatever is needed
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteInterchange : MonoBehaviour
{
    [Header("Object Properties Script")]
    //[SerializeField] private ObjectProperties objectProperties;

    [Tooltip("Reference to the game object this script is on")]
    [SerializeField] private GameObject thisObject;

    private PlayerInteract player;
    private SpriteRenderer spriteRenderer;

    [Tooltip("The Main Camera in the Scene (Will have a camera/gear symbol in the hierarchy)")]
    [SerializeField] private Camera playerCam;

    private Sprite originalSprite;
    private Sprite hoverSprite;
    private Sprite altSprite1;
    private Sprite altSprite2;

    [Header("Last Object Touched Data")]
    [Tooltip("DONT TOUCH PRETTY PLEASE :)")]
    public GameObject lastObject;
    [Tooltip("DONT TOUCH PRETTY PLEASE :)")]
    public ObjectProperties lastObjectProperties;
    [Tooltip("DONT TOUCH PRETTY PLEASE :)")]
    public SpriteRenderer lastSpriteRenderer;
    [Tooltip("DONT TOUCH PRETTY PLEASE :)")]
    public Sprite lastOGSprite;
    [Tooltip("DONT TOUCH PRETTY PLEASE :)")]
    public Sprite lastHoverSprite;
    [Tooltip("DONT TOUCH PRETTY PLEASE :)")]
    public Sprite lastSpriteAlt1;
    [Tooltip("DONT TOUCH PRETTY PLEASE :)")]
    public Sprite lastSpriteAlt2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerInteract>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// SHoots a ray out and changes the last object data to be the last object touched.
    /// If the last object touched is being touched, swap sprite to the highlighted one.
    /// </summary>
    void Update()
    {
        if (!player.inPrompt)
        {
            Vector3 cursorPos = Mouse.current.position.ReadValue();
            Ray ray = playerCam.ScreenPointToRay(cursorPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<SpriteInterchange>() != null)
            {
                lastObject = hit.collider.gameObject;
                lastObjectProperties = lastObject.GetComponent<ObjectProperties>();
                lastOGSprite = lastObjectProperties.itemSprite;
                lastHoverSprite = lastObjectProperties.highlightSprite;
                altSprite1 = lastObjectProperties.altSprite1;
                altSprite2 = lastObjectProperties.altSprite2;
                lastSpriteRenderer = lastObject.GetComponent<SpriteRenderer>();
                lastSpriteRenderer.sprite = lastHoverSprite;
            }
            else if (lastObject != null)
            {
                lastSpriteRenderer.sprite = lastOGSprite;
                lastObject = null;
            }
            else
            {
            }
        }
    }
}

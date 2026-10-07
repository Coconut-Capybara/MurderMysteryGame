using UnityEngine;

public class ObjectProperties : MonoBehaviour
{
    [Tooltip("A reference to the object this script is on")]
    public GameObject thisObject;

    [Header("Object Booleans")]
    [Tooltip("Whether The Player can perform any action related to this object")]
    public bool isInteractable;
    [Tooltip("Whether an item can be put into the inventory. If true, attach the Give Player Item Script")]
    public bool isGrabbable;
    [Tooltip("Can Items be used on this object. If true, attach the Item Needed Script")]
    public bool canUseOn;
    [Tooltip("Will using an item make this object dissapear?")]
    public bool consumable;
    [Tooltip("Does interacting with this item in any way pull up a summary prompt? This is NOT the time prompt")]
    public bool givesPrompt;
    [Tooltip("Has the player lost time by failing a puzzle related to this item. DO NOT MANUALLY ASSIGN")]
    public bool timeLock;
    [Tooltip("Is this object a piece of HARD evidence?")]
    public bool isHardEvidence;

    [Header("Time Related Strings")]
    [Tooltip("How much time will interacting with this item use up? Setting to 0 means no time prompt will show.")]
    public int timeUsage;
    [Tooltip("The prompt for what happened after time was used")]
    public string prompt;
    [Tooltip("Describes what the player will do to pass time")]
    public string timeText;
    [Header("Item Description")]
    [Tooltip("The text that shows up in the bottom left when hovering over an object for long enough. " +
        "Panel will only appear if this is not null")]
    public string itemDesc;

    [Header("Item Sprites")]
    [Tooltip("The item sprite when the game starts and is not being hovered over")]
    public Sprite itemSprite;
    [Tooltip("The sprite when the cursor hovers over the object")]
    public Sprite highlightSprite;
    [Tooltip("The non-hover sprite after an event has occured")]
    public Sprite altSprite1;
    [Tooltip("The hover sprite after an event has occured")]
    public Sprite altSprite2;
    [Tooltip("Whether the event to change the sprites has occured")]
    public bool usesAltSprites;

}

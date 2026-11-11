using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenJournal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject backButton;
    private PlayerInteract player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerInteract>();
    }

    public void OpenJournalPanel()
    {
        journalPanel.SetActive(true);
        title.SetActive(true);
        backButton.SetActive(true);
        player.cursorState = PlayerInteract.CursorState.None;
        GameObject.FindFirstObjectByType<PlayerInteract>().SetInPrompt(true);
    }

    public void CloseJournal()
    {
        journalPanel.SetActive(false);
        title.SetActive(false); 
        backButton.SetActive(false);
        GameObject.FindFirstObjectByType<PlayerInteract>().SetInPrompt(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(player.cursorState != PlayerInteract.CursorState.HoldingItem && !player.GetInPrompt())
        {
            player.cursorState = PlayerInteract.CursorState.InInventory;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (player.cursorState != PlayerInteract.CursorState.HoldingItem && !player.GetInPrompt())
        {
            player.cursorState = PlayerInteract.CursorState.None;
        }
    }
}

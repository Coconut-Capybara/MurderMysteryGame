using TMPro;
using UnityEngine;

public class TextPrompts : MonoBehaviour
{
    [SerializeField] private GameObject promptPanel;
    [SerializeField] private string prompt;
    [SerializeField] private TMP_Text promptText;
    public void ShowPrompt()
    {
       promptPanel.SetActive(true);
       promptText.text = prompt;        
    }
}

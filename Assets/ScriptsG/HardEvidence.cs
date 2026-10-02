using TMPro;
using UnityEngine;

public class HardEvidence : MonoBehaviour
{
    [SerializeField] private int totalHardEvidence;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TMP_Text endText;
    public void CollectHardEvidence()
    {
        totalHardEvidence += 1;
        if(totalHardEvidence == 4)
        {
            EndGame();      
        }
    }
    public void EndGame()
    {
        endGamePanel.SetActive(true); 
        endText.text = "You collected " + totalHardEvidence + " pieces of evidence";
    }
}

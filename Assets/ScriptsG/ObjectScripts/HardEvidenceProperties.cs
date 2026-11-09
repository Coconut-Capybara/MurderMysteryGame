/*****************************************************************************
// Script Name : HardEvidenceProperties
// Author : Bryson Welch
// Additional Author(s) : 
// Creation Date: 11/4/26
// Last Modified Date: 11/4/26
//
// Summary : Handles The Hard Evidence Obtaining Sequence
*****************************************************************************/
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HardEvidenceProperties : MonoBehaviour
{
    [Tooltip("The Main Camera object")]
    [SerializeField] private GameObject player;
    [Tooltip("The PlayerInteract Script. Should be on MainCamera")]
    [SerializeField] private PlayerInteract playerInteract;
    [Tooltip("The UI Image Asset for the hard evidence Sequence")]
    private Image hardEvidenceImage;
    [Tooltip("A reference to the Hard Evidence Sprite")]
    private Sprite hardEvidencePrefab;
    [Tooltip("The Panel the hard Evidence Sequence UI is on")]
    [SerializeField] private GameObject hardEvidencePanel;
    [Tooltip("The text box for the hard evidence sequence")]
    [SerializeField] private TMP_Text hardEvidenceText;
    [Tooltip("The text that shows up describing how the evidence will help the case.")]
    private string hardEvidenceDesc;
    [Tooltip("The source the audio will be played through. Should only be on the Player")]
    public AudioSource audioSource;
    [Tooltip("The sound to play when you get the Hard Evicence")]
    public AudioClip interactSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hardEvidencePanel.SetActive(false);
    }

    public void HardEvidenceSequence(GameObject item)
    {
        playerInteract.SetInGame(false);
        playerInteract.SetInPrompt(true);
        hardEvidencePanel.SetActive(true);
        hardEvidenceDesc = item.GetComponent<ObjectProperties>().hardEvidenceDesc;
        hardEvidenceImage = item.GetComponent<ObjectProperties>().hardEvidenceImage;
        hardEvidencePrefab = item.GetComponent<ObjectProperties>().hardEvidenceSprite;
        hardEvidenceText.text = hardEvidenceDesc;
        hardEvidenceImage.sprite = hardEvidencePrefab;
        if(interactSound != null)
        {
            audioSource.clip = interactSound;
            audioSource.Play();
        }

    }

    public void CloseHardEvidenceSequence()
    {
        playerInteract.SetInGame(true);
        playerInteract.SetInPrompt(false);
        hardEvidencePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

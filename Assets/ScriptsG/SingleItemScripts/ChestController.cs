using UnityEngine;

public class ChestController : MonoBehaviour
{
    private ObjectProperties objectProperties;
    [SerializeField] private GameObject machete;
    [SerializeField] private GameObject lockViolet;
    [SerializeField] private GameObject lockOrange;
    [SerializeField] private GameObject lockYellow;
    private bool macheteCreated;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectProperties = GetComponent<ObjectProperties>();
        macheteCreated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(lockViolet.activeInHierarchy || lockOrange.activeInHierarchy || lockYellow.activeInHierarchy)
        {
            objectProperties.usesAltSprites = false;
        }
        else
        {
            objectProperties.usesAltSprites = true;
            MacheteCreate();
        }
    }

    private void MacheteCreate()
    {
        if(macheteCreated == false)
        {
            machete.SetActive(true);
            macheteCreated = true;
        }
    }
}

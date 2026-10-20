using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPlayerCursor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 cursorPos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) && !GameObject.FindFirstObjectByType<PlayerInteract>()
            .GetInPrompt())
        {
            transform.position = hit.collider.transform.position;            
        }
        else
        {
            transform.position = Vector3.zero;  
        }
    }
    public void ResetCube()
    {
        transform.position = Vector3.zero;
    }
}

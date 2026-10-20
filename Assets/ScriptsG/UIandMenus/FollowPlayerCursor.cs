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
        if(Physics.Raycast(ray, out hit))
        {
            transform.position = hit.collider.transform.position;            
        }
        else
        {
            transform.position = Vector3.zero;  
        }
    }
}

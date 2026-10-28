using UnityEngine;

public class LockerController : MonoBehaviour
{
    private MovePointData movePointData;

    public void ActivateMovePoint()
    {
        movePointData = GetComponent<MovePointData>();
        movePointData.enabled = true;
    }


}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransController : MonoBehaviour
{
    [SerializeField] private string interiorScene;
    public void MoveScene()
    {
        SceneManager.LoadScene(interiorScene);
    }
}

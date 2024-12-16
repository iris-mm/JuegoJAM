using UnityEngine;
using UnityEngine.SceneManagement;

public class Afteroutro : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("GoMenu", 65);
    }

    private void GoMenu()
    {
        SceneManager.LoadScene(0);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Afterintro : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("GoCasino", 43);
    }

    private void GoCasino()
    {
        SceneManager.LoadScene(2);
    }
}

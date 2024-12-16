using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Primero()
    {
        SceneManager.LoadScene(7);
    }
    public void play()
    {

        SceneManager.LoadScene(2);

    }

    public void credits()
    {
        SceneManager.LoadScene(1);
    }

    public void salir()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    private void OnEnable()
    {

        SceneManager.LoadScene(0);

    }
    private void play()
    {

        SceneManager.LoadScene(2);

    }

    private void credits()
    {

        SceneManager.LoadScene(1);

    }

    private void exit()
    {

        Debug.Log("Exit");
        Application.Quit();

    }

}

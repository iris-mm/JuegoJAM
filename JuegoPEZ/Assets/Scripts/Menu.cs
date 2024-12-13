using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {


    public void play()
    {

        SceneManager.LoadScene(1);

    }

    public void credits()
    {

        SceneManager.LoadScene(2);

    }
    
    public void exit()
    {

        Debug.Log("Exit");
        Application.Quit();

    }

}

using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenasJuego : MonoBehaviour 
{
 
    public void salirGamble()
    {

        SceneManager.LoadScene(2);
           
    }

     public void batllaFinal()
    {
        SceneManager.LoadScene(6);
    }

     public void outro()
    {
        SceneManager.LoadScene(8);
    }

}

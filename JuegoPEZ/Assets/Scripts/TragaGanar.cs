using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TragaperrasGanar : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private bool luz1Encendida = false; 
    private bool luz2Encendida = false;
    private bool luz3Encendida = false;
    private bool ganar = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Reinicia el Animator y fuerza una animación inicial
        animator.Play("idle", -1, 0f);
    }

    void Update()
    {
        palanca();

        
        if (luz1Encendida && !luz2Encendida && Input.GetKeyDown(KeyCode.A))
        {
            
            animator.SetBool("luz2", true);
            luz2Encendida = true; 
        }

        if (luz1Encendida && luz2Encendida && !luz3Encendida &&Input.GetKeyDown(KeyCode.B))
        {

            animator.SetBool("luz3", true);
            luz3Encendida = true;
        }

        if (luz1Encendida && luz2Encendida && luz3Encendida && !ganar && Input.GetKeyDown(KeyCode.C))
        {

            animator.SetBool("ganar", true);
            ganar = true;
            for (int i = 0; i < 10; i++)
            {

                if (i == 9)
                {
                    animator.SetInteger("vuelta", 10);
                    Wow();
                }
            }
        }
    }
    private void Wow()
    {
        Invoke("GanarJuego", 3);
    }

    private void GanarJuego()
    {
        SceneManager.LoadScene(8);
    }

    void palanca()
    {
        if (Input.GetKey("p"))
        {
            animator.SetBool("tirada", true);

            
            StartCoroutine(ActivarLuz1());
        }
    }

    IEnumerator ActivarLuz1()
    {
        // Espera 3 segundos
        yield return new WaitForSeconds(1f);

       
        animator.SetBool("luz1", true);
        luz1Encendida = true; 
    }
}

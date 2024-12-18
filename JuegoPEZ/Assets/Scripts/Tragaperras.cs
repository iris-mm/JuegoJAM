using System.Collections;
using UnityEngine;

public class Tragaperras : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Animator animator;


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
    }

    void palanca()
    {

        if (Input.GetKey("p"))
        {

            animator.SetBool("tirada", true);

            animator.SetBool("fallida", true);

            for (int i = 0; i < 10; i++)
            {

                if (i == 9)
                {
                    animator.SetInteger("vuelta", 10);
                }
            }
        }

    }
}

using System.Collections;
using UnityEngine;

public class ControlPersonaje : MonoBehaviour
{
    public float moveSpeed;
    public bool isMoving;
    public Vector2 input;
    public float collision;
    public LayerMask Solido;

    public float spriteOffsetY = 0.5f; // Desplazamiento del sprite en el eje Y
    private Transform spriteTransform; // Referencia al objeto hijo del sprite
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteTransform = transform.GetChild(0); // Asegúrate de que el sprite sea un hijo del objeto principal
    }

    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("MoveY", input.y);
                animator.SetFloat("MoveX", input.x);

                var targetPosition = transform.position;
                targetPosition.x += input.x / 2;
                targetPosition.y += input.y / 2;

                if (transitable(targetPosition))
                    StartCoroutine(Move(targetPosition));
            }
        }

        animator.SetBool("IsMoving", isMoving);

      

        // Actualiza la posición del sprite con el desplazamiento
        UpdateSpriteOffset();
    }

   

    private bool transitable(Vector3 targetPosition)
    {
        if ((Physics2D.OverlapCircle(targetPosition, collision, Solido) != null) )
        {
            return false;
        }
        return true;
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        isMoving = true;
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    void UpdateSpriteOffset()
    {
        if (spriteTransform != null)
        {
            // Aplica el desplazamiento en el eje Y
            spriteTransform.localPosition = new Vector3(0, spriteOffsetY, 0);
        }
    }
}
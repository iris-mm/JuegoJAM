using System.Collections;
using UnityEngine;

public class ControlPersonaje : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed;
    public bool isMoving;
    public Vector2 input;
    public float collision;
    public LayerMask Solido;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
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
                targetPosition.x += input.x/2;
                targetPosition.y += input.y/2;

                if (transitable(targetPosition))
                    StartCoroutine(Move(targetPosition));
            }
        }

        animator.SetBool("IsMoving", isMoving);
    }

    private bool transitable(Vector3 targetPosition){
        if(Physics2D.OverlapCircle(targetPosition, collision, Solido) != null)
        {
            return false;
        }
        return true;
    }
    IEnumerator Move(Vector3 targetPosition)
    {
        isMoving= true;
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed*Time.deltaTime);
            yield return null;
        }

        
        transform.position = targetPosition;

        isMoving= false;
    }
}

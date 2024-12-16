using UnityEngine;

public class Carta : MonoBehaviour
{

    public bool isSelected = false; // Estado de selección
    private SpriteRenderer spriteRenderer;
    private VideoPokerLogic controlador;
    public float offsetY = 0.2f; // Cuánto se mueve hacia arriba

    public float initX;
    public float initY;

    public int valor; // 1-13 (As, 2, ..., Rey)
    public int palo;  // 0 = Corazones, 1 = Diamantes, 2 = Tréboles, 3 = Picas

    private Vector3 originalPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition.x = initX;
        originalPosition.y = initY;

        transform.position = originalPosition;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void OnMouseDown()
    {
        controlador= this.transform.parent.gameObject.GetComponent<VideoPokerLogic>();
        if (controlador.iniciado) { 
        // Alterna entre seleccionada y no seleccionada
        isSelected = !isSelected;
        UpdatePosition();
    }
    }

    void UpdatePosition()
    {
        // Mueve la carta hacia arriba si está seleccionada, o a su posición original si no lo está
        Vector3 targetPosition = isSelected ? originalPosition + new Vector3(0, offsetY, 0) : originalPosition;
        StartCoroutine(SmoothMove(targetPosition));
    }
    private System.Collections.IEnumerator SmoothMove(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
            yield return null;
        }
        transform.position = targetPosition;
    }

    public void IndexToValues(int index)
    {
        palo = index / 13;  // 0-3 (Corazones, Diamantes, Tréboles, Picas)
        valor = index % 13 + 1;  // 1-13 (As, 2, ..., Rey)
    }


    public int GetSpriteIndex()
    {
        return (palo * 13) + (valor - 1); // Índice único entre 0 y 51
    }
    public void ResetSelection()
    {
        // Restaura el estado y posición de la carta
        isSelected = false;
        transform.position = originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using TMPro; // Necesario si usas TextMeshPro

public class MostrarPremio : MonoBehaviour
{
    public TextMeshProUGUI textoPremio; // Arrastra aquí el componente del texto desde el editor

    // Método para actualizar el texto en pantalla
    public void ActualizarTextoPremio(string mensaje)
    {
        textoPremio.text = mensaje;
    }

    public void ReinicioTexto()
    {
        textoPremio.text = null;
    }
}
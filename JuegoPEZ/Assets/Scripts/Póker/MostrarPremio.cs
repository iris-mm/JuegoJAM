using UnityEngine;
using TMPro; // Necesario si usas TextMeshPro

public class MostrarPremio : MonoBehaviour
{
    public TextMeshProUGUI textoPremio; // Arrastra aqu� el componente del texto desde el editor

    // M�todo para actualizar el texto en pantalla
    public void ActualizarTextoPremio(string mensaje)
    {
        textoPremio.text = mensaje;
    }

    public void ReinicioTexto()
    {
        textoPremio.text = null;
    }
}
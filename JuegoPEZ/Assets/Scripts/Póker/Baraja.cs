using UnityEngine;
using System.Collections.Generic;

public class Baraja
{
    private List<CartaLogica> cartas; // Lista de todas las cartas
    private System.Random random = new System.Random(); // Para barajar las cartas

    public Baraja()
    {
        cartas = new List<CartaLogica>();
        // Crear todas las cartas (52 en total)
        for (int palo = 0; palo < 4; palo++)
        {
            for (int valor = 1; valor <= 13; valor++)
            {
                cartas.Add(new CartaLogica(valor, palo));
            }
        }
    }

    // Baraja las cartas
    public void Barajar()
    {
        for (int i = 0; i < cartas.Count; i++)
        {
            int j = random.Next(i, cartas.Count);
            var temp = cartas[i];
            cartas[i] = cartas[j];
            cartas[j] = temp;
        }
    }

    // Roba una carta de la baraja
    public int RobarCarta()
    {
        if (cartas.Count == 0)
        {
            Debug.LogWarning("La baraja está vacía.");
            return 0;
        }
        CartaLogica carta = cartas[0];
        cartas.RemoveAt(0);
        return carta.GetSpriteIndex();
    }

    public void DevolverCarta(int index)
    {
        cartas.Add(new CartaLogica(IndexToPalo(index), IndexToValue(index)));
    }
    public int IndexToPalo(int index)
    {
        return index / 13;  // 0-3 (Corazones, Diamantes, Tréboles, Picas)
    }
    public int IndexToValue(int index)
    {
        return index % 13 + 1;  // 1-13 (As, 2, ..., Rey)
    }
}


using UnityEngine;

public class CartaLogica
{

        public int valor; // 1-13 (As, 2, ..., Rey)
        public int palo;  // 0 = Corazones, 1 = Diamantes, 2 = Tréboles, 3 = Picas

        // Constructor para inicializar la carta con valor y palo
        public CartaLogica(int valor, int palo)
        {
            this.valor = valor;
            this.palo = palo;
        }

        // Método para obtener un índice único de sprite basado en el palo y el valor
        public int GetSpriteIndex()
        {
            return (palo * 13) + (valor - 1); // Esto genera índices únicos para las 52 cartas
        }
        public void IndexToValues(int index)
        {
        palo = IndexToPalo(index);
        valor = IndexToValue(index);
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

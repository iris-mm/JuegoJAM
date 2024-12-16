using UnityEngine;
using System;
using System.Linq; // Necesario para usar LINQ

public class VideoPokerLogic : MonoBehaviour
{
    public MostrarPremio mostrarPremio;
    // Constantes para los premios
    public int ESCALERA_REAL = 9;
    public int ESCALERA_COLOR = 8;
    public int POKER = 7;
    public int FULL_HOUSE = 6;
    public int COLOR = 5;
    public int ESCALERA = 4;
    public int TRIO = 3;
    public int DOS_PAREJAS = 2;
    public int PAREJA = 1;
    public int SIN_COMBINACION = 0;

    public GameObject[] objetosCartas; // Referencia a los 5 GameObjects de las cartas
    public Sprite[] spritesCartas;     // Sprites de las 52 cartas

    private int[] manoJugador = new int[5]; // Índices de las cartas en la mano del jugador
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private SpriteRenderer spriteRenderer;
    private Baraja baraja;

    public bool iniciado = false;

    void Start()
    {

    }

    void InicioRonda()
    {


        baraja = new Baraja();
        baraja.Barajar();
        GenerarMano();
        ActualizarSprites();

    }

    public void BotonInicio()
    {
        if (!iniciado)
        {
            iniciado = true;
            InicioRonda();
        }
    }

    public void BotonDescarte()
    {
        if (iniciado)
        {
            Descartar();
        }
    }

    void Update()
    {

    }

    void Descartar()
    {
        for (int indexMano = 0; indexMano < manoJugador.Length; indexMano++)
        {
            Carta carta = objetosCartas[indexMano].GetComponent<Carta>();
            if (carta.isSelected)
            {
                baraja.DevolverCarta(carta.GetSpriteIndex());
                carta.ResetSelection();
                carta.IndexToValues(baraja.RobarCarta());
                manoJugador[indexMano] = carta.GetSpriteIndex();
            }

        }
        ActualizarSprites();
        Premio(manoJugador, mostrarPremio);
        iniciado = false;
    }

    void GenerarMano()
    {
        for (int indexMano = 0; indexMano < manoJugador.Length; indexMano++)
        {
            manoJugador[indexMano] = baraja.RobarCarta();

            Carta carta = objetosCartas[indexMano].GetComponent<Carta>();
            carta.IndexToValues(manoJugador[indexMano]);
        }
    }

    void ActualizarSprites()
    {
        for (int indexMano = 0; indexMano < manoJugador.Length; indexMano++)
        {
            Carta carta = objetosCartas[indexMano].GetComponent<Carta>();
            spriteRenderer = carta.GetComponent<SpriteRenderer>();

            int sprite = carta.GetSpriteIndex();
            if (sprite < 0) sprite = 0;
            if (sprite > 51) sprite = 51;
            spriteRenderer.sprite = spritesCartas[sprite];
        }
    }
    public CartaLogica[] CrearCartasDesdeIndices(int[] indicesCartas)
    {
        // Creamos un array de objetos CartaLogica del mismo tamaño que el array de índices
        CartaLogica[] cartasLogicas = new CartaLogica[indicesCartas.Length];

        // Recorrer el array de índices de cartas
        for (int i = 0; i < indicesCartas.Length; i++)
        {
            int index = indicesCartas[i];

            // Crear una nueva CartaLogica para cada índice
            CartaLogica carta = new CartaLogica(0, 0); // Inicializamos con valores por defecto

            // Asignamos los valores de palo y valor usando el índice
            carta.IndexToValues(index);

            // Guardamos la carta en el array
            cartasLogicas[i] = carta;
        }

        // Devolvemos el array de cartas lógicas
        return cartasLogicas;
    }

    public int Premio(int[] indexMano, MostrarPremio mostrarPremio)
    {
        Array.Sort(indexMano); // Ordenar los índices de las cartas
        CartaLogica[] mano = CrearCartasDesdeIndices(indexMano);

        int straight = 0, flush = 0;
        string descripcionPremio = "Ninguna combinación";

        // Verificar si es un color (todas las cartas tienen el mismo palo)
        if (mano.All(c => c.palo == mano[0].palo))
            flush = 1;

        // Verificar si es una escalera (cartas consecutivas)
        if (mano.Zip(mano.Skip(1), (c1, c2) => c2.valor - c1.valor).All(diff => diff == 1))
            straight = 1;
        else if (mano[0].valor == 1 && mano[1].valor == 2 && mano[2].valor == 3 && mano[3].valor == 4 && mano[4].valor == 13)
            straight = 1; // Caso especial: Escalera "Wheel"

        var contadorValores = mano.GroupBy(c => c.valor).ToDictionary(g => g.Key, g => g.Count());

        int premio;
        if (straight == 1 && flush == 1)
        {
            if (mano[0].valor == 10)
            {
                premio = 9;
                descripcionPremio = "Escalera Real";
            }
            else
            {
                premio = 8;
                descripcionPremio = "Escalera de Color";
            }
        }
        else if (flush == 1)
        {
            premio = 5;
            descripcionPremio = "Color";
        }
        else if (straight == 1)
        {
            premio = 4;
            descripcionPremio = "Escalera";
        }
        else if (contadorValores.ContainsValue(4))
        {
            premio = 7;
            descripcionPremio = "Póker";
        }
        else if (contadorValores.ContainsValue(3) && contadorValores.ContainsValue(2))
        {
            premio = 6;
            descripcionPremio = "Full House";
        }
        else if (contadorValores.ContainsValue(3))
        {
            premio = 3;
            descripcionPremio = "Trío";
        }
        else if (contadorValores.Values.Count(v => v == 2) == 2)
        {
            premio = 2;
            descripcionPremio = "Dos Parejas";
        }
        else if (contadorValores.Values.Any(v => v == 2))
        {
            premio = 1;
            descripcionPremio = "Pareja";
        }
        else
        {
            premio = 0;
            descripcionPremio = "Ninguna combinación";
        }

        // Mostrar el tipo de premio en pantalla
        if (mostrarPremio != null)
        {
            mostrarPremio.ActualizarTextoPremio($"Resultado: {descripcionPremio}");
        }

        return premio;
    }
}

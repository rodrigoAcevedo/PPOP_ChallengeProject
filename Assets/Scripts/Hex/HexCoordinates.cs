using UnityEngine;

[System.Serializable]
public struct HexCoordinates
{
    [SerializeField]
    private int x, z;
    // El Eje X contempla poder navegar en las direcciones E, W
    public int X
    {
        get
        {
            return x;
        }
    }

    // El eje Z contempla poder navegar en las direcciones NE, SW
    public int Z
    {
        get 
        {
            return z;
        }
    }

    // Usamos "cube coordinates", para contemplar todas las posibles direcciones que puede tener una navegación en grilla hexagonal.
    // De aquí que implementamos el eje Y para poder determinar luego a cuanta distancia podríamos estar al navegar en las direcciones NW, SE
    public int Y
    {
        get
        {
            return -X - Z;
        }
    }

    public HexCoordinates(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {
        // Devolvemos las coordenadas considerando que estamos en un mapa hexagonal y tenemos tres posibles direcciones (horizontal de costado, vertical a derecha y vertical a izquierda).
        // En total manejamos 6 diferentes direcciones, orientadas en puntos cardinales: NW, NE, E, SE, SW, W.
        return new HexCoordinates(x - z / 2, z);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }
}
// Enum para comtemplar los distintos caminos posibles que tenemos desde cualquier celda.
using UnityEditor.Experimental.GraphView;

public enum HexDirection
{
    NE, E, SE, SW, W, NW
}

// Clase de ayuda para extender algunos métodos útiles para direcciones
public static class HexDirectionExtensions
{
    // Obtenemos la HexCell que se encuentra en la dirección opuesta.
    public static HexDirection Opposite(this HexDirection direction)
    {
        return (int)direction < 3 ? (direction + 3) : (direction - 3);
    }
}
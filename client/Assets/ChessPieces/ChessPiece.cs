using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessPieceType 
{
    None = 0,
    King = 1,
    Queen = 2,
    Rook = 3,
    Knight = 4,
    Bishop = 5,
    Pawn = 6
}
public class ChessPiece
{
    public int team;
    public int currentX;
    public int currentY;
    public ChessPieceType type;


    public virtual List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board)
    {
        List<Vector2Int> r = new List<Vector2Int>();
        return r;
    }
}

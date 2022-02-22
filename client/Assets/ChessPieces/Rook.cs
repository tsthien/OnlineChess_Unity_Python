using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Down
        for (int i = currentX - 1; i >= 0; i--)
        {
            if (board[i, currentY] == null)
                r.Add(new Vector2Int(i, currentY));
            if (board[i, currentY] != null)
            {
                if (board[i, currentY].team != team)
                    r.Add(new Vector2Int(i, currentY));
                break;
            }
                
        }

        //Up
        for (int i = currentX + 1; i < 8; i++)
        {
            if (board[i, currentY] == null)
                r.Add(new Vector2Int(i, currentY));
            if (board[i, currentY] != null)
            {
                if (board[i, currentY].team != team)
                    r.Add(new Vector2Int(i, currentY));
                break;
            }
                
        }

        //Left
        for (int i = currentY - 1; i >= 0; i--)
        {
            if (board[currentX, i] == null)
                r.Add(new Vector2Int(currentX, i));
            if (board[currentX, i] != null)
            {
                if (board[currentX, i].team != team)
                    r.Add(new Vector2Int(currentX, i));
                break;
            }
                
        }

        //Right
        for (int i = currentY + 1; i < 8; i++)
        {
            if (board[currentX, i] == null)
                r.Add(new Vector2Int(currentX, i));
            if (board[currentX, i] != null)
            {
                if (board[currentX, i].team != team)
                    r.Add(new Vector2Int(currentX, i));
                break;
            }
                
        }
        return r;
    }
}

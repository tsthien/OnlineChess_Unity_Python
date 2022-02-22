using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //top right
        int x = currentX + 1;
        int y = currentY + 2;
        if (x < 8 && y < 8)
            if (board[x, y] == null || board[x, y].team != team)
                r.Add(new Vector2Int(x, y));
        
        x = currentX + 2;
        y = currentY + 1;
        if (x < 8 && y < 8)
            if (board[x, y] == null || board[x, y].team != team)
                r.Add(new Vector2Int(x, y));
        
        //top left
        x = currentX + 1;
        y = currentY - 2;
        if (x < 8 && y >= 0)
            if (board[x, y] == null || board[x, y].team != team)
                r.Add(new Vector2Int(x, y));
        
        x = currentX + 2;
        y = currentY - 1;
        if (x < 8 && y >= 0)
            if (board[x, y] == null || board[x, y].team != team)
                r.Add(new Vector2Int(x, y));
        
        //bottom right
        x = currentX - 1;
        y = currentY + 2;
        if (x >= 0 && y < 8)
            if (board[x, y] == null || board[x, y].team != team)
                r.Add(new Vector2Int(x, y));
        
        x = currentX - 2;
        y = currentY + 1;
        if (x >=0 && y < 8)
            if (board[x, y] == null || board[x, y].team != team)
                r.Add(new Vector2Int(x, y));
        
        //bottom left
        x = currentX - 1;
        y = currentY - 2;
        if (x >=0 && y >= 0)
            if (board[x, y] == null || board[x, y].team != team)
                r.Add(new Vector2Int(x, y));
        
        x = currentX - 2;
        y = currentY - 1;
        if (x >= 0 && y >= 0)
            if (board[x, y] == null || board[x, y].team != team)
                r.Add(new Vector2Int(x, y));
        
        
        return r;
    }
}

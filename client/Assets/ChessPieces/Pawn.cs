using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        int direction = (team == 1) ? 1 : -1;
        // one in front
        if (0 <= currentX + direction && currentX + direction <= 7 && board[currentX + direction, currentY] == null) 
            r.Add(new Vector2Int(currentX + direction, currentY));    

        // two in front
        if (0 <= currentX + direction && currentX + direction <= 7 && board[currentX + direction, currentY] == null)
            if ((team == 1 && currentX == 1) || (team == 2 && currentX == 6))
                if (board[currentX + 2 * direction, currentY] == null)
                    r.Add(new Vector2Int(currentX + 2 * direction, currentY));
            
        // kill move
        if (currentY != 7 && 0 <= currentX + direction && currentX + direction <= 7)
            if (board[currentX + direction, currentY + 1] != null && board[currentX + direction, currentY + 1].team != team)
                r.Add(new Vector2Int(currentX + direction, currentY + 1)); 
        
        if (currentY != 0 && 0 <= currentX + direction && currentX + direction <= 7)
            if (board[currentX + direction, currentY - 1] != null && board[currentX + direction, currentY - 1].team != team)
                r.Add(new Vector2Int(currentX + direction, currentY - 1)); 
        
        return r;
    }
}

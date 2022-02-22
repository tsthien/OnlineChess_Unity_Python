using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board)
    {
        List<Vector2Int> r = new List<Vector2Int>();
        int[] offsetX = new int[] {1, 1, 0, -1, -1, -1, 0, 1};
        int[] offsetY = new int[] {0, 1, 1, 1, 0, -1, -1, -1};
        for (int i = 0; i < offsetX.Length; i++)
            if (0 <= currentX + offsetX[i] && currentX + offsetX[i] < 8 && 0 <= currentY + offsetY[i] && currentY + offsetY[i] < 8)
            {
                if (board[currentX + offsetX[i], currentY + offsetY[i]] == null)
                    r.Add(new Vector2Int(currentX + offsetX[i], currentY + offsetY[i]));
                else
                {
                    if (board[currentX + offsetX[i], currentY + offsetY[i]].team != team)
                        r.Add(new Vector2Int(currentX + offsetX[i], currentY + offsetY[i]));
                }
            }
        return r;
    }
}


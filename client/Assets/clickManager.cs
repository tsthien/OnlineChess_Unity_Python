using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickManager : MonoBehaviour
{
    private GameObject square;
    [SerializeField]
    networkManager nm;
    [SerializeField]
    positionManager pm;
    public int myteam = 0;
    public int turn = 0;
    public int win = 0;
    int hold = 0; //you are dragging a piece
    int prev_i;
    int prev_j;
    private List<Vector2Int> availableMoves = new List<Vector2Int>();

    void click()
    {
        Debug.Log("clicked");

        foreach(GameObject sq in pm.listSquares)
        {
            sq.transform.GetComponent<MeshRenderer>().enabled = false;
        }

        Vector3 mousePos = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit[] objectsUnderMouse = Physics.RaycastAll(mouseRay);
        square = null;
        foreach (RaycastHit hit in objectsUnderMouse)
        {
            GameObject objectUnderMouse = hit.collider.gameObject;
            if (objectUnderMouse.CompareTag("square"))
            {
                square = objectUnderMouse;
            }
        }
        if (!square)
        {
            hold = 0;
            return;
        }

        int i = square.GetComponent<valueOfSquare>().row;
        int j = square.GetComponent<valueOfSquare>().column;

        // black turn
        if (myteam == 1)
        {
            // pick up a black piece 
            if (hold == 0 && pm.m88[i, j] != 0 && pm.m88[i, j] <= 6)
            {
                square.transform.GetComponent<MeshRenderer>().enabled = true;
                prev_i = i;
                prev_j = j;

                // get a list of where i can go to, highlight tiles as well
                availableMoves = pm.chessPieces[i, j].GetAvailableMoves(ref pm.chessPieces);
                HighlightTiles();
                hold = 1;
            }
            // move successfully
            else if (hold == 1 && ContainsValidMove(ref availableMoves, i, j) == true)
            {
                // move to a empty square
                if (pm.m88[i, j] == 0)
                {
                    int t = pm.m88[i, j];
                    pm.m88[i, j] = pm.m88[prev_i, prev_j];
                    pm.m88[prev_i, prev_j] = t;
                }
                //
                if (pm.m88[i, j] >= 7)
                {
                    pm.m88[i, j] = pm.m88[prev_i, prev_j];
                    pm.m88[prev_i, prev_j] = 0;
                }
                hold = 0;
                nm.update_board();
                pm.matrixToReal();
            }
            else
            {
                hold = 0;
            }
            
        }
        else if (myteam == 2)
        {
            if (hold == 0 && pm.m88[i, j] != 0 && pm.m88[i, j] >= 7)
            {
                square.transform.GetComponent<MeshRenderer>().enabled = true;
                prev_i = i;
                prev_j = j;
                hold = 1;
                // get a list of where i can go to, highlight tiles as well
                availableMoves = pm.chessPieces[i, j].GetAvailableMoves(ref pm.chessPieces);
                HighlightTiles();
                hold = 1;
            }
            else if (hold == 1 && ContainsValidMove(ref availableMoves, i , j) == true)
            {
                if (pm.m88[i, j] == 0)
                {
                    int t = pm.m88[i, j];
                    pm.m88[i, j] = pm.m88[prev_i, prev_j];
                    pm.m88[prev_i, prev_j] = t;
                }
                if (pm.m88[i, j] <= 6)
                {
                    pm.m88[i, j] = pm.m88[prev_i, prev_j];
                    pm.m88[prev_i, prev_j] = 0;
                }
                hold = 0;
                nm.update_board();
                pm.matrixToReal();
            }
            else
            {
                hold = 0;
            }
        }


    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && myteam == turn && turn != 0 && myteam != 0)
        {
            click();
        }
    }

    private void HighlightTiles()
    {
        for (int i = 0; i < availableMoves.Count; i++)
            pm.listSquares[availableMoves[i].x, availableMoves[i].y].transform.GetComponent<MeshRenderer>().enabled = true;
    }

    private bool ContainsValidMove(ref List<Vector2Int> moves, int x, int y)
    {
        for (int i = 0; i < moves.Count; i++)
            if (moves[i].x == x && moves[i].y == y)
                return true;
        return false;
    }
}

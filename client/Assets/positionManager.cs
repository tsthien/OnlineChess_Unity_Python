using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class positionManager : MonoBehaviour
{
    [SerializeField]
    clickManager cm;
    public GameObject board;

    public GameObject backBish;
    public GameObject backKing;
    public GameObject backKnight;
    public GameObject backPawn;
    public GameObject backQueen;
    public GameObject backRook;

    public GameObject whiteBish;
    public GameObject whiteKing;
    public GameObject whiteKnight;
    public GameObject whitePawn;
    public GameObject whiteQueen;
    public GameObject whiteRook;

    public int targetFrameRate = 60;
    List<GameObject> currentObjects = new List<GameObject>();
    public GameObject[,] listSquares = new GameObject[8, 8];
    public int[,] m88;

    public ChessPiece[,] chessPieces;

    public void matrixToReal()
    {
        foreach (GameObject obj in currentObjects)
        {
            DestroyImmediate(obj);
        }
        currentObjects.Clear();
        matrixToChessPieces();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (m88[i, j] == 1)
                {
                    GameObject newObject;
                    newObject = Instantiate(backKing, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);
                    currentObjects.Add(newObject);
                }
                if (m88[i, j] == 2)
                {
                    GameObject newObject;
                    newObject = Instantiate(backQueen, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);
                    currentObjects.Add(newObject);
                }
                if (m88[i, j] == 3)
                {
                    GameObject newObject;
                    newObject = Instantiate(backRook, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);
                    currentObjects.Add(newObject);
                }
                if (m88[i, j] == 4)
                {
                    GameObject newObject;

                    newObject = Instantiate(backKnight, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);
                    float x = listSquares[i, j].transform.rotation.x;
                    float y = -180;
                    float z = listSquares[i, j].transform.rotation.z;
                    newObject.transform.Rotate(x, y, z, Space.Self);
                    


                    currentObjects.Add(newObject);
                }
                if (m88[i, j] == 5)
                {
                    GameObject newObject;
                    newObject = Instantiate(backBish, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);
                    currentObjects.Add(newObject);
                }
                if (m88[i, j] == 6)
                {
                    GameObject newObject;
                    newObject = Instantiate(backPawn, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);
                    currentObjects.Add(newObject);
                }
                if (m88[i, j] == 7)
                {
                    GameObject newObject;
                    newObject = Instantiate(whiteKing, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);
                    currentObjects.Add(newObject);
                }
                if (m88[i, j] == 8)
                {
                    GameObject newObject;
                    newObject = Instantiate(whiteQueen, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);
                    currentObjects.Add(newObject);
                }
                if (m88[i, j] == 9)
                {
                    GameObject newObject;
                    newObject = Instantiate(whiteRook, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);
                    currentObjects.Add(newObject);
                }
                if (m88[i, j] == 10)
                {
                    GameObject newObject;
                    newObject = Instantiate(whiteKnight, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);

                    float x = listSquares[i, j].transform.rotation.x;
                    float y = -180;
                    float z = listSquares[i, j].transform.rotation.z;
                    newObject.transform.Rotate(x, y, z, Space.Self);

                    currentObjects.Add(newObject);
                }
                if (m88[i, j] == 11)
                {
                    GameObject newObject;
                    newObject = Instantiate(whiteBish, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);
                    currentObjects.Add(newObject);
                }
                if (m88[i, j] == 12)
                {
                    GameObject newObject;
                    newObject = Instantiate(whitePawn, listSquares[i, j].transform.position, listSquares[i, j].transform.rotation);
                    currentObjects.Add(newObject);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        // fps limit
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;

        //get list square 8x8
        int index = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                listSquares[i, j] = board.transform.GetChild(index).gameObject;
                index++;
            }
        }
        /*
        m88 = new int[,] { { 3, 4, 5, 1, 2, 5, 4, 3 },
                                {6, 6, 6 ,6 ,6 ,6 ,6 ,6 },
                                { 0, 0, 0, 0, 0, 0, 0, 0},
                                { 0, 0, 0, 0, 0, 0, 0, 0},
                                { 0, 0, 0, 0, 0, 0, 0, 0},
                                { 0, 0, 0, 0, 0, 0, 0, 0},
                                {12, 12, 12, 12, 12, 12, 12, 12 },
                                {9, 10, 11, 8, 7, 11, 10, 9 } };
        matrixToReal();
        cm.myteam = 1;
        cm.turn = 1;*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void matrixToChessPieces()
    {
        
        chessPieces = new ChessPiece[8, 8];
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
            {
                
                if (m88[i, j] == 0)
                {
                    chessPieces[i, j] = null;
                }
                else if (m88[i, j] <= 6)
                {
                    if (m88[i, j] == 1)
                        chessPieces[i, j] = new King();
                    else if (m88[i, j] == 2)
                        chessPieces[i, j] = new Queen();
                    else if (m88[i, j] == 3)
                        chessPieces[i, j] = new Rook();
                    else if (m88[i, j] == 4)
                        chessPieces[i, j] = new Knight();
                    else if (m88[i, j] == 5)
                        chessPieces[i, j] = new Bishop();
                    else if (m88[i, j] == 6)
                        chessPieces[i, j] = new Pawn();
                    chessPieces[i, j].team = 1;
                    chessPieces[i, j].type = (ChessPieceType)m88[i, j]; 
                    chessPieces[i, j].currentX = i;
                    chessPieces[i, j].currentY = j;
                } 
                else
                {
                     if (m88[i, j] == 7)
                        chessPieces[i, j] = new King();
                    else if (m88[i, j] == 8)
                        chessPieces[i, j] = new Queen();
                    else if (m88[i, j] == 9)
                        chessPieces[i, j] = new Rook();
                    else if (m88[i, j] == 10)
                        chessPieces[i, j] = new Knight();
                    else if (m88[i, j] == 11)
                        chessPieces[i, j] = new Bishop();
                    else if (m88[i, j] == 12)
                        chessPieces[i, j] = new Pawn();
                    chessPieces[i, j].team = 2;
                    chessPieces[i, j].type = (ChessPieceType)m88[i, j] - 6; 
                    chessPieces[i, j].currentX = i;
                    chessPieces[i, j].currentY = j;
                }
            }

    }
}

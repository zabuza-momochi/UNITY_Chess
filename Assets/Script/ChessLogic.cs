using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessLogic : MonoBehaviour
{
    // Declare list of piece and tessel
    public List<GameObject> tessels;
    public List<GameObject> pieces;
    public Material blackPiece;


    // Declare tessel dim
    public float tesselDimension;
    public float tesselHeight;

    // Declare piece height
    public float pieceHeight;

    // Declare chessboard dim
    public Vector2 boardSize;

    // Declare bool for alternate tessel color
    public bool changeColor = false;

    // Start is called before the first frame update
    void Start()
    {
        // Generate and set position of chessboard
        generateBoard();

        // Generate and set position of pieces
        generatePieces();
    }

    public void generateBoard()
    {
        for (int x = 0; x < boardSize.x; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                // Alternate tessel color
                if (y > 0)
                {
                    changeColor = !changeColor;
                }

                // Set white tessel
                if (changeColor)
                {
                    // Instantiate tessel
                    GameObject newTessel = Instantiate(tessels[0], new Vector3(y * tesselDimension, tesselHeight, x * tesselDimension), Quaternion.identity);

                    // Set parent to chessboard object
                    newTessel.transform.parent = gameObject.transform;
                }
                // Set black tessel
                else
                {
                    // Instantiate tessel
                    GameObject newTessel = Instantiate(tessels[1], new Vector3(y * tesselDimension, tesselHeight, x * tesselDimension), Quaternion.identity);

                    // Set parent to chessboard object
                    newTessel.transform.parent = gameObject.transform;
                }
            }
        }
    }

    public void generatePieces()
    {
        // Index for get piece from list
        int index;

        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                // Set pieces to first line
                if (x == 0)
                {
                    // Start from tower, end with king
                    if (y < 5)
                    {
                        index = y;
                    }
                    // Recycle last three pieces
                    else
                    {
                        index = (int)boardSize.x - 1 - y;

                    }
                }

                // Set all pawns to secondo and third line
                else
                {
                    index = 5;
                }

                // Instantiate piece
                GameObject newpiece = Instantiate(pieces[index], new Vector3(y * tesselDimension, pieceHeight, x * tesselDimension), Quaternion.identity);
                newpiece.name = pieces[index].name + "_white";

                // Set parent to chessboard object
                newpiece.transform.parent = gameObject.transform;
            }
        }

        for (int x = (int)boardSize.x - 1; x > 5; x--)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                // Set pieces to first line
                if (x == (int)boardSize.x - 1)
                {
                    // Start from tower, end with king
                    if (y < 5)
                    {
                        index = y;
                    }
                    // Recycle last three pieces
                    else
                    {
                        index = (int)boardSize.x - 1 - y;

                    }
                }

                // Set all pawns to secondo and third line
                else
                {
                    index = 5;
                }

                // Instantiate piece
                GameObject newpiece = Instantiate(pieces[index], new Vector3(y * tesselDimension, pieceHeight, x * tesselDimension), Quaternion.identity);
                newpiece.GetComponentInChildren<MeshRenderer>().material = blackPiece;
                newpiece.name = pieces[index].name + "_black";


                if (newpiece.GetComponentInChildren<MeshRenderer>().materials.Length == 2)
                {
                    newpiece.GetComponentInChildren<MeshRenderer>().materials[1].color = Color.black;
                }

                newpiece.transform.rotation = new Quaternion(0, -180, 0, 0);

                // Set parent to chessboard object
                newpiece.transform.parent = gameObject.transform;
            }
        }
    }
}

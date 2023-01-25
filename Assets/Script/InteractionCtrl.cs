using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionCtrl : Singleton<InteractionCtrl> 
{
    // Declare camera for raycast
    public Camera cam;

    // Declare hit result
    RaycastHit hit;

    // Declare ray
    Ray ray;

    // Bool for check choice
    public bool canChoice;

    GameObject selectedPiece;

    void Start()
    {
        // Start with choice
        canChoice = true;
    }

    void Update()
    {
        // Enable only on mouse click (0) is left click
        if (Input.GetMouseButtonUp(0))
        {
            // Set ray, start from camera to mouse position
            ray = cam.ScreenPointToRay(Input.mousePosition);

            // Draw ray line
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

            // Check can choice
            if (canChoice)
            {
                // Get selected piece 
                selectPiece();
            }
            else if (selectedPiece != null)
            {
                // Get selected point
                selectPoint();
            }
        }

        // Enable only on mouse click (1) is right click
        if (Input.GetMouseButtonUp(1))
        {
            if (!canChoice && selectedPiece != null)
            {
                // Get selected piece 
                deselectPiece();
            }
        }
    }

    public void selectPiece()
    {
        // Check if raycast hit only object whith "PIECE" tag 
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Piece"))
        {
            // DEBUG
            Debug.Log(hit.collider.gameObject.name);

            // Set var
            selectedPiece = hit.collider.gameObject;

            // Highlight selection
            selectedPiece.GetComponent<PieceCtrl>().HighLight = true;

            // Elevate piece
            selectedPiece.transform.position += new Vector3(0, 0.5f, 0);

            // Disable choice
            canChoice = false;
        }
    }

    public void deselectPiece()
    {
        // Disable highlight selection
        selectedPiece.GetComponent<PieceCtrl>().HighLight = false;

        // Lower piece
        selectedPiece.transform.position -= new Vector3(0, 0.5f, 0);

        // Clear
        selectedPiece = null;

        // Enable choice
        canChoice = true;
    }

    public void selectPoint()
    {
        // Check if raycast hit only object whith "TESSEL" tag 
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Tessel"))
        {
            // DEBUG
            Debug.Log(hit.collider.gameObject.name);

            // Call method, set target to selected piece
            selectedPiece.GetComponent<PieceCtrl>().setTarget(hit.transform.position);

            // Disable highlight selection
            selectedPiece.GetComponent<PieceCtrl>().HighLight = false;

            // Clear
            selectedPiece = null;
        }
    }
}

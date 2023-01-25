using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCtrl : MonoBehaviour
{
    // Declare var
    public bool isMoving;
    public Vector3 targetPos;
    public Vector3 startPos;
    public float distance;
    public float distanceFromTarget;
    public float curvePercentage;    
    public float speed;
    public float height;

    // Declare animationCurve
    public AnimationCurve curve;

    public bool HighLight { set { highLight(value); } }

    // Start is called before the first frame update
    void Start()
    {
        // Set speed
        speed = 1.5f;

        // Set height piece on chessboard
        height = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for move
        if (isMoving)
        {
            // Move piece
            movePiece();
        }
    }

    public void movePiece()
    {
        // Get current distance from target (distance left)
        distanceFromTarget = Vector3.Distance(new Vector3(targetPos.x, height, targetPos.z), new Vector3(transform.position.x, height, transform.position.z));

        // Get current frame on curve
        curvePercentage = distanceFromTarget / distance;

        // Lerp position
        Vector3 lerpedPos = Vector3.Lerp(transform.position, new Vector3(targetPos.x, height, targetPos.z), speed * Time.deltaTime);

        // Set current height given from curve
        float newYPos = curve.Evaluate(curvePercentage);

        // Check is reached max
        if (newYPos > 0.25f)
        {
            // Set new height to lerped position
            lerpedPos.y = newYPos;
        }

        // Move piece
        transform.position = lerpedPos;

        // Check end position
        if (distanceFromTarget < 0.01f)
        {
            // Disable moving
            isMoving = false;

            // Set position
            transform.position = targetPos;
            startPos = transform.position;

            // Enable new choice / next move
            InteractionCtrl.Instance.canChoice = true;
        }
    }


    public void setTarget(Vector3 pos)
    {
        // Set position
        startPos = transform.position;
        targetPos = pos;

        // Get distance from selected point
        distance = Vector3.Distance(targetPos, startPos);

        // Enable moving
        isMoving = true;
    }

    public void highLight(bool toggle)
    {
        if (toggle)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.EnableKeyword("_EMISSION");

            if (gameObject.name.Contains("black"))
            {
                gameObject.GetComponentInChildren<MeshRenderer>().material.SetVector("_EmissionColor", new Color (0.32f, 0.31f, 0));
            }
        }
        else
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
}

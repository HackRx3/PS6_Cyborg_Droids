using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class linemanager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ARPlacementInteractable placementInteractable;
    public TextMeshPro mText;
    // Start is called before the first frame update
    void Start()
    {
        placementInteractable.objectPlaced.AddListener(DrawLine);
    }
    void DrawLine(ARObjectPlacementEventArgs args)
    {  //1. increase the point count
        lineRenderer.positionCount++;
        //2. let the points location in the line renderer.
        lineRenderer.SetPosition(index: lineRenderer.positionCount - 1, args.placementObject.transform.position);
        if (lineRenderer.positionCount > 1)
        {
            Vector3 pointA = lineRenderer.GetPosition(index: lineRenderer.positionCount - 1);
            Vector3 pointB = lineRenderer.GetPosition(index: lineRenderer.positionCount - 2);
            float dist = Vector3.Distance(pointA, pointB);

            TextMeshPro distText = (TextMeshPro)Instantiate(mText);
            distText.text = "" + dist;
            Vector3 directionVector = (pointB - pointA);
            Vector3 normal = args.placementObject.transform.up;
            Vector3 upd = Vector3.Cross(lhs: directionVector, rhs: normal).normalized;
            Quaternion rotation = Quaternion.LookRotation(forward: -normal, upwards: upd);

            distText.transform.rotation = rotation;
            distText.transform.position = (pointA + directionVector * 0.5f) + upd * 0.2f;

        }
    }
}
  

                                                              


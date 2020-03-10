using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] GameObject ropePrefab; // Prefab of the rope GameObject
    [SerializeField] private List<Vector3> points = new List<Vector3>(); // List of clicked and unused point positions
    [SerializeField] private List<Rope> ropes = new List<Rope>(); // List of ready to be drawn and unused ropes
    private bool drawingRope = false; // Variable to check if a rope is currently being drawn
    [SerializeField] private GameObject levelSelectPrefab; // Level selection panel prefab

    private void Update()
    {
        CreateLine();
        DrawRope();
    }

    public void CreateLine()
    {
        // If more than points are in the list, then Rope with start and end points is added
        if (points.Count > 1)
        {
            Rope(points[0], points[1]);
        }

        // If last point was clicked, Rope is added to with the end point position to the first point
        else if (points.Count == 1 && GameManager.Instance.LastPoint())
        {
            Rope(points[0], GameManager.Instance.firstPointPos);
        }
    }

    private void DrawRope()
    {
        // If there are Ropes left and no ropes are being drawn, then draw next rope
        if (ropes.Count > 0 && !drawingRope)
        {
            StartCoroutine(RopeStrech(ropes[0]));
        }
    }

    private void Rope(Vector3 firstPos, Vector3 lastPos)
    {
        // Created a rope object under the point
        GameObject rope = Instantiate(ropePrefab, transform);
        RectTransform ropeTransform = rope.GetComponent<RectTransform>();
        ropeTransform.localPosition = firstPos;

        // Rotates rope so it faced the end point position
        ropeTransform.rotation = Quaternion.LookRotation(Vector3.forward, lastPos - ropeTransform.localPosition);

        // Adds rope to the list to be drawn
        ropes.Add(new Rope(Vector2.Distance(ropeTransform.localPosition, lastPos), ropeTransform));
        points.RemoveAt(0);
    }

    public void AddRopePoint(Vector3 pos)
    {
        points.Add(pos);
    }

    public IEnumerator RopeStrech(Rope rope)
    {
        // If no rope is bein drawn then new rope is drawn over time till end point position
        drawingRope = true;
        while (rope.ropeTransform.sizeDelta.y < rope.distance)
        {
            rope.ropeTransform.sizeDelta = new Vector2(45f, rope.ropeTransform.sizeDelta.y + 20f);
            yield return new WaitForSeconds(0.03f);
        }

        // Removes first rope from the list and sets drawingRope variable to false, so other ropes could be drawn
        ropes.RemoveAt(0);
        drawingRope = false;

        // If there are no more ropes left and points left, spawn level selection panel
        if (ropes.Count == 0 && points.Count == 0)
        {
            Instantiate(levelSelectPrefab, transform);
        }
    }
}

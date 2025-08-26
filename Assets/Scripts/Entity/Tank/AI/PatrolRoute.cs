
using System.Collections.Generic;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    [SerializeField] private bool ShowWaypoints;
    public List<Vector2> GetWaypoints()
    {
        var waypoints = new List<Vector2>();
        foreach (Transform child in transform)
        {
            waypoints.Add(child.position);
        }
        return waypoints;
    }

    // В редакторе показываем точки
    private void OnDrawGizmos()
    {
        if (!ShowWaypoints) { return; }
        Gizmos.color = Color.blue;
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.3f);
            if (i > 0)
            {
                Gizmos.DrawLine(transform.GetChild(i - 1).position,
                               transform.GetChild(i).position);
            }
        }
    }
}
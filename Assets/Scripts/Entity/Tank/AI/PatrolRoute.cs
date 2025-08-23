
using System.Collections.Generic;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    public List<Vector3> GetWaypoints()
    {
        var waypoints = new List<Vector3>();
        foreach (Transform child in transform)
        {
            waypoints.Add(child.position);
        }
        return waypoints;
    }

    // В редакторе показываем точки
    private void OnDrawGizmosSelected()
    {
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
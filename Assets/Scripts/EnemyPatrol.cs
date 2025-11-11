using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public bool pingPong = false;   // si true va y regresa; si false hace bucle
    public float arriveThreshold = 0.05f;
    public bool faceDirection = true;

    int index = 0;
    int dir = 1; // para ping-pong

    void Update()
    {
        if (waypoints == null || waypoints.Length < 2) return;

        Vector3 target = waypoints[index].position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (faceDirection)
        {
            Vector3 delta = target - transform.position;
            delta.y = 0f;
            if (delta.sqrMagnitude > 0.0001f)
                transform.rotation = Quaternion.LookRotation(delta);
        }

        if (Vector3.Distance(transform.position, target) <= arriveThreshold)
        {
            if (pingPong)
            {
                if (index == waypoints.Length - 1) dir = -1;
                else if (index == 0) dir = 1;
                index += dir;
            }
            else
            {
                index = (index + 1) % waypoints.Length;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2) return;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypoints.Length - 1; i++)
            Gizmos.DrawLine(waypoints[i].position, waypoints[i+1].position);
        if (!pingPong) Gizmos.DrawLine(waypoints[waypoints.Length-1].position, waypoints[0].position);
    }
}

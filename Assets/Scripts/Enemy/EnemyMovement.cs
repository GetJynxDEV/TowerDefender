using UnityEditor.Build.Content;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    private int currentIndex = 0;

    private void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Length) currentIndex = waypoints.Length - 1;
        }
    }

    public void StopMoving()
    {
        enabled = false;
    }

    public void ResetMovement()
    {
        currentIndex = 0;
        enabled = true;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

}
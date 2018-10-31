using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    public float speed = 10f;

    private Transform target;
    private int WavePointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (WavePointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            GameManager.instance.health--;
            return;
        }
        WavePointIndex++;
        target = Waypoints.points[WavePointIndex];
    }


}

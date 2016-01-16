using UnityEngine;
using System.Collections.Generic;

public class SpawnObstacle : MonoBehaviour {

    [SerializeField] private float spawnTimeMin;
    [SerializeField] private float spawnTimeMax;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotation;
    [SerializeField] private List<GameObject> obstacleList;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Spawn", Random.Range(spawnTimeMin, spawnTimeMax), Random.Range(spawnTimeMin, spawnTimeMax));
    }

    void Spawn()
    {
        GameObject obs = obstacleList[Random.Range(0, obstacleList.Count - 1)];
        GameObject obj = Instantiate(obs, this.transform.position, this.transform.rotation) as GameObject;

        if (obj.GetComponent<AudioSource>() != null)
        {
            obj.GetComponent<AudioSource>().enabled = false;
        }

        if (obj.GetComponent<VehicleBehavior>() != null)
        {
            obj.GetComponent<VehicleBehavior>().enabled = false;
        }

        if (obj.GetComponent<PushedObstacleBehavior>() != null)
        {
            obj.GetComponent<PushedObstacleBehavior>().enabled = true;
        }

        obj.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.up);
        obj.AddComponent<RollingObstacle>().setSpeed(Random.Range(minSpeed, maxSpeed));

        Destroy(obj, 15.0f);
    }
}

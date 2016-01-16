using UnityEngine;
using System.Collections;

public class RollingObstacle : MonoBehaviour, Obstacle {

    [SerializeField] private float speed;
    private bool collided;

	// Use this for initialization
	void Start ()
    {
        this.collided = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if (!this.hasCollided())
        {
            Vector3 forwardVector = this.transform.forward * speed * Time.deltaTime;
            this.transform.Translate(forwardVector, Space.World);
        }
	}

    public void onObstacleCollision()
    {
        
    }

    public bool hasCollided()
    {
        return this.collided;
    }

    public GameEnumerations.ObstacleType getObstacleCategory()
    {
        return GameEnumerations.ObstacleType.Obstacle_Generic;
    }
    
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}

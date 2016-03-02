using UnityEngine;
using System.Collections;

public class Manhole : MonoBehaviour {

    [SerializeField] private float frequency;
    [SerializeField] private float speed;
    [SerializeField] private Transform manhole;
    [SerializeField] private Transform destination;
    [SerializeField] private ParticleSystem particles;

    private bool active;
    private Transform originalPosition;

	void Start ()
    {
        originalPosition = this.transform;
        particles.Stop();
        StartCoroutine(activateManhole(frequency));

    }
	
	void FixedUpdate ()
    {
        if (active)
        {
            float angle = 2.0f * Mathf.Sin(2.0f * 3.0f * Time.time + Mathf.PI / 2.0f);
            manhole.rotation = Quaternion.AngleAxis(angle, manhole.right);
            manhole.position = Vector3.MoveTowards(manhole.position, this.destination.position, speed);
        } else
        {
            float angle = 2.0f * Mathf.Sin(2.0f * 8.0f * Time.time + Mathf.PI / 2.0f);
            manhole.rotation = Quaternion.AngleAxis(angle, angle * manhole.forward + manhole.right);
            manhole.position = Vector3.MoveTowards(manhole.position, this.originalPosition.position, speed);
        }
	}

    private IEnumerator activateManhole(float time)
    {
        this.active = true;
        this.tag = "Obstacle";
        if (!particles.isPlaying)
        {
            particles.Play();
        }
        yield return new WaitForSeconds(time);
        StartCoroutine(deactivateManhole(time));
    }

    private IEnumerator deactivateManhole(float time)
    {
        this.active = false;
        this.tag = "Untagged";
        if (particles.isPlaying)
        {
            particles.Stop();
        }
        yield return new WaitForSeconds(time);
        StartCoroutine(activateManhole(time));
    }
}

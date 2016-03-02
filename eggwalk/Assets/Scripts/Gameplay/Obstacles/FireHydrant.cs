using UnityEngine;
using System.Collections;

public class FireHydrant : MonoBehaviour {

    [SerializeField] private float frequency;
    [SerializeField] private ParticleSystem particles;

	// Use this for initialization
	void Start () {
        StartCoroutine(activateHydrant(frequency));
	}

    private IEnumerator activateHydrant(float time)
    {
        particles.Play();
        yield return new WaitForSeconds(time);
        StartCoroutine(deactivateHydrant(time));
    }

    private IEnumerator deactivateHydrant(float time)
    {
        particles.Stop();
        yield return new WaitForSeconds(time);
        StartCoroutine(activateHydrant(time));
    }
}

using UnityEngine;
using System.Collections;

public class PeriodicExplosion : MonoBehaviour {

    [SerializeField] private float initialTime;
    [SerializeField] private float periodicTime;
    [SerializeField] private ParticleSystem explosion;

	// Use this for initialization
	void Start ()
    {
        //StartCoroutine(WaitAndExplode());
        InvokeRepeating("Explode", initialTime, periodicTime);
	}

    public IEnumerator WaitAndExplode()
    {
        yield return new WaitForSeconds(3.0f);
        Explode();
    }

    private void Explode()
    {
        explosion.Play();
        GetComponent<Rigidbody>().AddForce(100.0f * transform.up + transform.right * 5.0f, ForceMode.Impulse);
    }
}

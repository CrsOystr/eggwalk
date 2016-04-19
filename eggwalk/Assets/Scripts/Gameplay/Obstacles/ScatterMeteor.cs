using UnityEngine;
using System.Collections.Generic;

public class ScatterMeteor : MonoBehaviour
{
    [SerializeField] private int rate;
    [SerializeField] private GameObject meteor;
    private GameObject[] area;
    [SerializeField] private Transform origin;

    // Use this for initialization
    void Start ()
    {
        area = GameObject.FindGameObjectsWithTag("Road");
        InvokeRepeating("launchMeteor", 5.0f, rate);

    }

    public void launchMeteor()
    {
        int r = Random.Range(0, area.Length - 1);
        MeshRenderer localArea = area[r].GetComponent<MeshRenderer>();

        float x = Random.Range(localArea.bounds.min.x, localArea.bounds.max.x);
        float y = Random.Range(localArea.bounds.min.y, localArea.bounds.max.y);
        float z = Random.Range(localArea.bounds.min.z, localArea.bounds.max.z);
        GameObject m = Instantiate(meteor, origin.position, Quaternion.identity) as GameObject;
        m.GetComponent<Meteor>().Target = new Vector3(x, y, z);
        m.GetComponent<Meteor>().Launch();
        Destroy(m, 40.0f);
    }
}

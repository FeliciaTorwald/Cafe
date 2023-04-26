using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    BobaTeaHandler bTH;
    public GameObject preFab;
    public GameObject spawnPos;

    private void Start()
    {
        bTH = FindObjectOfType<BobaTeaHandler>();
    }

    //void Spawn()
    //{
    //    GameObject cleanDish = Instantiate(preFab, spawnPos.transform.position, Quaternion.identity);

    //    cleanDish.GetComponent<Rigidbody>().isKinematic = false;
    //    cleanDish.transform.position += Random.insideUnitSphere + new Vector3(0, 1.2f, 0);
    //    Vector3 force = Random.insideUnitSphere * 5;
    //    force.y = Mathf.Abs(force.y);
    //    cleanDish.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DirtyBobatea")
        {
            bTH.DestroyDish();
            //Spawn();
        }
    }
}

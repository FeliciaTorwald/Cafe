using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    BobaTeaHandler bTH;
    public GameObject preFab;
    public GameObject spawnPos;
    BubbleSpawner bS;
    public ParticleSystem bubbles;
    public AudioSource source;
    public AudioClip bubbleSound;

    private void Start()
    {
        bTH = FindObjectOfType<BobaTeaHandler>();
        //bS = FindObjectOfType<BubbleSpawner>();
        bubbles.Pause();
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
            bTH.Invoke(nameof(bTH.DestroyDish),0.5f);
            //bS.StartCoroutine(bS.Spawn());
            bubbles.Play();
            source.PlayOneShot(bubbleSound);
            //Spawn();
        }
    }
}

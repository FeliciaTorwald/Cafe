using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject preFab;
    public float amountToSpawn = 10;
    public float interval = 0.4f;
    GameObject bubble;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(Spawn());
        }
    }
    public IEnumerator Spawn()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            bubble = Instantiate(preFab, transform.position, Quaternion.identity);
            bubble.transform.localScale = Vector3.one * Random.Range(1f,3f);
            Vector2 randomCircle = Random.insideUnitCircle;
            bubble.transform.position += new Vector3(randomCircle.x,0,randomCircle.y) * 7 + new Vector3(0, 4, 0);
            Vector3 force = Random.insideUnitSphere * 1;
            force.y = Mathf.Abs(force.y);
            bubble.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

            yield return new WaitForSecondsRealtime(interval);

        }
    }
}

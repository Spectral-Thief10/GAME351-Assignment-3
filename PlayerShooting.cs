using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;
    private float lastShot;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKeyDown(KeyCode.F) && Time.time - lastShot >= 1f)
        {
            GameObject clone = Instantiate(prefab);
            lastShot = Time.time;
            clone.transform.position = shootPoint.transform.position;
            clone.transform.rotation = shootPoint.transform.rotation;   

        }
    }
}
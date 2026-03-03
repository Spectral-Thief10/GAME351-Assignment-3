using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rigid;

    public float speed;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,speed * Time.deltaTime,0);
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Doc"))
        {
            Debug.Log("Doc has been shot");
            collision.gameObject.GetComponent<DocShotDead>()?.animator.SetTrigger("Shot");
             collision.gameObject.GetComponent<DocShotDead>().SetDead();
        }

        if (collision.gameObject.CompareTag("Billy"))
        {
            Debug.Log("Billy has been shot");
            collision.gameObject.GetComponent<BillyShotDead>()?.animator.SetTrigger("Shot");
            collision.gameObject.GetComponent<BillyShotDead>().SetDead();
        }

         if (collision.gameObject.CompareTag("Jessie"))
        {
            Debug.Log("Jessie has been shot");
            collision.gameObject.GetComponent<JessieShotDead>()?.animator.SetTrigger("Shot");
            collision.gameObject.GetComponent<JessieShotDead>().SetDead();
        }
         if (collision.gameObject.CompareTag("Belle"))
        {
            Debug.Log("Belle has been shot");
            collision.gameObject.GetComponent<BelleShotDead>()?.animator.SetTrigger("Shot");
            collision.gameObject.GetComponent<BelleShotDead>().SetDead();
        }
        
        Destroy(gameObject);
    }
}

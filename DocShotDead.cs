using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocShotDead : MonoBehaviour
{

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
      public AudioClip deadSound;
    private AudioSource audioSource;

    public Animator animator;
    int shotTriggerHash;

    private bool dead = false;
 
    enum CountingMethod{
        Frames,
        Invoke,
        Coroutine
    }

    private float counter = 0;
    private float timeToAct = 20f;
    [SerializeField] private CountingMethod countingMethod;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Awake()
    {
        
        shotTriggerHash = Animator.StringToHash("Shot");
    }

    // Update is called once per frame
    void Update()
    {
        if(countingMethod == CountingMethod.Frames){
            if(counter < timeToAct){
                counter+= Time.deltaTime;

            }
            else{
                if(dead == false){
                    counter = 0;
                    int index = Random.Range(0, 3);
                    if(index == 0){
                        audioSource.PlayOneShot(clip1);
                    }
                    else if(index == 1){
                        audioSource.PlayOneShot(clip2);
                    }
                    else if(index == 2){
                        audioSource.PlayOneShot(clip3);
                    }
                    }
            }
            

        }
    }

    public void SetDead(){
        dead = true;
         audioSource.PlayOneShot(deadSound);
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Bullet")){
             Debug.Log("Doc is dead!\n");
            animator.SetTrigger(shotTriggerHash);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;
    private float lastShot;

    [Header("Combat Music Settings")]
    public float combatDuration = 10f;

    private float combatTimer = 0f;
    private bool inCombat = false;

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

            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.SwitchState(MusicManager.MusicState.Fight);
            }

            // Reset combat timer
            inCombat = true;
            combatTimer = combatDuration;

        if (inCombat)
            {
                combatTimer -= Time.deltaTime;

                if (combatTimer <= 0f)
                {
                    inCombat = false;

                    if (MusicManager.Instance != null)
                    {
                        MusicManager.Instance.EndCombat();
                    }
                }
            }
        }
    }

 }

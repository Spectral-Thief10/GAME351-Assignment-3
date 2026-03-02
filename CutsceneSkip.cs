using UnityEngine;
using UnityEngine.Playables;

public class CutsceneSkip : MonoBehaviour
{
    public PlayableDirector cutscene; // Drag your Timeline here
    public GameObject player;         // Drag your Player here
    public GameObject mainCamera;     // Optional if you need control

    private bool cutsceneEnded = false;

    void Update()
    {
        if (!cutsceneEnded && Input.GetKeyDown(KeyCode.Escape))
        {
            SkipCutscene();
        }
    }

    public void SkipCutscene()
    {
        cutscene.time = cutscene.duration;
        cutscene.Evaluate();
        cutscene.Stop();
        cutsceneEnded = true;

        // Enable player control (assuming you have a PlayerController script)
        player.GetComponent<PlayerController>().enabled = true;
    }
}
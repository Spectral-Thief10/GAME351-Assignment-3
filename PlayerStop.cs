using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector director;
    public MonoBehaviour playerController;
    public Cinemachine.CinemachineVirtualCamera playerVCam;

    void Start()
    {
        playerController.enabled = false;
        playerVCam.Priority = 5;

        director.stopped += OnCutsceneEnd;
    }

    void OnCutsceneEnd(PlayableDirector pd)
    {
        playerController.enabled = true;
        playerVCam.Priority = 50;
    }
}
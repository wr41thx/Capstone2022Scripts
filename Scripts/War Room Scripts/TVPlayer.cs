using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class TVPlayer : MonoBehaviour, IInteractable
{
    private VideoPlayer videoPlayer;
    [SerializeField] private Light tvLight;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();

        // WebGL only allows url sources for Unity Video Player.
        // Hard coding for now since only 1 tv is being used in the game and this is stable. 
        // Can Easily alter script to make it more modular if needed.

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = "https://i.imgur.com/t6k2Cqv.mp4";
        videoPlayer.enabled = false;
    }

    public void ToggleTV() 
    {
        if (videoPlayer.enabled)
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            else
            {
                videoPlayer.Play();
            }
        }
        else 
        {
            
            tvLight.enabled = true;
            audioSource.Play();
            videoPlayer.enabled = true;
        }
        
    }

    public void Interact() 
    {
        ToggleTV();
    }
}

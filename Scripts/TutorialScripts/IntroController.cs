using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    // Controls the intro when the player clicks the "SEEK SHELTER IMMEDIATELY" button.

    [SerializeField] private GameObject whiteLight;
    private Light whiteDirLight;
    [SerializeField] private GameObject redLight;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float intensityRate;
    [SerializeField] private float delayTime;
    private bool startFlash;

    [SerializeField] private GameObject menuCamera;
    [SerializeField] private GameObject playerContainer;
    [SerializeField] private AudioSource broadcastAudio;
    [SerializeField] private GameObject sirenAudio;

    [SerializeField] private GameObject canvasBtn;
    [SerializeField] private GameObject canvasTitle;
    [SerializeField] private GameObject canvasTutPrompt;
    private bool showTut;

    private void Awake()
    {
        showTut = false;
        Cursor.lockState = CursorLockMode.Locked;
        delayTime = 1.0f;
        broadcastAudio.Stop();
        redLight.SetActive(false);
        whiteDirLight = whiteLight.GetComponent<Light>();
        StartCoroutine(DelayFlash(delayTime));
    }

    private void Update()
    {
        if (startFlash)
        {
            StartFlash();
        }
    }

    private IEnumerator DelayFlash(float delay)
    {
        yield return new WaitForSeconds(delay);
        startFlash = true;
    }

    private IEnumerator TutPromptDelay(float delay) 
    {
        yield return new WaitForSeconds(delay);
        canvasTutPrompt.SetActive(false);
    }

    private void StartFlash() 
    {
        intensityRate += .2f * Time.deltaTime;
        whiteDirLight.intensity = Mathf.Lerp(maxIntensity, 0, intensityRate);
        if (whiteDirLight.intensity <= 0) 
        {
            redLight.SetActive(true);
            startFlash = false;
            sirenAudio.SetActive(true);
            showTut = true;
            StartCoroutine(TutPromptDelay(6f));
            EnablePlayer();
        }
    }

    private void EnablePlayer() 
    {
        menuCamera.SetActive(false);
        canvasBtn.SetActive(false);
        canvasTitle.SetActive(false);
        Cursor.visible = false;
        playerContainer.SetActive(true);
        if (showTut)
        {
            canvasTutPrompt.SetActive(true);
        }
        else 
        {
            canvasTutPrompt.SetActive(false);
        }
        
    }

}

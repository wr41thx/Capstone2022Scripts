using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float alphaRate;
    [SerializeField] private float alpha;

    private void Awake()
    {
       
        alpha = 0;
        fadeScreen = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        alpha += alphaRate * Time.deltaTime;
        FadeToBlack();
    }

    private void FadeToBlack()
    {
        fadeScreen.color = new Color(0, 0, 0, alpha);
    }

}

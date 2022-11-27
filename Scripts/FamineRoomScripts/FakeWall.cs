using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWall : MonoBehaviour
{
    [SerializeField] private List<GlassJar> jars = new();
    [SerializeField] private AudioManagerSO audioManagerSO;

    private Animator _anim;
    private bool _hasLowered = false;

    void Awake()
    {
        _anim = gameObject.GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hasLowered)
        {
            foreach (GlassJar jar in jars)
            {
                if (!jar.Get_hasCorrectObject())
                {
                    return;
                }
            }
            // Play the animation to lower the wall
            _anim.SetTrigger("Lower");
            audioManagerSO.PlayAudio("Lower Wall", transform.position, 16f);
            _hasLowered = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassJar : MonoBehaviour
{
    [SerializeField] private AudioManagerSO audioManagerSO;
    [SerializeField] private string targetObjectTag;
    [Tooltip("First Material is the Default Material, Second Material is the Material You Want to Change to.")]
    [SerializeField] private List<Material> jarMaterials = new();

    private Renderer _materialRenderer;
    private MeshCollider _meshCollider;
    private bool _hasCorrectObject = false;

    // Start is called before the first frame update
    void Start()
    {
        _materialRenderer = gameObject.GetComponent<Renderer>();
        _meshCollider = gameObject.GetComponent < MeshCollider>();
    }

    public bool Get_hasCorrectObject()
    {
        return _hasCorrectObject;
    }

    public void Set_hasCorrectObject(bool object_in_jar)
    {
        _hasCorrectObject = object_in_jar;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetObjectTag)
        {
            _materialRenderer.material = jarMaterials[1];
            _meshCollider.convex = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == targetObjectTag)
        {
            _materialRenderer.material = jarMaterials[0];
            _meshCollider.convex = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            audioManagerSO.PlayAudio("Glass Tap", transform.position);
        }
    }
}

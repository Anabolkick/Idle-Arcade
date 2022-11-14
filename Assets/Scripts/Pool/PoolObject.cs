using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private Quaternion _startRotation;
    private Vector3 _startSize;
    
    void Awake()
    {
        transform.parent = null;
        _startSize = transform.localScale;
        _startRotation= transform.rotation;
    }
    public void ReturnToPool()
    {
        this.gameObject.SetActive(false);
    }

    public void ResetDefaultValues()
    {
        transform.parent = null;
        transform.localScale = _startSize;
        transform.rotation = _startRotation;
    }
}

using System.Collections;
using UnityEngine;

public class Replacer : MonoBehaviour
{
    public static Replacer Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public IEnumerator MoveLerp(Transform movedObject, Transform target, Vector3? addedVector = null, float duration = 0.5f)
    {
        addedVector ??= Vector3.zero;
        var startPos = movedObject.position;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            movedObject.position = Vector3.Lerp(startPos, target.position + addedVector.Value, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        movedObject.position = target.position + addedVector.Value;
        movedObject.parent = target;
    }
}

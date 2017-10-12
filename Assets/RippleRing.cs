using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleRing : MonoBehaviour {
    //these fields must be const as we save data across all instances via static
    private const int segments = 180;
    private const float startRadius = 0.01f;
    private const float endRadius = 0.3f;
    private const float timeToExpand = 1f;
    private const float startSize = 0.07f;
    private const float endSize = 0.001f;

    public static Dictionary<int, Dictionary<int, Vector3>> calcPoints;
    LineRenderer line;


    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        if (calcPoints == null)
        {
            calcPoints = new Dictionary<int, Dictionary<int, Vector3>>();
            float elapsed = 0;
            int i = 0;
            while (elapsed < timeToExpand)
            {
                elapsed += Time.fixedDeltaTime;
                float currentSize = Mathf.Lerp(startSize, endSize, (elapsed / timeToExpand));
                float currentRad = Mathf.Lerp(startRadius, endRadius, (elapsed / timeToExpand));
                line.startWidth = currentSize;
                line.endWidth = currentSize;

                if (!calcPoints.ContainsKey(i))
                {
                    calcPoints.Add(i, new Dictionary<int, Vector3>());
                }

                CreatePointsInMemory(i, currentRad);
                i++;
            }
        }

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        StartCoroutine(ExpandCircle());
    }

    private void Update()
    {

    }

    IEnumerator ExpandCircle()
    {
        float elapsed = 0;
        while (true)
        {
            int i = 0;
            while (elapsed < timeToExpand)
            {
                elapsed += Time.fixedDeltaTime;
                float currentSize = Mathf.Lerp(startSize, endSize, (elapsed / timeToExpand));
                line.startWidth = currentSize;
                line.endWidth = currentSize;
                SetPoints(i);
                i++;
                yield return new WaitForFixedUpdate();
            }
            i = 0;
            elapsed = 0;
        }

    }

    void SetPoints(int index)
    {
        for (int i = 0; i < (segments + 1); i++)
        {
            line.SetPosition(i, calcPoints[index][i]);
        }
    }

    void CreatePointsInMemory(int radIndex, float radius)
    {
        float x;
        float y;
        float z = 0f;

        float angle = 0f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle);
            y = Mathf.Cos(Mathf.Deg2Rad * angle);
            calcPoints[radIndex].Add(i, new Vector3(x, y, z) * radius);
            angle += (360f / segments);
        }
    }
}

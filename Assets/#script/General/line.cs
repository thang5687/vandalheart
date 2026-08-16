using System.Collections;
using System.Threading;
using UnityEngine;

public class line : MonoBehaviour
{
    private void Start()
    {
        GetComponent<LineRenderer>().material = WorldMap.instance.linemat;
    }
    private void OnEnable()
    {
        StartCoroutine(animated());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator animated()
    {
        int count = 0;
        while (true)
        {
            count++;
            GetComponent<LineRenderer>().material.mainTextureOffset += new Vector2(-0.01f, 0);
            yield return new WaitForEndOfFrame();
            if(count == 100)
            {
                count = 0;
                GetComponent<LineRenderer>().material.mainTextureOffset = Vector2.zero;
            }
        }
    }
}

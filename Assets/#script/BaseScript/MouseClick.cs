using System;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    private void Awake()
    {
        instance=this;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right click down!");
            if(onrightMouseClick!=null)
            onrightMouseClick.Invoke();
        }

    }
    #region MyRegion
    public static MouseClick instance;
    public Action onrightMouseClick;
    #endregion
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CameraManage : MonoBehaviour
{
    private void Awake()
    {
        Cameras.Clear();
        foreach (var Cam in GetComponentsInChildren<Camera>())
        {
            Cameras.Add(Cam);
        }
    }
    void Start()
    {
        Debug.Log("Quickkey: C+number = switch camera, C+ Arrow = move cam");
        currentCam = Cameras[0];
    }
    void Update()
    {
        Camcontrol();
    }
    #region att
    private int current_Cam;
    public int Current_Cam
    {
        get { return current_Cam; }
        set 
        { 
            current_Cam = value;
            current_Cam = Mathf.Clamp(current_Cam,0, Cameras.Count-1);
            ActiveCam(current_Cam);
        }
    }
    #endregion
    #region components
    public List<Camera> Cameras;
    public Camera currentCam;
    #endregion
    #region function
    public void Camcontrol()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (GameManager.GM.UpDown(0))
            {
                Current_Cam--;
            }
            if (GameManager.GM.DownDown(0))
            {
                Current_Cam++;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentCam.transform.position += new Vector3Int(0, 1, 0);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentCam.transform.position += new Vector3Int(0, -1, 0);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentCam.transform.position += new Vector3Int(-1, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentCam.transform.position += new Vector3Int(1, 0, 0);
            }
        }
    }
    private void ActiveCam(int cm)
    {
        foreach(Camera cam in Cameras)
        {
            cam.enabled = false;
        }
        Cameras[cm].enabled = true;
        currentCam = Cameras[cm];
    }
    #endregion
}

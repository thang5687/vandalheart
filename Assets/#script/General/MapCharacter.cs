using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCharacter : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        CharState += 0;
        animco = StartCoroutine(anim());
    }
    private void OnDisable()
    {
        StopCoroutine(animco);
        transform.Find("char").localPosition=Vector3.zero;
    }
    #region att
    public int charState;
    public int CharState
    {
        get { return charState; }
        set
        {
            charState = value;
            switch(charState)
            {
                case 0:
                    charobj.gameObject.SetActive(false);
                    break;
                case 1:
                    charobj.gameObject.SetActive(true);
                    transform.position = WorldMap.instance.Currentitem.transform.position;
                    break;
            }
        }
    }
    public int charcurrentPos;
    public int CharcurrentPos
    {
        get { return charcurrentPos; }
        set 
        {
            charcurrentPos = value; 
            //UpData(); 
        }
    }
    public Transform charobj
    {
        get { return transform.Find("char"); }
    }
    #endregion
    #region com
    private Coroutine animco;
    public static MapCharacter instance;
    #endregion
    #region func
    private IEnumerator anim()
    {
        Transform temp = transform.Find("char");
        while (true)
        {
            for (int i = 0; i < 25; i++)
            {
                temp.localPosition += new Vector3(0, 0.01f);
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 25; i++)
            {
                temp.localPosition += new Vector3(-0.01f, 0);
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 25; i++)
            {
                temp.localPosition += new Vector3(0,-0.01f);
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 25; i++)
            {
                temp.localPosition += new Vector3(0.01f,0);
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public IEnumerator MoveTo(Transform target)
    {
        if (CharState != 1)
        {
            Debug.Log("moving");
            yield break;
        }
        CharState = 2;
        while (Vector2.Distance(transform.position, target.position)>0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 7*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        transform.position = target.position;
        CharcurrentPos = WorldMap.instance.LocationList.IndexOf(target.gameObject);
        WorldMap.instance.Currentitem = target;
        Debug.Log("moved");
        CharState = 1;
    }
    private void UpData()
    {
        DatabaseManager.instance.UpdateData("Map", "id" , "1", "charPos" , CharcurrentPos.ToString());
    }
    #endregion
}

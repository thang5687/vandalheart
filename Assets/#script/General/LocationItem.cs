using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        if(onmousedownAct!=null) onmousedownAct.Invoke();
        if (WorldMapEdit.instance.Mode == 1)
        { 
            WorldMapEdit.instance.CurrentItem = this.transform;
        }
        if (WorldMapEdit.instance.Mode == 2)
        {
            if (WorldMapEdit.instance.CurrentItem.GetComponent<LocationItem>().connectList.Contains(WorldMap.instance.LocationList.IndexOf(this.gameObject)))
            {
                WorldMapEdit.instance.CurrentItem.GetComponent<LocationItem>().RemoveLineAt(transform);
            }
            else
            {
                WorldMapEdit.instance.CurrentItem.GetComponent<LocationItem>().CreateLineTo(transform);
            }
        }
    }
    #region att
    public List<int> connectList;
    public int currentWay;
    public int CurrentWay
    {
        get { return currentWay; }
        set 
        { 
            currentWay = value;
            if (currentWay < 0) currentWay = LineList.Count - 1;
            if (currentWay > LineList.Count-1) currentWay = 0;
            //currentWay = Mathf.Clamp(value, 0, LineList.Count-1);
            AlllineDeactive();
            LineList[currentWay].SetActive(true);
            if (MapCharacter.instance.CharState==1)
                MapCharacter.instance.transform.position = this.transform.position;    
            WorldMap.instance.CharacterCurrentTarget = WorldMap.instance.LocationList[connectList[currentWay]];
        }
    }
    #endregion
    #region com
    public GameObject LinePrefrag;
    public List<GameObject> LineList;
    public Action onmousedownAct;
    public Coroutine coRR;
    #endregion
    #region func
    public void CreateLineTo(Transform target)
    {
        GameObject templine = Instantiate(LinePrefrag, transform);
        LineList.Add(templine);
        templine.GetComponent<LineRenderer>().SetPosition(0, transform.position);
        templine.GetComponent<LineRenderer>().SetPosition(1, target.position);
        connectList.Add(WorldMap.instance.LocationList.IndexOf(target.gameObject));
    }
    public void RemoveLineAt(Transform target)
    {
        int index = WorldMap.instance.LocationList.IndexOf(target.gameObject);
        index = connectList.IndexOf(index);
        Destroy(LineList[index].gameObject);
        LineList.RemoveAt(index);
        connectList.RemoveAt(index);
    }
    public void AllLineactive()
    {
        coRR= StartCoroutine(highlightitem());
        foreach (GameObject line in LineList)
        {
            line.SetActive(true);
        }
    }
    public void AlllineDeactive()
    {
        try { StopCoroutine(coRR); } catch { }
        transform.localScale = Vector3.one;
        foreach (GameObject line in LineList)
        {
            line.SetActive(false);
        }
    }
    public IEnumerator highlightitem()
    {
        Vector3 inc = new Vector3(0.01f, 0.01f, 0.01f);
        while (true) 
        {
            this.transform.localScale += inc;
            if (transform.localScale.x > 2f)
                transform.localScale = Vector3.one;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
        }
    }
    public void ActiveWay(Transform target)
    {
        int index = WorldMap.instance.LocationList.IndexOf(target.gameObject);
        AlllineDeactive();
        LineList[connectList.IndexOf(index)].SetActive(true);
    }
    #endregion
}

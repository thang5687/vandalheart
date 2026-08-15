using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainVandalMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(var item in menuListtrans.GetComponentsInChildren<Button>())
        {
            items.Add(item);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(DelayEnable());

    }
    private void OnDisable()
    {
        GameManager.GM.Act_DownDown[0] = null;
        GameManager.GM.Act_UpDown[0] = null;
        GameManager.GM.Act_LeftDown[0] = null;
        GameManager.GM.Act_RightDown[0] = null;
        GameManager.GM.Act_Fire1Down[0] = null;
        GameManager.GM.Act_Fire2Down[0] = null;
        GameManager.GM.Act_Fire3Down[0] = null;
        GameManager.GM.Act_Fire4Down[0] = null;
        Debug.Log(this.name+" OnDisable");
    }
    #region att
    private int currentItem;
    public int CurrentItem
    {
        get { return currentItem; }
        set 
        {
            currentItem = value; 
            currentItem = Mathf.Clamp(currentItem, 0, items.Count-1);
            foreach(var item in items)
            {
                HightLightItem_undo(item.transform);
            }
            HightLightItem(items[currentItem].transform);
        }
    }
    #endregion
    #region com
    public Transform menuListtrans;
    public List<Button> items;
    public GameObject[] ActiveList;
    #endregion
    #region func
    public IEnumerator DelayEnable()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        GameManager.GM.Act_DownDown[0] = null;
        GameManager.GM.Act_UpDown[0] = null;
        GameManager.GM.Act_LeftDown[0] = null;
        GameManager.GM.Act_RightDown[0] = null;
        GameManager.GM.Act_Fire1Down[0] = null;
        GameManager.GM.Act_Fire2Down[0] = null;
        GameManager.GM.Act_Fire3Down[0] = null;
        GameManager.GM.Act_Fire4Down[0] = null;
        GameManager.GM.Act_DownDown[0] += () =>
        {
            CurrentItem++;
        };
        GameManager.GM.Act_UpDown[0] += () =>
        {
            CurrentItem--;
        };
        GameManager.GM.Act_LeftDown[0] += () =>
        {
            CurrentItem--;
        };
        GameManager.GM.Act_RightDown[0] += () =>
        {
            CurrentItem++;
        };
        GameManager.GM.Act_Fire1Down[0] += () =>
        {
            ActiveCurrent_item();
        };
        GameManager.GM.Act_Fire2Down[0] += () =>
        {
            ActiveCurrent_item();
        };
        GameManager.GM.Act_Fire3Down[0] += () =>
        {
            ActiveCurrent_item();
        };
        GameManager.GM.Act_Fire4Down[0] += () =>
        {
            ActiveCurrent_item();
        }; 
        yield return new WaitForEndOfFrame();
        CurrentItem += 0;
        Debug.Log(this.name + " OnEnable");
    }
    public void HightLightItem(Transform item)
    {
        item.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        item.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
    }
    public void HightLightItem_undo(Transform item)
    {
        item.localScale = Vector3.one;
        item.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(91,91,91,255);
    }
    public void ActiveCurrent_item()
    {
        switch(CurrentItem)
        {
            case 0:
                ActiveList[0].SetActive(true);
                this.gameObject.SetActive(false);
                break;
            case 1:
                UI_ver2022.instance.PressF1Onclick();
                break;
        }
    }
    #endregion
}

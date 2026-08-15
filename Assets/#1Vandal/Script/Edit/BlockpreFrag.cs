using System;
using UnityEngine;

public class BlockpreFrag : MonoBehaviour
{
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        if(OnMouseDownAction!=null)
        OnMouseDownAction.Invoke();
    }
    private void OnMouseEnter()
    {
        if (OnMouseEnterAction != null)
            OnMouseEnterAction.Invoke();
    }
    #region Att
    public int id;
    public float x,y,z;
    public int moveCost;
    public int MoveCost
    {
        get { return moveCost; }
        set 
        { 
            moveCost = value;
            UpdateCost();
        }
    }
    public int BlockState; //0==free, 1==occupy
    #endregion
    #region Com
    public Action OnMouseDownAction, OnMouseEnterAction;
    public GameObject BlueSquare;
    #endregion
    #region Func
    private void UpdateCost()
    {
        try
        {
            GetComponentInChildren<TextMesh>().text = MoveCost.ToString();
        }
        catch(Exception e){
            Debug.Log(e);
        } 
    }
    public void ActiveBlue(bool state=true)
    {
        if (state) BlockState = 0; // character can move here
        BlueSquare.SetActive(true);
        BlueSquare.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void DeActiveBlue(bool state = true)
    {
        if (state) BlockState = 1;// block is on occupy state
        BlueSquare.SetActive(false);
        BlueSquare.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void ActiveGreen()
    {
        BlueSquare.SetActive(true);
        BlueSquare.GetComponent<SpriteRenderer>().color = Color.green;
    }
    #endregion
}

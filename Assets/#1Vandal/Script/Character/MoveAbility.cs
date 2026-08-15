using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MoveAbility : MonoBehaviour
{
    void Awake()
    {
        GetComponent<CharacterContr>().AddAbility("MOVE");
    }
    private void Start()
    {
        foreach (var abi in GetComponent<CharacterContr>().AbilityList)
        {
            if (abi.GetComponentInChildren<TextMeshProUGUI>().text == "MOVE")
            {
                abi.GetComponent<Button>().onClick.RemoveAllListeners();
                abi.GetComponent<Button>().onClick.AddListener(() => 
                { 
                    ActiveMove();
                    GetComponent<CharacterContr>().State=1;
                });
            }
        }
    }
    private void OnDestroy()
    {
        GetComponent<CharacterContr>().RemoveAbility("MOVE");
    }
    #region Attributes
    public int Movepoint
    {
        get
        {
            return GetComponent<CharacterContr>().MovePow;
        }
    }
    public List<BlockpreFrag> moveRoad;
    #endregion
    #region Components
    public List<BlockpreFrag> BlockList
    {
        get { return Board.instance.BlockList; }
    }
    private GameObject CharPrefrag
    {
        get { return GetComponent<CharacterContr>().CharPrefrag; }
    }
    #endregion
    #region Functions
    public void MoveTo(BlockpreFrag target)
    {
        GetComponent<CharacterContr>().PosX = target.x;
        GetComponent<CharacterContr>().PosY = target.y;
        GetComponent<CharacterContr>().PosZ = target.z;
    }
    public void RoadTo(BlockpreFrag target)
    {
        moveRoad.Clear();
        moveRoad.Add(target);
        BlockpreFrag nextbl = target;
        for (int i = Movepoint-1; i >=0; i--)
        {
            foreach (BlockpreFrag bl in Board.instance.GetAdjBlockAt(nextbl))
            {
                if(bl.BlockState ==0 && movePossible(bl, nextbl) && bl.MoveCost< nextbl.MoveCost)
                {
                    moveRoad.Add(bl);
                    nextbl= bl;
                    break;
                }
            }
        }
        foreach (var bl in BlockList)
        {
            if (bl.BlockState == 0)
            {
                if (moveRoad.Contains(bl))
                {
                    bl.ActiveGreen();
                }
                else
                {
                    bl.ActiveBlue(false);
                }
            }     
        }
    }
    public bool movePossible(BlockpreFrag start, BlockpreFrag target)
    {
        int h = Board.instance.HeightDiffer(start, target);
        if (h == 0)
        {
            return true;
        }
        else if (h > 0) // descentPower
        {
            if (Mathf.Abs(h) <= GetComponent<CharacterContr>().DescentPower)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (h < 0) // clim
        {
            if (Mathf.Abs(h) <= GetComponent<CharacterContr>().ClimbPower)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else return false;
    }
    public void ActiveMove()
    {
        Board.instance.BlockMapAt(Mathf.RoundToInt(CharPrefrag.transform.position.x), Mathf.RoundToInt(CharPrefrag.transform.position.y), GetComponent<CharacterContr>().ClimbPower, GetComponent<CharacterContr>().DescentPower);
        foreach (var bl in BlockList)
        {
            if (bl.MoveCost > Movepoint)
            {
                bl.DeActiveBlue();
            }
            else
            {
                bl.ActiveBlue();
                bl.OnMouseEnterAction = null;
                bl.OnMouseEnterAction += () =>
                {
                    RoadTo(bl);
                };
                bl.OnMouseDownAction = null;
                bl.OnMouseDownAction += () =>
                {
                    TargetBlockToMove(bl);
                };
            }
        }
        MouseClick.instance.onrightMouseClick = null;
        MouseClick.instance.onrightMouseClick += () =>
        {
            GetComponent<CharacterContr>().State = 0;
            GetComponent<CharacterContr>().abilityCanvas.SetActive(true);
            Board.instance.ResetAllblock();
            MouseClick.instance.onrightMouseClick = null;
        };
    }
    public void TargetBlockToMove(BlockpreFrag target)
    {
        foreach (var bl in BlockList)
        {
            bl.DeActiveBlue();
            bl.OnMouseEnterAction = null;
            bl.OnMouseDownAction = null;
            if (bl == target)
            {
                bl.ActiveGreen();
                bl.OnMouseDownAction += () =>
                {
                    StartCoroutine(MoveToTargetBlockAnim(bl));
                };
            }
           
        }   
    }
    public IEnumerator MoveToTargetBlockAnim(BlockpreFrag target)
    {
        while(Vector2.Distance(GetComponent<CharacterContr>().currentPos, target.transform.position)>0.1f)   
        {
            GetComponent<CharacterContr>().currentPos = Vector2.MoveTowards(GetComponent<CharacterContr>().currentPos, target.transform.position,15f*Time.deltaTime);
            GetComponent<CharacterContr>().PosX = GetComponent<CharacterContr>().currentPos.x;
            GetComponent<CharacterContr>().PosY = GetComponent<CharacterContr>().currentPos.y;  
            yield return new WaitForEndOfFrame();
        }
        GetComponent<CharacterContr>().PosX = target.x;
        GetComponent<CharacterContr>().PosY = target.y;
        GetComponent<CharacterContr>().PosZ = target.z;
        //GetComponent<CharacterContr>().currentstandingbl = target;
        yield break;
    }
    #endregion
}

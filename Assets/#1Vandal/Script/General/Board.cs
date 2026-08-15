using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    #region attribute
    public int boardX, boardY;
    public int BoardX
    {
        get { return boardX; }
        set 
        { 
            boardX = value;
        }
    }
    public int BoardY
    {
        get { return boardY; }
        set 
        {
            boardY = value;
        }
    }
    public int[] Zlist,BlockMaterial;
    public string Zmap,MapTexture;
    #endregion
    #region components
    public static Board instance;
    public GameObject BlockPreprag;
    public List<BlockpreFrag> BlockList;
    public List<GameObject> MapObjects;
    public Transform BoardTrf;
    #endregion
    #region functions
    public IEnumerator CreateBoard(bool loadz=false)
    {
        Transform[] samplecube = BoardTrf.GetComponentsInChildren<Transform>();
        foreach(Transform t in samplecube)
        {
            if(t != BoardTrf)
            BlockList.Add(t.GetComponent<BlockpreFrag>());
        }
        foreach(BlockpreFrag cube in BlockList)
        {
            try
            {
                Destroy(cube.gameObject);
            }
            catch { }                
        }
        BlockList.Clear();
        for (int i = 0; i < boardX; i++)
        {
            for(int j = 0; j < boardY; j++)
            {
                GameObject tempBlock = Instantiate(BlockPreprag, BoardTrf);
                tempBlock.transform.position = new Vector3(i, j, 0.5f);
                BlockpreFrag tempcs = tempBlock.GetComponent<BlockpreFrag>();
                BlockList.Add(tempcs);
                tempcs.id = j+i* boardY;
                tempcs.x = i;
                tempcs.y = j;
            }
        }
        yield return new WaitForEndOfFrame();
        if (loadz)LoadBlockAttribut();
    }
    public void LoadBlockAttribut()
    {
        string[] tempZ = Zmap.Split('@');
        Zlist = new int[tempZ.Length];
        try
        {
            for (int i = 0; i < BlockList.Count; i++)
            {
                int.TryParse(tempZ[i], out Zlist[i]);
                BlockList[i].transform.localScale = new Vector3(1, 1, 1 + float.Parse(tempZ[i]) * 0.5f);
                BlockList[i].z = -Zlist[i]*0.25f;
            }
        }
        catch { }
        tempZ = MapTexture.Split('@');
        BlockMaterial = new int[tempZ.Length];
        try
        {
            for (int i = 0; i < BlockList.Count; i++)
            {
                int.TryParse(tempZ[i], out BlockMaterial[i]);
                BlockList[i].GetComponent<MeshRenderer>().material= BoardEdit.instance.mats[BlockMaterial[i]];
            }
        }
        catch { }
    }
    public void UpdateBlockAttribute()
    {
        Zlist = new int[BlockList.Count];
        Zmap = "";
        for (int i = 0; i < BlockList.Count; i++)
        {
            Zlist[i] = Mathf.RoundToInt((BlockList[i].transform.localScale.z - 1f) / 0.5f);
            //Debug.Log(Zlist[i]);
            Zmap += Zlist[i].ToString() + "@";
        }
        try { Zmap = Zmap.Substring(0, Zmap.Length - 1); }
        catch { }
        finally
        {
            DatabaseManager.instance.UpdateData("Board", "id", BoardEdit.instance.id.text, "Zmap", Zmap);
        }
        BlockMaterial = new int[BlockList.Count];
        MapTexture = "";
        for (int i = 0; i < BlockList.Count; i++)
        {
            for(int m = 0;m<BoardEdit.instance.mats.Length;m++)
            {
                if (BlockList[i].GetComponent<MeshRenderer>().materials[0].name == BoardEdit.instance.mats[m].name + " (Instance)")
                {
                    BlockMaterial[i]=m;
                    //Debug.Log(BoardEdit.instance.mats[m].name);
                    break;
                }
            }
            MapTexture += BlockMaterial[i].ToString() + "@";
           
        }
        try { MapTexture = MapTexture.Substring(0, MapTexture.Length - 1); Debug.Log(MapTexture);
        }
        catch { }
        finally
        {
            DatabaseManager.instance.UpdateData("Board", "id", BoardEdit.instance.id.text, "Texture", MapTexture);
        }
    }
    public bool CheckBlockExit(int x, int y)
    {
        foreach (var bl in BlockList)
        {
            if(Mathf.RoundToInt(bl.transform.position.x) == x && Mathf.RoundToInt(bl.transform.position.y) == y)
            {
                return true;
            }
        }
        return false;
    }
    public BlockpreFrag GetBlockat(int x, int y)
    {
        foreach(var bl in BlockList)
        {
            if (Mathf.RoundToInt(bl.transform.position.x) == x && Mathf.RoundToInt(bl.transform.position.y) == y)
            {
                return bl;
            }
        }
        return null;
    }
    public List<BlockpreFrag> GetAdjBlockAt(int x, int y)
    {
        List<BlockpreFrag> tempList = new List<BlockpreFrag>();
        if (CheckBlockExit(x + 1, y))
        {
            tempList.Add(GetBlockat(x + 1, y));
        }
        if (CheckBlockExit(x - 1, y))
        {
            tempList.Add(GetBlockat(x - 1, y));
        }
        if (CheckBlockExit(x, y + 1))
        {
            tempList.Add(GetBlockat(x, y + 1));
        }
        if (CheckBlockExit(x, y - 1))
        {
            tempList.Add(GetBlockat(x, y - 1));
        }
        return tempList;
    }
    public List<BlockpreFrag> GetAdjBlockAt(BlockpreFrag bl)
    {
        List<BlockpreFrag> tempList = new List<BlockpreFrag>();
        int x = Mathf.RoundToInt(bl.transform.position.x);
        int y = Mathf.RoundToInt(bl.transform.position.y);
        tempList = GetAdjBlockAt(x, y);
        //Debug.Log("adjblock at" +x+"/" +y +"="+tempList.Count);
        return tempList;
    }
    public List<BlockpreFrag> GetAllBlockCost(int cost)
    {
        List<BlockpreFrag> tempList = new List<BlockpreFrag>();
        foreach(var bl in BlockList)
        {
            if (bl.MoveCost == cost) { tempList.Add(bl);}
        }
        //Debug.Log("cost "+ cost + " total =" +tempList.Count);
        return tempList;
    }
    public int HeightDiffer(BlockpreFrag origin, BlockpreFrag target)
    {
        int diff =Mathf.RoundToInt((origin.transform.localScale.z - target.transform.localScale.z)/0.5f);
        //Debug.Log(diff+ "-"+ origin.id+"-"+ target.id);
        return diff;
    }
    public void BlockMapAt(int x, int y, int ClimbPower, int descentPower)
    {
        foreach (var bl in BlockList)
        {
            bl.MoveCost = -1;
        }
        if (CheckBlockExit(x, y)) 
        { 
            GetBlockat(x, y).MoveCost = 0;
        }
        bool negativeBlock = GetAllBlockCost(-1).Count > 0 ? true : false;
        int count = 0;
        //while (negativeBlock)   
        for (int i = 0; i < 99; i++)
        { 
            foreach (var bl in GetAllBlockCost(i))
            {    
                foreach (var adjbl in GetAdjBlockAt(bl))
                {
                    if (adjbl.MoveCost ==-1)
                    {
                        if (HeightDiffer(bl, adjbl) == 0)
                        {
                            adjbl.MoveCost = count + 1;
                        }
                        else if (HeightDiffer(bl, adjbl) > 0) // descentPower
                        {
                            if (Mathf.Abs(HeightDiffer(bl, adjbl)) <= descentPower)
                            {
                                adjbl.MoveCost = count+1;
                            }
                        }
                        else if (HeightDiffer(bl, adjbl) < 0) // clim
                        {
                            if (Mathf.Abs(HeightDiffer(bl, adjbl)) <= ClimbPower)
                            {
                                adjbl.MoveCost = count + 1 + Mathf.Abs(HeightDiffer(bl, adjbl));
                            }
                        }
                    }
                }
            }
            count++;
            //Debug.Log(count);
            negativeBlock = GetAllBlockCost(-1).Count > 0 ? true : false;
            if(!negativeBlock) break;
        }
    }
    public void ResetAllblock()
    {
        foreach (var bl in BlockList)
        {
            bl.DeActiveBlue();
        }
    }
    #endregion
}

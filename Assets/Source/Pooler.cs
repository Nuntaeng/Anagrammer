using UnityEngine;
using System.Collections.Generic;

public class Pooler : MonoBehaviour
{
    public class PoolObject
    {
        public class PoolData
        {
            public int x = 0;
            public int y = 0;
            public string word = "";
        }
        public Block block;
        public Stack<PoolData> log = new Stack<PoolData>();
    }

    public int poolCount = 30;
    public Transform blockStorage = null;
    public GameObject poolTarget = null;

    List<PoolObject> blocks;
    int currentBlockIdx = 0;



    void Awake()
    {
        blocks = new List<PoolObject>();
        for (int cnt = 0; cnt < poolCount; cnt++)
        {
            GameObject block = GameObject.Instantiate(poolTarget);
            block.name = cnt.ToString();
            block.transform.parent = blockStorage;
            block.transform.localScale = Vector3.one;
            block.transform.localPosition = Vector3.zero;

            PoolObject newobj = new PoolObject();
            newobj.block = block.GetComponent<Block>();
            blocks.Add(newobj);

            block.SetActive(false);
        }

        Debug.Log("[Pooler]Object " + poolTarget.name + " is now ready to use!");
    }


    // ======================================
    public Block GetBlock()
    {
        Block ret = blocks[currentBlockIdx].block;
        while(ret.gameObject.activeSelf)
        {
            if (currentBlockIdx < (poolCount - 1))
                currentBlockIdx += 1;
            else
                currentBlockIdx = 0;
            ret = blocks[currentBlockIdx].block;
        }
        ret.gameObject.SetActive(true);
        return ret;
    }

}
using UnityEngine;
using System.Collections;

public class GameBoard : MonoBehaviour
{
    class Grid
    {
        public Block block;
        public bool isExist = false;

        public Grid() { }
        public void SpawnBlock(Block block)
        {
            isExist = true;
            this.block = block;
        }

        public void DestroyBlock(bool isImmediate)
        {
            isExist = false;
            if (isImmediate)
            {
                Destroy(block.gameObject);
                block = null;
            }
            else
                block.blockAnime.SetBool("isSpawned", false);
        }

        public void Incorporation(Grid grid)
        {
            if (this.block.Incorporation(grid.block) == WordDispatch.Instance.Hurigana)
            {
                this.DestroyBlock(false);
                WordDispatch.Instance.Correct();
            }
            grid.block.blockAnime.SetTrigger("Merge");
            grid.isExist = false;
        }

        public void Move(Grid position, int x, int y)
        {
            position.block = this.block;
            position.isExist = this.isExist;
            this.block = null;
            this.isExist = false;
            position.block.SetPosition(x, y);
        }
    }

    Grid[,] gameBoard = new Grid[5, 5];
    Vector3 clickStartPos = Vector3.zero;
    Vector3 moveDirection = Vector3.zero;
    bool isProcessing = false;
    bool isPausing = false;


    public GameObject blockPrefab;

    // =====================================

    void Start()
    {
        WordDispatch.Instance.NewWord();
        for (int x = 0; x < 5; x++)
            for (int y = 0; y < 5; y++)
                gameBoard[x, y] = new Grid();
        CreateBlock();
        Logging();
    }

    void Update()
    {
        if(!isPausing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickStartPos = Input.mousePosition;
                isProcessing = false;
            }

            if (Input.GetMouseButton(0) && !isProcessing)
            {
                moveDirection = (clickStartPos - Input.mousePosition);
                Debug.Log(moveDirection.ToString());

                if (moveDirection.x >= 100f)  // LEFT
                {
                    isProcessing = true;
                    Process(Vector2.left);
                    Execute();
                }
                else if (moveDirection.x <= -100f) // RIGHT
                {
                    isProcessing = true;
                    Process(Vector2.right);
                    Execute();
                }
                else if (moveDirection.y <= -100f) // UP
                {
                    isProcessing = true;
                    Process(Vector2.up);
                    Execute();
                }
                else if (moveDirection.y >= 100f) // DOWN
                {
                    isProcessing = true;
                    Process(Vector2.down);
                    Execute();
                }
            }
        }
    }

    // =====================================

    public void PauseInput(bool chk)
    {
        isPausing = chk;
    }

    /// <summary>
    /// 블럭 생성 코드들
    /// >> CreateBlock() : 블럭 생성
    /// >> CreateBlock(int x, int y) : 지정한 위치에 블럭 생성
    /// </summary>
    #region 블럭 생성

    void CreateBlock()
    {
        int x = 0, y = 0;
        while (true)
        {
            x = Random.Range(0, 5);
            y = Random.Range(0, 5);
            if (!gameBoard[x, y].isExist)
                break;
        }
        CreateBlock(x, y);
    }

    void CreateBlock(int x, int y)
    {
        GameObject newBlock = GameObject.Instantiate(blockPrefab);
        newBlock.transform.parent = this.transform;
        newBlock.transform.localScale = Vector3.one;
        gameBoard[x, y].SpawnBlock(newBlock.GetComponent<Block>());
        gameBoard[x, y].block.SetPositionImmediately(x, y);
    }

    #endregion

    void Process(Vector2 Direction)
    {
        ScoreMng.Instance.mergeCnt = 0;
        switch (Direction.ToString())
        {
            case "(-1.0, 0.0)":          //LEFT
                #region LEFT
                for (int y = 0; y < 5; y++)
                    for (int x = 0; x <5; x++)
                        for (int jx = x; jx > 0; jx--)
                            if (gameBoard[jx, y].isExist && !gameBoard[jx, y].block.isStatic)
                            {
                                // 옆자리에 블럭 있으면 합체
                                if (gameBoard[jx - 1, y].isExist && !gameBoard[jx - 1, y].block.isStatic)
                                    gameBoard[jx, y].Incorporation(gameBoard[jx - 1, y]);
                                if (gameBoard[jx - 1, y].isExist && gameBoard[jx - 1, y].block.isStatic)
                                    continue;
                                gameBoard[jx, y].Move(gameBoard[jx - 1, y], jx - 1, y);
                            }
                Debug.Log("LEFT");
                #endregion
                break;
            case "(1.0, 0.0)":           //RIGHT
                #region RIGHT
                for (int y = 0; y < 5; y++)
                    for (int x = 4; x >= 0; x--)
                        for (int jx = x; jx < 4; jx++)
                            if(gameBoard[jx, y].isExist && !gameBoard[jx, y].block.isStatic)
                            {
                                // 옆자리에 블럭 있으면 합체
                                if (gameBoard[jx + 1, y].isExist && !gameBoard[jx + 1, y].block.isStatic)
                                    gameBoard[jx, y].Incorporation(gameBoard[jx + 1, y]);
                                if (gameBoard[jx + 1, y].isExist && gameBoard[jx + 1, y].block.isStatic)
                                    continue;
                                gameBoard[jx, y].Move(gameBoard[jx + 1, y], jx + 1, y);
                            }
                Debug.Log("RIGHT");
                #endregion
                break;
            case "(0.0, -1.0)":          //UP
                #region UP
                for (int x = 0; x < 5; x++)
                    for (int y = 4; y >= 0; y--)
                        for (int jy = y; jy < 4; jy++)
                            if (gameBoard[x, jy].isExist && !gameBoard[x, jy].block.isStatic)
                            {
                                // 옆자리에 블럭 있으면 합체
                                if (gameBoard[x, jy + 1].isExist && !gameBoard[x, jy + 1].block.isStatic)
                                    gameBoard[x, jy].Incorporation(gameBoard[x, jy + 1]);
                                if (gameBoard[x, jy + 1].isExist && gameBoard[x, jy + 1].block.isStatic)
                                    continue;
                                gameBoard[x, jy].Move(gameBoard[x, jy + 1], x, jy + 1);
                            }
                #endregion
                break;
            case "(0.0, 1.0)":           //DOWN
                #region DOWN
                for (int x = 0; x < 5; x++)
                    for (int y = 0; y < 5; y++)
                        for (int jy = y; jy > 0; jy--)
                            if (gameBoard[x, jy].isExist && !gameBoard[x, jy].block.isStatic)
                            {
                                // 옆자리에 블럭 있으면 합체
                                if (gameBoard[x, jy - 1].isExist && !gameBoard[x, jy - 1].block.isStatic)
                                    gameBoard[x, jy].Incorporation(gameBoard[x, jy - 1]);
                                if (gameBoard[x, jy - 1].isExist && gameBoard[x, jy - 1].block.isStatic)
                                    continue;
                                gameBoard[x, jy].Move(gameBoard[x, jy - 1], x, jy - 1);
                            }
                #endregion
                break;
        }
    }

    void Execute()
    {
        ObjLogging();
        Logging();
        foreach (var bblock in gameBoard)
            if (bblock.isExist)
                bblock.block.Execute();
        SEManager.Instance.PlaySE("BlockMove");
        if (ScoreMng.Instance.mergeCnt > 0)
            SEManager.Instance.PlaySE("BlockMerge");
        CreateBlock();
    }

    public void Reset()
    {
        foreach(var bblock in gameBoard)
            if (bblock.isExist)
                bblock.DestroyBlock(false);
        CreateBlock();
    }

    public void Correct()
    {
        SEManager.Instance.PlaySE("Clear");
        foreach(var bblock in gameBoard)
        {
            if (bblock.isExist)
            {
                if (bblock.block.isStatic)
                    bblock.block.ChangeMode();
                bblock.block.StaticOn();
            }
        }
    }

    #region Logger
    void Logging()
    {
        string logg = "";
        for(int x = 0; x < 5; x++)
        {
            for(int y = 0; y < 5; y++)
            {
                if (gameBoard[y, x].isExist)
                    logg += "O ";
                else
                    logg += "X ";
            }
            logg += "\n";
        }
        Debug.Log(logg);
    }

    void ObjLogging()
    {
        string logg = "";
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                try
                {
                    if (gameBoard[y, x].block != null)
                        logg += "Y ";
                    else
                        logg += "N ";
                }
                catch
                {
                    logg += "N ";
                }
            }
            logg += "\n";
        }
        Debug.Log(logg);
    }
    #endregion
}
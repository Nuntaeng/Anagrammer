using UnityEngine;
using System.Collections.Generic;

public class BlockDispatch : MonoBehaviour
{
    public GameObject blockPrefab;

    enum Direction { NULL, UP, DOWN, LEFT, RIGHT }
    Vector3 pressedPos;
    Block[,] gameBoard = new Block[5, 5];
    bool isExecuted = true;






    void Start()
    {
        SpawnBlock();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pressedPos = Input.mousePosition;
            isExecuted = false;
        }
        if (Input.GetMouseButton(0) && !isExecuted)
        {
            Vector3 dirJudge = pressedPos - Input.mousePosition;
            if (dirJudge.x > 50f) // 왼쪽
            {
                isExecuted = true;
                MoveDecision(Direction.LEFT);
            }
            if (dirJudge.x < -50f)// 오른쪽
            {
                isExecuted = true;
                MoveDecision(Direction.RIGHT);
            }  
            if (dirJudge.y < -50f) // 위쪽
            {
                isExecuted = true;
                MoveDecision(Direction.UP);
            }
            if (dirJudge.y > 50f) // 아래쪽
            {
                isExecuted = true;
                MoveDecision(Direction.DOWN);
            }
        }
    }

    void SpawnBlock()
    {
        int x = 0, y = 0;
        while(true)
        {
            try
            {
                x = Random.Range(0, 4);
                y = Random.Range(0, 4);
                if (!gameBoard[y, x].gameObject)
                {
                    Debug.Log("ddd");
                    break;
                }
            }
            catch
            {
                break;
            }
        }

        GameObject newBlock = (GameObject)Instantiate(blockPrefab);
        newBlock.transform.parent = this.transform;
        newBlock.transform.localScale = Vector3.one;
        gameBoard[y, x] = newBlock.GetComponent<Block>();
        gameBoard[y, x].SetPositionImmediately(x, y);
    }

    void MoveDecision(Direction inputDirection)
    {
        switch (inputDirection)
        {
            case Direction.UP:
                for (int x = 0; x < 5; x++)
                    for (int y = 4; y >= 0; y--)
                    {
                        try
                        {
                            if (gameBoard[y, x].gameObject != null)
                            {
                                int jy = y - 1;
                                while (jy >= 0)
                                {
                                    if (gameBoard[jy, x] != null)
                                    // 합체하고 반복 탈출
                                    {
                                        gameBoard[jy, x].PrevPosLogging(x, jy);
                                        gameBoard[jy, x].Incorporation(gameBoard[y, x]);
                                        Debug.Log("UP MERGE TO (" + x + "/" + y + ") TO (" + x + "/" + jy + ")");
                                        break;
                                    }

                                    jy -= 1;
                                }
                                if (jy <= 0)
                                // 마지막 인덱스까지 안걸렸을 때다.
                                // 맨 마지막 인덱스쪽으로 블럭을 조용히 옮겨두자.
                                {
                                    gameBoard[y, x].PrevPosLogging(x, y);
                                    gameBoard[y, x].SetPosition(x, 0);
                                    Debug.Log("UP MOVE TO (" + x + "/" +  y + ") TO (" + x + "/0)");
                                }
                            }
                        }
                        catch
                        {

                        }
                    }

                break;
            case Direction.DOWN:
                for (int x = 0; x < 5; x++)
                    for (int y = 0; y < 5; y++)
                    {
                        try
                        {
                            if (gameBoard[y, x].gameObject != null)
                            {
                                int jy = y + 1;
                                while (jy < 5)
                                {
                                    if (gameBoard[jy, x] != null)
                                    // 합체하고 반복 탈출
                                    {
                                        gameBoard[jy, x].PrevPosLogging(x, jy);
                                        gameBoard[jy, x].Incorporation(gameBoard[y, x]);
                                        break;
                                    }

                                    jy += 1;
                                }
                                if (jy >= 4)
                                {
                                    // 마지막 인덱스까지 안걸렸을 때다.
                                    // 맨 마지막 인덱스쪽으로 블럭을 조용히 옮겨두자.
                                    gameBoard[y, x].PrevPosLogging(x, y);
                                    gameBoard[y, x].SetPosition(x, 4);
                                }
                            }
                        }
                        catch
                        {
                        }
                    }

                break;
            case Direction.LEFT:
                for (int y = 0; y < 5; y++)
                    for (int x = 4; x >= 0; x--)
                    {
                        try
                        {
                            if (gameBoard[y, x].gameObject != null)
                            {
                                int jx = x - 1;
                                while (jx >= 0)
                                {
                                    if (gameBoard[y, jx] != null)
                                    // 합체하고 반복 탈출
                                    {
                                        gameBoard[y, jx].PrevPosLogging(jx, y);
                                        gameBoard[y, jx].Incorporation(gameBoard[y, x]);
                                        Debug.Log("LEFT MERGE TO (" + x + "/" + y + ") TO (" + jx + "/" + y + ")");
                                        break;
                                    }

                                    jx -= 1;
                                }
                                if (jx <= 0)
                                // 마지막 인덱스까지 안걸렸을 때다.
                                // 맨 마지막 인덱스쪽으로 블럭을 조용히 옮겨두자.
                                {
                                    gameBoard[y, x].PrevPosLogging(x, y);
                                    gameBoard[y, x].SetPosition(0, y);
                                    Debug.Log("LEFT MOVE TO (" + x + "/" + y + ") TO (" + x + "/0)");
                                }
                            }
                        }
                        catch
                        {
                        }
                    }

                break;
            case Direction.RIGHT:
                for (int y = 0; y < 5; y++)
                    for (int x = 0; x < 5; x++)
                    {
                        try
                        {
                            if (gameBoard[y, x].gameObject != null)
                            {
                                int jx = x + 1;
                                while (jx < 5)
                                {
                                    if (gameBoard[y, jx] != null)
                                    // 합체하고 반복 탈출
                                    {
                                        gameBoard[y, jx].PrevPosLogging(jx, y);
                                        gameBoard[y, jx].Incorporation(gameBoard[y, x]);
                                        break;
                                    }

                                    jx += 1;
                                }
                                if (jx >= 4)
                                {
                                    // 마지막 인덱스까지 안걸렸을 때다.
                                    // 맨 마지막 인덱스쪽으로 블럭을 조용히 옮겨두자.
                                    gameBoard[y, x].PrevPosLogging(x, y);
                                    gameBoard[y, x].SetPosition(4, y);
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                        
                break;

            default: break;
        }

        foreach (var boardBlock in gameBoard)
        {
            try{ boardBlock.Execute(); }
            catch { continue; }
        }

        SpawnBlock();
    }

    public void RestoreBlock()
    {
        foreach(var boardBlock in gameBoard)
        {
            try
            {
                boardBlock.Recover();
                boardBlock.Execute();
            }
            catch { continue; }
        }
    }
    
}
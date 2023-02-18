using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] blocks;
    public GameObject[] buffs;
    public int maxNumberOfBlocksInWave;
    public int numberOfBlocksType;
    public Transform blocksHolder;
    public int level;

    private List<GameObject> blocksList = new List<GameObject>();
    private List<GameObject> buffsList = new List<GameObject>();
    private AimControl aimControl;
    private BottomBorder bottomBorder;
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        aimControl = FindObjectOfType<AimControl>();
        bottomBorder = FindObjectOfType<BottomBorder>();
        Random.seed = System.Environment.TickCount;
        numberOfBlocksType = blocks.Length;
        StartCoroutine(EndTurn());
    }

    private IEnumerator AddWaves()
    {
        // addball buff placement
        int buffIndex = Random.Range(0, spawnPoints.Length);
        bool isBuffCreated = false;
        for (int j = 0; j < buffsList.Count; j++)
        {
            if (!buffsList[j].activeInHierarchy)
            {
                buffsList[j].transform.position = spawnPoints[buffIndex].position;
                buffsList[j].SetActive(true);
                isBuffCreated = true;
                break;
            }
        }
        if (!isBuffCreated)
        {
            GameObject go = Instantiate(buffs[0], spawnPoints[buffIndex].position, Quaternion.identity);
            go.transform.SetParent(blocksHolder);
            buffsList.Add(go);
        }
        // Blocks placement
        maxNumberOfBlocksInWave = Random.Range(0, spawnPoints.Length);
        int blockscreated = 0;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if(i==buffIndex)
                continue;
            int blocksType = Random.Range(0, numberOfBlocksType + 1);
            if (blocksType < numberOfBlocksType && blockscreated < maxNumberOfBlocksInWave)
            {
                bool isCreated = false;
                for (int j = 0; j < blocksList.Count; j++)
                {
                    if (!blocksList[j].activeInHierarchy &&
                        blocksList[j].GetComponent<Block>().blockType == blocks[blocksType].GetComponent<Block>().blockType)
                    {
                        blocksList[j].transform.position = spawnPoints[i].position;
                        blocksList[j].SetActive(true);
                        blockscreated++;
                        isCreated = true;
                        break;
                    }
                }
                if (!isCreated)
                {
                    GameObject go = Instantiate(blocks[blocksType], spawnPoints[i].position, Quaternion.identity);
                    go.transform.SetParent(blocksHolder);
                    blocksList.Add(go);
                    blockscreated++;
                }
            }
        }
        yield return null;
    }

    private IEnumerator MoveBlocks()
    {
        for (int i = 0; i < blocksList.Count; i++)
        {
            if (blocksList[i].activeInHierarchy)
                blocksList[i].GetComponent<BlockMovementControl>().Move();
        }

        for (int i = 0; i < buffsList.Count; i++)
        {
            if (buffsList[i].activeInHierarchy)
                buffsList[i].GetComponent<BlockMovementControl>().Move();
        }
        yield return null;
    }

    public IEnumerator EndTurn()
    {
        level++;
        yield return AddWaves();
        yield return MoveBlocks();
        aimControl.AimStateToAim();
        bottomBorder.startPointPosHasSet = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Shared instance of the level manager. Accesible from any script by his static nature.")]
    public static LevelManager sharedInstance;
    [Tooltip("Store every instanceable level created to the level")]
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    [Tooltip("Store only the levels instantiated in the scene")]
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();
    [Tooltip("The transform of an empty game object that is used to reference the position to start instantiation")]
    public Transform levelStartPosition;

    private void Awake()
    {
        SetSharedInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * This metod is using the allLevelBlocks and currentLevelBlocks lists to know everytime how many blocks are instantiated in the scene and chose what block to instantiate and where.
     */
    public void AddLevelBlock()
    {
        // Creating a random number between 0 and the number of elements stored in allTheLevelBlocks allow us to instantiate blocks randomly.
        int randomIdx = Random.Range(0, allTheLevelBlocks.Count);
        // Creates a LevelBlock to store the new Block instance created in the following lines.
        LevelBlock block;
        // A new vector that will be used to store the location of the levelStartPosition or the previous block end point. 
        Vector3 spawnPosition = Vector3.zero;

        // If there aren't any block instantiated yet, block will store an instance for the first block, created exclusively to start the level.
        if(currentLevelBlocks.Count == 0)
        {
            block = Instantiate(allTheLevelBlocks[0]);
            spawnPosition = levelStartPosition.position;
        }
        // Else if there are one or more blocks instantiated block will take a random instance from allTheLevelBlocks lists as value.
        else
        {
            block = Instantiate(allTheLevelBlocks[randomIdx]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].m_endPoint.position;
            
        }

        block.transform.SetParent(this.transform, false);

        /*
         * To set the exact position of the block it must be a substraction of vectors.
         * The point where we want the block to start, minus the startPoint of the block.
         */
        block.transform.position = spawnPosition - block.m_startPoint.position;
        // Finally the just added to the scene block is added either to the currentLevelBlocks list.
        currentLevelBlocks.Add(block);

    }

    public void RemoveLevelBlock()
    {
        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAllLevelBlocks()
    {
        while(currentLevelBlocks.Count > 0)
        {
            RemoveLevelBlock();
        }
    }

    /*
     * The utility of this method is to set the 2nd parameter of the for cycle to instantiate the same number of level blocks in the scene.
     */
    public void GenerateInitialBlocks()
    {
        for(int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }

    /* 
     * The sharedInstance must store this script as his value. 
     * If this variable haven't been set yet, it takes this instance of the script value.
     * This is used to have just one shredInstance storing the reference.
     */
    public void SetSharedInstance()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
}

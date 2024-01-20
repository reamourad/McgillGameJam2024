using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlocksMenu : MonoBehaviour
{
    public int row;
    public int col;
    public Vector3 originalPosition; 
    public GameObject[,] instances;
    public DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        createBlocksMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createBlocksMenu()
    {
        float index_i = 0;
        float index_j = 0;
        instances = new GameObject[row, col];
        int indexBlock = 0;
        for (int i = 0; i < row; i++)
        {
            for(int j = 0; j < col; j++)
            {
                GameObject instance = Instantiate(dataManager.blockSelectingButton);
                instance.GetComponent<Image>().sprite= dataManager.blocks[indexBlock].GetComponent<SpriteRenderer>().sprite;

                //increment block indeces
                instance.GetComponent<Blocks>().row = i;
                instance.GetComponent<Blocks>().col = j;
                instance.GetComponent<Blocks>().blockReferencing = dataManager.blocks[indexBlock];

                //add position offset
                instance.transform.SetParent(gameObject.transform, false);
                instance.transform.position = new Vector3(originalPosition.x + index_j, originalPosition.y + index_i);

                //button component
                instance.GetComponent<Button>().onClick.AddListener(onBlockClick);

                //counter to loop through blocks array
                indexBlock++;

                index_j += 1;

                instances[i, j] = instance;
            }
            index_j = 0;
            index_i -= 1;
        }
    }

    void onBlockClick()
    {
        GameObject currentInstance = EventSystem.current.currentSelectedGameObject;
        dataManager.lastClickedSprite = currentInstance.GetComponent<Image>().sprite;
        dataManager.blockSelected = currentInstance.GetComponent<Blocks>().blockReferencing;

    }
}

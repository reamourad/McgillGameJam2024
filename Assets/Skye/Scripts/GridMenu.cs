using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.Rendering.DebugUI.Table;

public class GridMenu : MonoBehaviour
{
    public int row;
    public int col;
    public Vector3 originalPosition;
    public GameObject[,] instances;
    public DataManager dataManager;
    public GameObject prefab;

    public Canvas canvas; 
    public float gridSpacing;
    // Start is called before the first frame update
    void Start()
    {
        createGridMenu();
    }

    void createGridMenu()
    {
        float index_i = 0;
        float index_j = 0;
        instances = new GameObject[row, col];
        int indexBlock = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject instance = Instantiate(prefab);
                indexBlock++;
                instance.GetComponent<Blocks>().row = i;
                instance.GetComponent<Blocks>().col = j;

                instance.transform.SetParent(gameObject.transform, false);
                instance.transform.position = new Vector3(originalPosition.x + index_j, originalPosition.y + index_i);
                instance.GetComponent<Button>().onClick.AddListener(onGridClick);

                index_j += gridSpacing;
                instances[i, j] = instance;
            }
            index_j = 0;
            index_i -= gridSpacing;
        }
    }

    public void spawnObjects()
    {
        float index_i = 0;
        float index_j = 0;
        canvas.enabled = false;
        GameObject objectParent = new GameObject();
        objectParent.gameObject.name = "Parent";

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (instances[i, j].GetComponent<Blocks>().blockReferencing == null)
                    continue;

                GameObject obj = Instantiate(instances[i, j].GetComponent<Blocks>().blockReferencing, new Vector3(originalPosition.x + index_j, originalPosition.y + index_i), Quaternion.identity);
                obj.transform.parent = objectParent.transform;
                index_j -= 1.5f;
            }
            index_j = 0;
            index_i -= 1.5f;
        }

        objectParent.AddComponent<StickComponents>();
        objectParent.GetComponent<StickComponents>().stickChildren();
    }

    void onGridClick()
    {
        //todo: check if the instance is a block 
        GameObject currentInstance = EventSystem.current.currentSelectedGameObject;

        //check height and width if it fits 
        int height = dataManager.blockSelected.GetComponent<Dimensions>().height;
        int width = dataManager.blockSelected.GetComponent<Dimensions>().width;

        int row = currentInstance.GetComponent<Blocks>().row;
        //get the row/column of the current grid object 
        //Debug.Log(currentInstance.GetComponent<Blocks>().row);
        Debug.Log(row - height + 1);
        if(row - height + 1 < 0)
        {
            Debug.Log("rip"); 
        }
        ////rotate blocks
        //if (currentInstance.GetComponent<Image>().sprite == dataManager.lastClickedSprite)
        //{
        //    currentInstance.transform.eulerAngles = new Vector3(0, 0, currentInstance.transform.eulerAngles.z - 90);
        //} else
        //{
        //    //set blocks
        //    currentInstance.GetComponent<Image>().sprite = dataManager.lastClickedSprite;
        //    currentInstance.GetComponent<Blocks>().blockReferencing = dataManager.blockSelected;
        //}


    }
}

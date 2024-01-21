using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class GridMenu : MonoBehaviour
{
    public int row;
    public int col;
    public Vector3 originalPosition;
    public GameObject[,] instances;
    public DataManager dataManager;
    public GameObject prefab;
    public MoneyManager moneyManager;

    public Canvas canvas; 
    public float gridSpacing;

    public bool isDefending;
    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        dataManager.gridMenu = this;
        originalPosition += Camera.main.transform.position;

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
        bool shouldAllowSpawn = false;

        if (isDefending)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (instances[i, j].GetComponent<Blocks>().blockReferencing != null)
                    {
                        if (instances[i, j].GetComponent<Blocks>().blockReferencing.gameObject.name.Contains("ing"))
                        {
                            shouldAllowSpawn = true;
                        }

                    }
                }
            }
        }

        if (isDefending && shouldAllowSpawn == false)
        {
            Debug.Log("PLACE A KING");
            return;
        }


        float index_i = 0;
        float index_j = 0;
        canvas.enabled = false;
        GameObject objectParent = new GameObject();
        objectParent.gameObject.name = "Parent";



        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (instances[i, j].GetComponent<Blocks>().blockReferencing != null && instances[i, j].GetComponent<Blocks>().blockReferencing.GetComponent<Dimensions>().canSpawn)
                {
                    GameObject obj = Instantiate(instances[i, j].GetComponent<Blocks>().blockReferencing, new Vector3(originalPosition.x + index_j, originalPosition.y + index_i), instances[i,j].transform.rotation);
                    obj.transform.parent = objectParent.transform;

                    int index = System.Array.IndexOf(dataManager.blocks, instances[i, j].GetComponent<Blocks>().blockReferencing);

                    obj.AddComponent<HealthComponent>();
                    obj.GetComponent<HealthComponent>().maxHealth = dataManager.blockHealth[index];
                    obj.GetComponent<HealthComponent>().value = dataManager.blockPoint[index];

                }
                index_j += 1.5f;
            }
            index_j = 0;
            index_i -= 1.5f;
        }

        objectParent.AddComponent<StickComponents>();
        objectParent.GetComponent<StickComponents>().stickChildren();
        objectParent.SetActive(false);

        if (GameObject.Find("GameManager").GetComponent<GameManager>())
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.parentListing.Add(objectParent.transform);
            gm.proceedToNextStage();
        }
        
        


    }

    void onGridClick()
    {
        //todo: check if the instance is a block 
        GameObject currentInstance = EventSystem.current.currentSelectedGameObject;

        //check height and width if it fits 
        int height = dataManager.blockSelected.GetComponent<Dimensions>().height;
        int width = dataManager.blockSelected.GetComponent<Dimensions>().width;

        int row = currentInstance.GetComponent<Blocks>().row;
        int column = currentInstance.GetComponent<Blocks>().col;


        //rotate blocks
        if (currentInstance.GetComponent<Blocks>().blockReferencing == dataManager.blockSelected)
        {
            bool shouldRot = true;
            for (int i = 0; i < dataManager.nonRotatables.Length; i ++)
            {
                if (dataManager.blockSelected == dataManager.nonRotatables[i])
                {
                    shouldRot = false;
                    break;
                }
            }

            if (shouldRot)
            {
                currentInstance.transform.eulerAngles = new Vector3(0, 0, currentInstance.transform.eulerAngles.z - 90);
            }

        }
        else
        {

          
            //get the row/column of the current grid object 
            if (row - height + 1 >= 0 && column - width + 1 >= 0)
            {
                int oldCost = 0;
                if (currentInstance.GetComponent<Blocks>().blockReferencing != null)
                {
                    oldCost = currentInstance.GetComponent<Blocks>().blockReferencing.GetComponent<Dimensions>().cost;
                }
                int price = moneyManager.currentMoney + oldCost - dataManager.blockSelected.GetComponent<Dimensions>().cost;
                Debug.Log(price);
                if (price > 0)
                {
                    moneyManager.updateMoney(price); 
                }
                else
                {
                    return;
                }

                //only for 2 height
                if (currentInstance.GetComponent<Blocks>().blockReferencing != null)
                {
                    Debug.Log("Not null");
                    if (currentInstance.GetComponent<Blocks>().blockReferencing.GetComponent<Dimensions>().arrayOfSprites.Length + 1 > 1)
                    {
                        if (currentInstance.GetComponent<Image>().sprite == currentInstance.GetComponent<Blocks>().blockReferencing.GetComponent<Dimensions>().arrayOfSprites[0])
                        {
                            Debug.Log("Bottom"); 
                            int currentheight = currentInstance.GetComponent<Blocks>().blockReferencing.GetComponent<Dimensions>().height; 
                            for (int i = 0; i < currentheight; i++)
                            {
                                GameObject instance = dataManager.gridMenu.instances[row - i, column];
                                instance.GetComponent<Image>().sprite = prefab.GetComponent<Image>().sprite;
                                instance.GetComponent<Blocks>().blockReferencing = prefab;
                            }
                        }
                        else if(currentInstance.GetComponent<Image>().sprite == currentInstance.GetComponent<Blocks>().blockReferencing.GetComponent<Dimensions>().arrayOfSprites[1])
                        {
                            int currentheight = currentInstance.GetComponent<Blocks>().blockReferencing.GetComponent<Dimensions>().height;
                            for (int i = 0; i < currentheight; i++)
                            {
                                Debug.Log("Top");
                                GameObject instance = dataManager.gridMenu.instances[row + i, column];
                                instance.GetComponent<Image>().sprite = prefab.GetComponent<Image>().sprite;
                                instance.GetComponent<Blocks>().blockReferencing = prefab;
                            }
                        }
                    }
                }

                currentInstance.GetComponent<Blocks>().blockReferencing = dataManager.blockSelected;

                currentInstance.GetComponent<Blocks>().blockReferencing.GetComponent<Dimensions>().canSpawn = true;

                //set blocks based on their height 
                for (int i = 0; i < height; i++)
                {
                    GameObject instance = dataManager.gridMenu.instances[row - i, column];
                    instance.GetComponent<Image>().sprite = dataManager.lastClickedSprites[i];
                    instance.GetComponent<Blocks>().blockReferencing = dataManager.blockSelected;



                }
                for (int i = 0; i < width; i++)
                {
                    GameObject instance = dataManager.gridMenu.instances[row, column + i];
                    instance.GetComponent<Image>().sprite = dataManager.lastClickedSprites[i];
                    instance.GetComponent<Blocks>().blockReferencing = dataManager.blockSelected;
                }

            }
        }
        
    }
}

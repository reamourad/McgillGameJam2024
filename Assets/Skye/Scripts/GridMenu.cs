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
    // Start is called before the first frame update
    void Start()
    {
        createGridMenu();
    }

    // Update is called once per frame
    void Update()
    {
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

                index_j += 0.3f;
            }
            index_j = 0;
            index_i -= 0.3f;
        }
    }

    void onGridClick()

    {
        GameObject currentInstance = EventSystem.current.currentSelectedGameObject;
        currentInstance.GetComponent<Image>().sprite = dataManager.lastClickedSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public GameObject blockSelectingButton;
    public GameObject[] blocks;

    public GameObject [] whiteAttacking;
    public GameObject[] blackAttacking;
    public GameObject[] whiteDefending;
    public GameObject[] blackDefending;

    public float [] blockHealth;
    public float[] blockPoint;

    public int initialMoney; 
    public Sprite[] lastClickedSprites;
    public GameObject blockSelected;
    public GameObject[] nonRotatables;
    public GridMenu gridMenu; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

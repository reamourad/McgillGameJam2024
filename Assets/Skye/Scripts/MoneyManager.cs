using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public DataManager dataManager;
    public int currentMoney;
    public TMP_Text tmp;
    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        tmp.text = dataManager.initialMoney.ToString();
        Debug.Log(dataManager.initialMoney.ToString());
    }

    public void updateMoney(int price)
    {
        currentMoney -= price;
        tmp.text = currentMoney.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

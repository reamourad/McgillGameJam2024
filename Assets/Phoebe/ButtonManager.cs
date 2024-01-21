using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonManager : MonoBehaviour
{
   public static ButtonManager Instance;

   private void Awake()
   {
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    else
    {
        Destroy(gameObject);
    }
   }
   
}

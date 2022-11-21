using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("•”‰®‚Ì”Ô†")] int _loomNum;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
}

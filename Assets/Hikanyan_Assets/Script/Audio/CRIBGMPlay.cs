using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRIBGMPlay : MonoBehaviour
{
    [SerializeField] int _bgmNumber;

    private void Start()
    {
        CRIAudioManager.Instance.CRIPlayBGM(_bgmNumber);
    }
}

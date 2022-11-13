using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;
using UnityEditor;
using System;
public class CRIAudioManager : MonoBehaviour
{
    public static CRIAudioManager instance;
    [SerializeField] string _streamingAssetsPathACF;//.acf
    [SerializeField] string _cueSheetBGM;//.acb
    [SerializeField] string _cueSheetSE;//,acb

    [SerializeField, Range(0f, 1f)] float _bgmPlayVolume = default;
    [SerializeField, Range(0f, 1f)] float _sePlayVolume = default;
    
    CriAtomExPlayback _criAtomExPlayback;
    CriAtomEx.CueInfo _cueInfo;

    CriAtomSource _criAtomSourceBgm;
    CriAtomSource _criAtomSourceSe;

    int _cueIndexID;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //acf
            string path = Common.streamingAssetsPath + $"/{_streamingAssetsPathACF}.acf";
            CriAtomEx.RegisterAcf(null,path);

            //CriAtom
            new GameObject().AddComponent<CriAtom>();

            //BGM.acb
            CriAtom.AddCueSheet(_cueSheetBGM, $"{_cueSheetBGM}.acb", null, null);
            //SE.acb
            CriAtom.AddCueSheet(_cueSheetSE, $"{_cueSheetSE}.acb", null, null);

            //CriAtomSourceBGM
            _criAtomSourceBgm = gameObject.AddComponent<CriAtomSource>();
            _criAtomSourceBgm.cueSheet = _cueSheetBGM;
            //CriAtomSourceSE
            _criAtomSourceSe = gameObject.AddComponent<CriAtomSource>();
            _criAtomSourceSe.cueSheet = _cueSheetSE;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //ゲーム内プレビュー用のレベルモニター機能を追加
        CriAtom.SetBusAnalyzer(true);
        //PlayerSettings setting = SaveLoad.LoadSettings<PlayerSettings>();
        //_bgmPlayVolume = setting.BGMVolume;
        //_sePlayVolume = setting.SEVolume;
    }
}

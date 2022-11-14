using System.Collections;
using UnityEngine;
using CriWare;
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
        //音量設定のロード
        //PlayerSettings setting = SaveLoad.LoadSettings<PlayerSettings>();
        //_bgmPlayVolume = setting.BGMVolume;
        //_sePlayVolume = setting.SEVolume;
    }

    public void CRIPlayBGM(int index)
    {
        bool startFlag = false;
        CriAtomSource.Status status = _criAtomSourceBgm.status;
        if ((status == CriAtomSource.Status.Stop) || (status == CriAtomSource.Status.PlayEnd))
        {
            this._criAtomExPlayback = _criAtomSourceBgm.Play(index);
            startFlag = true;
        }
        if (startFlag == false)
        {
            int cur = this._criAtomExPlayback.GetCurrentBlockIndex();
            CriAtomExAcb acb = CriAtom.GetAcb("_cueSheetBGM");
            if (acb != null)
            {
                acb.GetCueInfo(index, out _cueInfo);

                cur++;
                if (_cueInfo.numBlocks > 0)
                {
                    _criAtomExPlayback.SetNextBlockIndex(cur % _cueInfo.numBlocks);
                }
            }
        }
    }
    public void CRIPlayBGM(int index, float delayTime)
    {
        StartCoroutine(CRIDelayPlaySound(index, delayTime));
    }
    private IEnumerator CRIDelayPlaySound(int index, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        CRIPlayBGM(index);
    }

    public void CRIPauseAudio(bool isPause)
    {
        _criAtomSourceBgm.Pause(isPause);
    }
    public void CRIPlaySE(int index)
    {
        _criAtomSourceSe.Play(index);
    }
}

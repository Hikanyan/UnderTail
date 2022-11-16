using System.Collections;
using UnityEngine;
using CriWare;
using System;
public class CRIAudioManager : MonoBehaviour
{
    public static CRIAudioManager instance;
    [SerializeField] string _streamingAssetsPathACF;//.acf
    [SerializeField] string _cueSheetAll;//.acb

    [SerializeField, Range(0f, 1f)] float _allPlayVolume = default;

    CriAtomExPlayback _criAtomExPlayback;
    CriAtomEx.CueInfo _cueInfo;

    CriAtomSource _criAtomSourceAll;

    int _cueIndexID;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //acf
            string path = Common.streamingAssetsPath + $"/{_streamingAssetsPathACF}.acf";
            CriAtomEx.RegisterAcf(null, path);

            //CriAtom
            new GameObject().AddComponent<CriAtom>();

            //BGM.acb
            CriAtom.AddCueSheet(_cueSheetAll, $"{_cueSheetAll}.acb", null, null);

            //CriAtomSourceBGM
            _criAtomSourceAll = gameObject.AddComponent<CriAtomSource>();
            _criAtomSourceAll.cueSheet = _cueSheetAll;

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
        CriAtomSource.Status status = _criAtomSourceAll.status;
        if ((status == CriAtomSource.Status.Stop) || (status == CriAtomSource.Status.PlayEnd))
        {
            this._criAtomExPlayback = _criAtomSourceAll.Play(index);
            startFlag = true;
        }
        if (startFlag == false)
        {
            int cur = this._criAtomExPlayback.GetCurrentBlockIndex();
            CriAtomExAcb acb = CriAtom.GetAcb(_cueSheetAll);
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

    public void CRIPauseAudio(bool isPause) => _criAtomSourceAll.Pause(isPause);
    public void CRIPlaySE(int index) => _criAtomSourceAll.Play(index);
}

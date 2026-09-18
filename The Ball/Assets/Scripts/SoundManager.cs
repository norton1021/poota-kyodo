using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundData
    {
        // 音の名前
        public string name;
        // 音源
        public AudioClip audioClip;
        // 音量
        public float volume;
        // 一度再生してから、次再生出来るまでの間隔(秒)
        public float playableDistance;
        // 前回再生した時間
        public float playedTime;
    }

    [SerializeField, Header("Volumeは0.0～1.0の範囲で指定してね\nPlayed Timeは編集しないでください")]
    SoundData[] soundDatas;

    // AudioSource（スピーカー）を同時に鳴らしたい音の数だけ用意
    AudioSource[] audioSourceList = new AudioSource[10];

    // 別名(name)をキーとしたSoundの管理用Dictionary
    Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    [System.Serializable]
    public class BgmData
    {
        // 音の名前
        public string name;
        // 音源
        public AudioClip bgmClip;
        // 音量
        public float volume;
    }

    [SerializeField, Header("Volumeは0.0～1.0の範囲で指定してね")]
    BgmData[] bgmDatas;

    // BGM専用のスピーカーを1つだけ用意
    AudioSource bgmSource;

    // 別名(name)をキーとしたBGMの管理用Dictionary
    Dictionary<string, BgmData> bgmDictionary = new Dictionary<string, BgmData>();

    void Start()
    {
        // audioSourceList配列の数だけAudioSourceを自分自身に生成して配列に格納
        for (int i = 0; i < audioSourceList.Length; i++)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }

        // ループ再生できるBGM専用のAudioSourceを用意
        this.bgmSource = gameObject.AddComponent<AudioSource>();
        this.bgmSource.loop = true;

        // soundDictionaryにセット
        foreach (SoundData soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
        }

        // bgmDictionaryにセット
        foreach (BgmData bgmData in bgmDatas)
        {
            bgmDictionary.Add(bgmData.name, bgmData);
        }
    }

    // 指定された別名で登録されたAudioClipを再生
    public void PlayTheSound(string name)
    {
        // 管理用Dictionaryから、別名で探索
        if (soundDictionary.TryGetValue(name, out SoundData soundData))
        {
            // 爆音が流れるのを防止
            if (soundData.volume > 1)
            {
                Debug.LogWarning("音量は0.0～1.0の範囲で指定してください");
                return;
            }

            // まだ再生するには早い
            if (Time.realtimeSinceStartup - soundData.playedTime < soundData.playableDistance)
            {
                return;
            }

            // 次回用に今回の再生時間の保持
            soundData.playedTime = Time.realtimeSinceStartup;
            // 見つかったら、再生
            CheckTheSound(soundData.audioClip, soundData.volume); 
        }
        else
        {
            Debug.LogWarning("その別名は登録されていません: " + name);
        }
    }

    // 指定された別名で登録されたAudioClipを再生
    public void PlayTheBGM(string name)
    {
        // nameに「stop」と送られたら現在再生中のBGMをフェードアウトさせる
        if (name == "stop")
        {
            this.bgmSource.Stop();
            return;
        }

        // 管理用Dictionaryから、別名で探索
        if (bgmDictionary.TryGetValue(name, out BgmData bgmData))
        {
            // 爆音が流れるのを防止
            if (bgmData.volume > 1)
            {
                Debug.LogWarning("音量は0.0～1.0の範囲で指定してください");
                return;
            }

            // BGMが再生中なら
            if (this.bgmSource.isPlaying)
            {
                // 現在再生中のBGMと同じなら無視
                if (this.bgmSource.clip == bgmData.bgmClip)
                {
                    return;
                }
                // 現在再生中のBGMと違うなら
                else
                {
                    // BGMを流す
                    this.bgmSource.clip = bgmData.bgmClip;
                    this.bgmSource.volume = bgmData.volume;
                    this.bgmSource.Play();
                }
            }
            // BGMが流れていないなら
            else
            {
                // BGMを流す
                this.bgmSource.clip = bgmData.bgmClip;
                this.bgmSource.volume = bgmData.volume;
                this.bgmSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("その別名は登録されていません: " + name);
        }
    }

    // 指定されたAudioClipを未使用のAudioSourceで再生
    void CheckTheSound(AudioClip clip, float volume)
    {
        AudioSource audioSource = GetUnusedAudioSource();

        // 再生できませんでした
        if (audioSource == null)
        {
            return;
        }

        // 未使用のAudioSourceにクリップを取り付けて再生
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    // 未使用のAudioSourceの取得。全て使用中の場合はnullを返却
    AudioSource GetUnusedAudioSource()
    {
        for (int i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false)
            {
                return audioSourceList[i];
            }
        }

        // 未使用のAudioSourceは見つかりませんでした
        return null;
    }
}
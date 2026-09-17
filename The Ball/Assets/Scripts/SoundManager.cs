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
        // 一度再生してから、次再生出来るまでの間隔(秒)
        public float playableDistance;
        // 前回再生した時間
        public float playedTime;
    }

    [SerializeField, Header("Played Timeは編集しないでね")]
    SoundData[] soundDatas;

    // AudioSource（スピーカー）を同時に鳴らしたい音の数だけ用意
    AudioSource[] audioSourceList = new AudioSource[10];

    // 別名(name)をキーとした管理用Dictionary
    Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    void Start()
    {
        // audioSourceList配列の数だけAudioSourceを自分自身に生成して配列に格納
        for (int i = 0; i < audioSourceList.Length; i++)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }

        // soundDictionaryにセット
        foreach (SoundData soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
        }
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

    // 指定されたAudioClipを未使用のAudioSourceで再生
    void CheckTheSound(AudioClip clip)
    {
        AudioSource audioSource = GetUnusedAudioSource();

        // 再生できませんでした
        if (audioSource == null)
        {
            return;
        }

        // 未使用のAudioSourceにクリップを取り付けて再生
        audioSource.clip = clip;
        audioSource.Play();
    }

    // 指定された別名で登録されたAudioClipを再生
    public void PlayTheSound(string name)
    {
        // 管理用Dictionaryから、別名で探索
        if (soundDictionary.TryGetValue(name, out SoundData soundData))
        {
            // まだ再生するには早い
            if (Time.realtimeSinceStartup - soundData.playedTime < soundData.playableDistance)
            {
                return;
            }

            // 次回用に今回の再生時間の保持
            soundData.playedTime = Time.realtimeSinceStartup;
            // 見つかったら、再生
            CheckTheSound(soundData.audioClip); 
        }
        else
        {
            Debug.LogWarning("その別名は登録されていません: " + name);
        }
    }
}
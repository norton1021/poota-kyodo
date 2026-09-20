using UnityEngine;

public class CannonController : MonoBehaviour
{
    [System.Serializable]
    public class CannonData
    {
        [Header("弾")]
        public GameObject bulletPrefab;
        [Header("最初の弾を撃つまでの時間")]
        public float initialInterbal;
        [Header("発射間隔")]
        public float interval;
        [Header("弾速")]
        public float bulletVelocity;
    }

    [SerializeField, Header("大砲の設定")]
    CannonData cannonData;

    float delta = 0;

    void Start()
    {
        this.delta = this.cannonData.initialInterbal;
    }

    void Update()
    {
        this.delta -= Time.deltaTime;
        if (this.delta <= 0)
        {
            this.delta = this.cannonData.interval;
            GameObject cannon = Instantiate(this.cannonData.bulletPrefab, transform.position, transform.rotation);
            cannon.transform.localScale = this.transform.localScale;
            cannon.GetComponent<BulletController>().bulletVerocity = this.cannonData.bulletVelocity;
        }
    }
}

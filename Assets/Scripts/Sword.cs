using UnityEngine;

public enum SwordType
    {
        None,
        Wind,
        Rain,
        Sun,
        Snow,
        
    }

public class Sword : MonoBehaviour
{
    public SwordType currentType = SwordType.None;
    // Prefab references for each sword effect
    [SerializeField] GameObject windTornadoPrefab;
    [SerializeField] GameObject lightningPrefab;
    [SerializeField] GameObject sunRayPrefab;
    [SerializeField] GameObject snowFreezePrefab;

    public void SetSwordType(Vector2 direction)
    {
        switch(currentType)
        {
            case SwordType.Wind:
                // Handle Wind type
                ShootTornado(direction);
                break;
            case SwordType.Rain:
                // Handle Rain type
                ThunderStrike(direction);
                break;
            case SwordType.Sun:
                // Handle Sun type
                SunRay(direction);
                break;
            case SwordType.Snow:
                // Handle Snow type
                SnowFreeze(direction);
                break;
        }
    }
    void ShootTornado(Vector2 direction)
    {
        GameObject tornado = Instantiate(windTornadoPrefab, transform.position, Quaternion.identity);
        tornado.GetComponent<TornadoProjectile>().Setup(direction, 8f);
    }

    void ThunderStrike(Vector2 direction)
    {
        GameObject thunder = Instantiate(lightningPrefab, transform.position, Quaternion.identity);
        thunder.GetComponent<LightningProjectile>().Setup(direction, 6f);
    }

    void SunRay(Vector2 direction)
    {
        GameObject sunRay = Instantiate(sunRayPrefab, transform.position, Quaternion.identity);
        //sunRay.GetComponent<SunRay>().Setup(direction, 5f); not yet implimented
    }
    void SnowFreeze(Vector2 direction)
    {
        GameObject snowFreeze = Instantiate(snowFreezePrefab, transform.position, Quaternion.identity);
        //snowFreeze.GetComponent<SnowFreeze>().Setup(direction, 4f); not yet implimented
    }
}

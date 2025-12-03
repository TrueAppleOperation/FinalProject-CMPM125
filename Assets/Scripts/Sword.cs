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

    [SerializeField] GameObject windTornadoPrefab;
    [SerializeField] GameObject lightningPrefab;
    [SerializeField] GameObject sunRayPrefab;
    [SerializeField] GameObject snowFreezePrefab;

    public void SetSwordType(Vector2 direction)
    {
        switch (currentType)
        {
            case SwordType.Wind:
                ShootTornado(direction);
                break;

            case SwordType.Rain:
                ThunderStrike(direction);
                break;

            case SwordType.Sun:
                SunRay(direction);
                break;

            case SwordType.Snow:
                SnowFreeze(direction);
                break;
        }
    }

    // debug helper so pressing P always spawns a sun ray
    public void DebugSpawnSun(Vector2 direction)
    {
        Debug.Log("DebugSpawnSun called");
        SunRay(direction);
    }

    void ShootTornado(Vector2 direction)
    {
        GameObject tornado = Instantiate(windTornadoPrefab, transform.position, Quaternion.identity);
        tornado.GetComponent<TornadoProjectile>().Setup(direction, 8f);
    }

    void ThunderStrike(Vector2 direction)
    {
        Debug.Log("Lighting Prefab: " + lightningPrefab);
        GameObject lightning = Instantiate(lightningPrefab, transform.position, Quaternion.identity);
        lightning.GetComponent<LightningProjectile>().Setup(direction, 6f);
    }

    void SunRay(Vector2 direction)
{
    Debug.Log("SunRay() called, dir = " + direction);

  
    Vector3 spawnPos = transform.position + (Vector3)direction * 2f;

    GameObject sunRay = Instantiate(sunRayPrefab, spawnPos, Quaternion.identity);

    SunProjectile sp = sunRay.GetComponent<SunProjectile>();
    if (sp == null)
    {
        Debug.LogError("SunProjectile missing on prefab!");
        return;
    }

    sp.Setup(direction, 5f);
}


    void SnowFreeze(Vector2 direction)
    {
        GameObject snowFreeze = Instantiate(snowFreezePrefab, transform.position, Quaternion.identity);
        snowFreeze.GetComponent<SnowProjectile>().Setup(direction, 4f);
    }
}

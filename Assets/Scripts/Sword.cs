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
    private SunProjectile activeSun; 

    [SerializeField] GameObject windTornadoPrefab;
    using UnityEngine;

    public enum SwordType { None, Wind, Rain, Sun, Snow }

    public class Sword : MonoBehaviour
    {
        public SwordType currentType = SwordType.None;
        private SunProjectile activeSun;

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

            if (activeSun != null)
            {
                Debug.Log("SunRay: sunPower already active, skipping spawn.");
                return;
            }

            if (sunRayPrefab == null)
            {
                Debug.LogError("SunRay: sunRayPrefab is NULL!");
                return;
            }

            GameObject sunRay = Instantiate(sunRayPrefab, transform.position, Quaternion.identity);
            SunProjectile sp = sunRay.GetComponent<SunProjectile>();
            if (sp == null)
            {
                Debug.LogError("SunRay: SunProjectile component missing on prefab");
                Destroy(sunRay);
                return;
            }

            sp.Setup(direction, 5f);
            activeSun = sp;
            StartCoroutine(ClearSunWhenDestroyed(sunRay));
        }

        private System.Collections.IEnumerator ClearSunWhenDestroyed(GameObject sunRay)
        {
            while (sunRay != null)
                yield return null;
            activeSun = null;
        }

        void SnowFreeze(Vector2 direction)
        {
            GameObject snowFreeze = Instantiate(snowFreezePrefab, transform.position, Quaternion.identity);
            snowFreeze.GetComponent<SnowProjectile>().Setup(direction, 4f);
        }
    }

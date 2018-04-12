using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Events : MonoBehaviour
{
    public EnumWeatherType weatherType = EnumWeatherType.CLOUDY;

    public GameObject rainObject;
    public GameObject spawnZone;

    [SerializeField] private GameObject pitPrefab;
    [SerializeField] private GameObject poolPrefab;

    public float pitChance = 10f;

    public GameObject fogObject;

    public GameObject mealPrefab;
    public GameObject helmetPrefab;

    private bool attacking;

    private void Start()
    {
        InvokeRepeating("RandomEvent", 15f, 30f);
        InvokeRepeating("RandomObject", 2f, 10f);
    }

    private void RandomEvent()
    {
        rainObject.SetActive(false);
        fogObject.SetActive(false);

        var chance = Random.Range(0, 100);
        if (chance < 35)
        {
            weatherType = EnumWeatherType.RAIN;
            rainObject.SetActive(true);
        }

        if (chance >= 35 && chance < 60)
        {
            weatherType = EnumWeatherType.CLOUDY;
        }

        if (chance >= 60)
        {
            weatherType = EnumWeatherType.FOGGY;
            fogObject.SetActive(true);
        }

        Debug.Log("current weather: " + weatherType);

        chance = Random.Range(0, 100);

        var position = RandomPointInBox(spawnZone.transform.position,
            spawnZone.transform.localScale);
        if (chance <= 60)
        {
            GameObject mealObject = Instantiate(mealPrefab);
            mealObject.transform.position = position;
        }
        else
        {
            GameObject mealObject = Instantiate(helmetPrefab);
            mealObject.transform.position = position;
        }
    }

    private void RandomObject()
    {
        Vector3 position;
        switch (weatherType)
        {
            case EnumWeatherType.CLOUDY:
            case EnumWeatherType.FOGGY:
                var chance = Random.Range(0, 100);
                if (chance <= pitChance)
                {
                    position = RandomPointInBox(spawnZone.transform.position,
                        spawnZone.transform.localScale);

                    var spawnedPit = Instantiate(pitPrefab);
                    if (GameObject.Find("Player").GetComponent<Collider2D>()
                        .IsTouching(spawnedPit.GetComponent<Collider2D>()))
                    {
                        Destroy(spawnedPit);
                    }

                    position.z = 100;
                    spawnedPit.transform.position = position;
                    pitChance = 10f;
                    Camera.main.GetComponent<CameraShake>().shakeDuration = 2f;
                }
                else
                {
                    pitChance += 10f;
                }

                break;
            case EnumWeatherType.RAIN:
                position = RandomPointInBox(spawnZone.transform.position,
                    spawnZone.transform.localScale);
                var spawnedPool = Instantiate(poolPrefab);
                position.z = 100;
                spawnedPool.transform.position = position;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3(
                   (Random.value - 0.5f) * size.x,
                   (Random.value - 0.5f) * size.y,
                   (Random.value - 0.5f) * size.z
               );
    }

    public enum EnumWeatherType
    {
        RAIN,
        CLOUDY,
        FOGGY
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ultMaster : MonoBehaviour
{
    public Canvas canvas;
    public GameObject transitionAPrefab;
    public GameObject transitionBPrefab;
    public GameObject flameRingAPrefab;
    public GameObject flameRingBPrefab;
    public GameObject flameRingCPrefab;
    public GameObject crossBoltAPrefab;
    public GameObject crossBoltBPrefab;
    public GameObject fancyStrikeAPrefab;
    public GameObject fancyStrikeBPrefab;
    public GameObject lightningStrikeAPrefab;
    private GameObject ultIcon;
    private Text gauge;
    private Image ringLoaded;
    private GameObject transitionA;
    private GameObject transitionB;
    private string appendToGauge;
    private float fullyLoadedTime;
    private int mainTextSize;
    private bool transitionASpawned = false;
    private bool transitionBSpawned = false;
    private bool flamesSpawned = false;
    private bool lightningSpawned = false;
    private bool gaugeFilled = false;
    private float timeElapsed = 0f;
    private float gaugeVal = 0;
    private List<GameObject> flameRings = new List<GameObject>();
    private List<GameObject> lightningStyles = new List<GameObject>();
    private readonly int fullyLoaded = 100;
    private readonly int loadSpeed = 45;
    private readonly float delayToStartSpawnFlames = 0.2f;
    private readonly float delayToStartSpawnLightning = 1f;
    private readonly float delayDuringSpawnFlamesMin = 0.4f;
    private readonly float delayDuringSpawnFlamesMax = 1.5f;
    private readonly float delayBetweenLightningMin = 0.3f;
    private readonly float delayBetweenLightningMax = 1.2f;
    private readonly float delayToDestroyTransition = 8f;
    private readonly float delayToStartTransitionB = 0.25f;
    private readonly int flamesXYMin = -100;
    private readonly int flamesXYMax = 100;
    private readonly int flamesXYRotateMax = 65;
    private readonly int flamesToSpawnMin = 4;
    private readonly int flamesToSpawnMax = 12;
    private readonly int lightningXYMin = -35;
    private readonly int lightningXYMax = 40;
    private readonly int lightningXYRotateMax = 15;

    void Start()
    {
        ultIcon = canvas.transform.Find("ultIcon").gameObject;
        gauge = ultIcon.transform.GetComponentInChildren<Text>();
        ringLoaded = ultIcon.transform.Find("ringLoaded").GetComponent<Image>();
        flameRings.Add(flameRingAPrefab);
        flameRings.Add(flameRingBPrefab);
        lightningStyles.Add(crossBoltAPrefab);
        lightningStyles.Add(crossBoltBPrefab);
        lightningStyles.Add(fancyStrikeAPrefab);
        lightningStyles.Add(fancyStrikeBPrefab);
        lightningStyles.Add(lightningStrikeAPrefab);
        mainTextSize = gauge.fontSize;
        appendToGauge = "<size=" + (mainTextSize / 2).ToString() + ">%</size>";
        gauge.text = gaugeVal + appendToGauge;
        ringLoaded.fillAmount = 0;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (!gaugeFilled)
        {
            updateGauge();
        }
        else
        {
            if (!transitionASpawned)
            {
                spawnTransitionA();
            }

            if ((timeElapsed >= fullyLoadedTime + delayToStartSpawnFlames) &&
                (!flamesSpawned))
            {
                initializeFlames();
            }

            Destroy(ultIcon);
            Destroy(transitionA, delayToDestroyTransition);

            if ((timeElapsed >= fullyLoadedTime + delayToStartTransitionB) &&
                (!transitionBSpawned))
            {
                spawnTransitionB();
            }

            if ((timeElapsed >= fullyLoadedTime + delayToStartSpawnLightning) &&
                (!lightningSpawned))
            {
                spawnLightning();
            }
        }
    }

    void updateGauge()
    {
        if (gaugeVal < fullyLoaded)
        {
            gaugeVal = timeElapsed * loadSpeed;
            gauge.text = (int)gaugeVal + appendToGauge;
            ringLoaded.fillAmount = gaugeVal / 100;
            fullyLoadedTime = timeElapsed;
        }
        else
        {
            gaugeFilled = true;
        }
    }

    void spawnTransitionA()
    {
        transitionA = Instantiate(transitionAPrefab) as GameObject;
        transitionA.transform.SetParent(canvas.transform, false);
        Transform triangleMaskTransform = transitionA.transform.Find("ringDTriangleMask");
        triangleMaskTransform.Rotate(0, 0, Random.Range(0, 360));
        transitionASpawned = true;
    }

    void spawnTransitionB()
    {
        GameObject transitionB = Instantiate(transitionBPrefab) as GameObject;
        transitionB.transform.SetParent(canvas.transform, false);
        transitionBSpawned = true;
    }

    void initializeFlames()
    {
        int numFlamesToSpawn = Random.Range(flamesToSpawnMin, flamesToSpawnMax);
        for (int i = 0; i < numFlamesToSpawn; i++)
        {
            int flameToSpawn = Random.Range(0, flameRings.Count);
            StartCoroutine(spawnFlames(flameToSpawn));
        }
        flamesSpawned = true;
    }

    void spawnLightning()
    {
        lightningSpawned = true;
        float lightningDelayTime = Random.Range(delayBetweenLightningMin, delayBetweenLightningMax);
        StartCoroutine(canSpawnLightningDelay(lightningDelayTime));
        int lightningStyleToSpawn = Random.Range(0, lightningStyles.Count);
        GameObject lightning = Instantiate(lightningStyles[lightningStyleToSpawn]);
        lightning.transform.SetParent(canvas.transform, false);
        lightning.transform.Translate(new Vector3(Random.Range(lightningXYMin, lightningXYMax),
            Random.Range(lightningXYMin, lightningXYMax), 0f));
        lightning.transform.Rotate(Random.Range(0, lightningXYRotateMax),
            Random.Range(0, lightningXYRotateMax), Random.Range(0, 360));
    }

    IEnumerator canSpawnLightningDelay(float lightningDelayTime)
    {
        yield return new WaitForSeconds(lightningDelayTime);
        lightningSpawned = false;
    }

    IEnumerator spawnFlames(int flameToSpawn)
    {
        GameObject flameRing = Instantiate(flameRings[flameToSpawn]) as GameObject;
        flameRing.transform.SetParent(canvas.transform, false);
        flameRing.transform.Translate(new Vector3(Random.Range(flamesXYMin, flamesXYMax),
            Random.Range(flamesXYMin, flamesXYMax), 0f));
        flameRing.transform.Rotate(Random.Range(0, flamesXYRotateMax),
            Random.Range(0, flamesXYRotateMax), Random.Range(0, 360));
        yield return new WaitForSeconds(Random.Range(delayDuringSpawnFlamesMin, delayDuringSpawnFlamesMax));
    }
}

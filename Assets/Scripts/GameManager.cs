using System.Collections;
using TMPro;
using UnityEngine;
using Vuforia;

public class ARInteraction : MonoBehaviour
{
    public GameObject cubePrefab;
    private GameObject spawnedCube;
    public GameObject imgTarget;
    private AudioSource soundComp;
    public AudioClip soundClick, soundInst, soundCollect;
    public bool targetFound = false;
    public TMP_Text textScore;

    private int _score = 0;
    public int score { get { return _score; }
        set
        {
            _score = value;
            textScore.text = $"Score: {_score}";
        }
    }

    private void Start()
    {
        soundComp = GetComponent<AudioSource>();
        score = 0;

        if (spawnedCube == null)
        {
            spawnedCube = Instantiate(cubePrefab, imgTarget.transform);
            spawnedCube.SetActive(false);

        }
    }

    public void cubeEnable()
    {
        soundComp.clip = soundInst;
        soundComp.Play();

        Renderer cubeRenderer = spawnedCube.GetComponent<Renderer>();
        cubeRenderer.enabled = true;
        Vector3 spawnPosition = new Vector3(Random.Range(-50f, 50f), Random.Range(5f, 5), Random.Range(-95, 95));
        spawnedCube.transform.localPosition = spawnPosition;
        spawnedCube.SetActive(true);
        spawnedCube.transform.GetChild(0).gameObject.SetActive(false);
        spawnedCube.transform.GetChild(1).gameObject.SetActive(false);


    }
    public void cubeDisable()
    {
        if(spawnedCube != null)
        {
            if (spawnedCube.activeSelf)
            {
                soundComp.clip = soundClick;
                soundComp.Play();
                spawnedCube.SetActive(false);

                if (targetFound)
                    Invoke("cubeEnable", 1);
            }
        }
        

    }

    public void fncTargetFound()
    {
        targetFound = true;
    }
    public void fncTargetDisable()
    {
        targetFound = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && spawnedCube != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == spawnedCube)
            {
                score++;
                soundComp.clip = soundCollect;
                soundComp.Play();

                Renderer cubeRenderer = spawnedCube.GetComponent<Renderer>();
                cubeRenderer.enabled = false;
                spawnedCube.transform.GetChild(0).gameObject.SetActive(true);
                spawnedCube.transform.GetChild(1).gameObject.SetActive(true);

                Invoke("cubeDisable", 2);
            }
        }
    }
}

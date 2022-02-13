using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material paradiseSkybox;
    [SerializeField] private GameObject paradiseEnv;
    [SerializeField] private GameObject desertEnv;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoTransition()
    {
        RenderSettings.skybox = paradiseSkybox;
        desertEnv.SetActive(false);
        paradiseEnv.SetActive(true);
    }
}

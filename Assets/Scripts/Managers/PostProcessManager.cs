using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public EnviromentController env;
    public Volume volume;
    private Bloom bloom;
    private FilmGrain grain;

    [SerializeField] private AnimationCurve bloomCurve;
    [SerializeField] private AnimationCurve grainCurve;
    public float transitionDuration = 2f;

    private bool halfway = false;
    private bool transitionFinished = false;
    void Start()
    {
        CacheObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CacheObjects()
    {
        volume.profile.TryGet<Bloom>(out bloom);
        volume.profile.TryGet<FilmGrain>(out grain);
    }

    public void DoTransition()
    {
        StartCoroutine(TransitionCoroutine());
    }

    IEnumerator TransitionCoroutine()
    {
        float time = 0;
        while (time < transitionDuration)
        {
            if (!halfway && time > transitionDuration / 2)
            {
                halfway = true;
                Halfway();
            }
            bloom.intensity.value = bloomCurve.Evaluate(time) * 10;
            grain.intensity.value = grainCurve.Evaluate(time);
            time += Time.deltaTime;
            yield return null;
        }

        bloom.intensity.value = bloomCurve.keys[bloomCurve.length - 1].value * 10;
        grain.intensity.value = grainCurve.keys[grainCurve.length - 1].value;
        transitionFinished = true;
    }

    void Halfway()
    {
        env.DoTransition();
    }
}

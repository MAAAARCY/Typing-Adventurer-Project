using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour 
{
    [SerializeField] private AudioClip[] SE;
    [SerializeField] private AnimationCurve fadeIn;

    private bool StartFlag;
    private float spawnEffectTime = 2;
    private float pause = 1;
    private AudioSource SESource;

    ParticleSystem ps;
    float timer = 0;
    float countup = 0;
    int status = 0;
    Renderer _renderer;

    int shaderProperty;

    void Start()
    {
        shaderProperty = Shader.PropertyToID("_cutoff");
        _renderer = GetComponent<Renderer>();

        StartFlag = true;
        this.SESource = this.GetComponent<AudioSource>();
    }
	
    void Update()
    {
        if (countup > 3.0f)
        {
            if (status == 0)
            {
                this.SESource.PlayOneShot(this.SE[0]);
                status++;
            }
            if (timer < spawnEffectTime + pause)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                StartFlag = false;
            }
        }
        else
        {
            countup += Time.deltaTime;
        }

        if (StartFlag) _renderer.material.SetFloat(shaderProperty, fadeIn.Evaluate( Mathf.InverseLerp(0, spawnEffectTime, timer)));
        
    }

    public void LoseBossSound()
    {
        this.SESource.PlayOneShot(this.SE[1]);
    }
}

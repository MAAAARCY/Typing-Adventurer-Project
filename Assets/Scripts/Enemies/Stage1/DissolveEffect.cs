using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour {

    private bool StartFlag;
    public float spawnEffectTime = 2;
    public float pause = 1;
    public AnimationCurve fadeIn;
    public AudioClip[] SE;
    private AudioSource SESource;

    ParticleSystem ps;
    float timer = 0;
    float countup = 0;
    int status = 0;
    Renderer _renderer;

    int shaderProperty;

	void Start ()
    {
        shaderProperty = Shader.PropertyToID("_cutoff");
        _renderer = GetComponent<Renderer>();

        StartFlag = true;
        this.SESource = this.GetComponent<AudioSource>();
    }
	
	void Update ()
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
                //this.Gankyuu_obj_for_animation.SetActive(false);
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

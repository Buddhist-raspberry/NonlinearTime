using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGlowing : Glowing
{
    TrailRenderer m_renderer;
    [ColorUsageAttribute(true,true)]
    public Color SpeedUpColor;
    [ColorUsageAttribute(true,true)]
    public Color SpeedDownColor;
    [ColorUsageAttribute(true,true)]
    public Color InvertColor;
    [ColorUsageAttribute(true,true)]
    public Color SelectedColor;
    private bool selected;

    protected override void Start(){
        base.Start();
        m_renderer = GetComponent<TrailRenderer>();
        m_renderer.materials[0].SetFloat("_Edge", 1f);
        selected = false;
    }
    protected override void SetGlowing(float timeScale)
    {
        if (!selected) {
            if (timeScale <= 0){
                m_renderer.materials[0].SetFloat("_RimIntensity", 1);
                m_renderer.materials[0].SetColor("_RimColor", InvertColor);
                m_renderer.materials[0].SetColor("_EdgeColor", InvertColor);
            }
            else if (timeScale > 0 && timeScale < 1){
                float intensity = timeScale;
                m_renderer.materials[0].SetFloat("_RimIntensity", intensity);
                m_renderer.materials[0].SetColor("_RimColor", SpeedDownColor);
                m_renderer.materials[0].SetColor("_EdgeColor", SpeedDownColor);
            }
            else if (timeScale > 1 ){
                float intensity = (timeScale-1)/10.0f;
                intensity = Mathf.Min(intensity, 1);
                m_renderer.materials[0].SetFloat("_RimIntensity", intensity);
                m_renderer.materials[0].SetColor("_RimColor", SpeedUpColor);
                m_renderer.materials[0].SetColor("_EdgeColor", SpeedUpColor);
            }
            else if (timeScale == 1){
                m_renderer.materials[0].SetFloat("_RimIntensity", 0);
                m_renderer.materials[0].SetColor("_RimColor", SpeedUpColor);
                m_renderer.materials[0].SetColor("_EdgeColor", SpeedUpColor);
            }
        }
        else {
            m_renderer.materials[0].SetColor("_EdgeColor", SelectedColor);
        }
    }

    public override void Selected()
    {
        selected = true;
    }
    public override void Unselected()
    {
        selected = false;
    }
}

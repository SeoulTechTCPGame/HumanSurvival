using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform bar;

    public void SetState(float current, float max)
    {
        float state = (float)current;
        state /= max;
        if (state < 0f) state = 0f;
        bar.transform.localScale=new Vector3(state,1f,1f);
        
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class Blok : Sponsor
{
    private int m_tap = 0;
    private SpriteRenderer m_rnd;

    private void OnEnable()
    {
        m_rnd = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    [SerializeField, Tooltip("Color Step")] private List<Color> m_stepColor = new List<Color>();
    public override void Active()
    {
        if (m_tap >= 2)
        {
            Destroy(gameObject);
            return;
        }
        m_rnd.color = m_stepColor[m_tap];
        m_tap++;
    }
}

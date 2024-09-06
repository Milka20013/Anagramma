using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Vector3 startScale;
    [HideInInspector] public float damageModifierP = 0;
    public Text characters;
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private Color[] colors;
    void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (Hints.instance.isSwapping)
        {
            Hints.instance.SetTileToSwap(this);
            Hints.instance.ActivatePanel(true, characters.text);
            return;
        }
        if (transform.position == startPosition)
        {
            TextField.instance.AttachObject(this.gameObject);
        }
        else
        {
            TextField.instance.DetachObject(this.gameObject);
        }
    }
    public float GetDamageMultiplier()
    {
        return 1 + damageModifierP/100;
    }
    public float GetForceMultiplier()
    {
        return 1 + 0.05f;
    }
    public void CalculateDamageModifier(int pos, int charSetLength)
    {
        if (pos == 1)
        {
            damageModifierP = -8f;
            spr.color = colors[0];
        }
        else if (pos == 2)
        {
            damageModifierP = -5f;
            spr.color = colors[1];
        }
        else if (pos < charSetLength / 3)
        {
            damageModifierP = 0;
            spr.color = colors[2];
        }
        else if (pos < charSetLength * 2 / 3)
        {
            damageModifierP = 10f;
            spr.color = colors[3];
        }
        else
        {
            damageModifierP = 20f;
            spr.color = colors[4];
        }
    }
}

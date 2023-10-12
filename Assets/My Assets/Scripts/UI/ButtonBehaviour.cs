using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        m_Animator.SetBool("Hover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Animator.SetBool("Hover", false);
    }
}

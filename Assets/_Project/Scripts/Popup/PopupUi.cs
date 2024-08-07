﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUi : MonoBehaviour
{
    [SerializeField] private GameObject PopupPanel;
    
    [SerializeField] private PopupAnimationSettings OpenAnimation;
    [SerializeField] private PopupAnimationSettings CloseAnimation;

    [Space(10)] [Header("Prefabs")]
    [SerializeField]  private GameObject LayoutPrefab;
    [SerializeField] private Transform LayoutParent;
    [SerializeField] private Transform PanelBackGround;
    public int padding;

    [Space(10)] [Header("Colors")]
    public Color SelectedButtonColor;
    public Color UnselectedButtonColor;
    public Color BlankButtonColor;
    public Color WrongButtonColor;
    public Color CorrectButtonColor;

    private void Start()
    {
        Popup.Instance.OnPopupOpened += SetPopupButtons;
        Popup.Instance.OnPopupOpened += () => TweenAnimations.PopUpAnimation(PopupPanel, OpenAnimation);
        Popup.Instance.OnPopupClosed += () => TweenAnimations.PopUpAnimation(PopupPanel, CloseAnimation);
        Popup.Instance.OnPopupCreated += OnPopupCreated;
    }

    private void SetPopupButtons()
    {
        var currentQuestion = GameData.CurrentQuestion;
        var isSolution = GameData.IsSolution;
        var buttons = Popup.Instance.Buttons;
        var list = GameData.GetSelections();
        
        for (int i = 0; i < list.Count; i++)
        {
            if (isSolution)
            {
                switch (GameData.GetResult(i))
                {
                    case Result.Correct:
                        buttons[i].ButtonImage.color = CorrectButtonColor;
                        break;
                    case Result.Wrong:
                        buttons[i].ButtonImage.color = WrongButtonColor;
                        break;
                    case Result.Blank:
                        buttons[i].ButtonImage.color = BlankButtonColor;
                        break;
                }
            }
            else
            {
                if (list[i])
                {
                    buttons[i].ButtonImage.color = SelectedButtonColor;
                }
                else
                {
                    buttons[i].ButtonImage.color = UnselectedButtonColor;
                }
            }

            buttons[i].ButtonComponent.interactable = true;
            if (i == currentQuestion)
                buttons[i].ButtonComponent.interactable = false;
        }
    }

    private void OnPopupCreated()
    {
        var Buttons = Popup.Instance.Buttons;
        var Layouts = SetLayouts();
        int layoutCount = Layouts.Count;
        int buttonsPerLayout = Mathf.CeilToInt((float)Buttons.Count / layoutCount);
    
        for (int i = 0; i < Buttons.Count; i++)
        {
            var button = Buttons[i];
            var layoutIndex = i / buttonsPerLayout;
            layoutIndex = Mathf.Min(layoutIndex, layoutCount - 1); // Ensure index is within range
            var layout = Layouts[layoutIndex];
            button.Button.transform.SetParent(layout); 
            button.Button.transform.localScale = Vector3.one;
        }
    
        StartCoroutine(SetPopupBackground());
    }


    private List<Transform> SetLayouts()
    {
        List<Transform> layouts = new List<Transform>();
        var LayoutCount = Popup.Instance.LayoutCount;
        for (int i = 0; i < LayoutCount; i++)
        {
            var tempLayout = Instantiate(LayoutPrefab, LayoutParent);
            layouts.Add(tempLayout.transform);
        }

        return layouts;
    }

    private IEnumerator SetPopupBackground()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(LayoutParent.GetComponent<RectTransform>());
        var rect = LayoutParent.GetComponent<RectTransform>().rect;
        var width = rect.width;
        var height = rect.height;
        PanelBackGround.GetComponent<RectTransform>().sizeDelta = new Vector2(width + padding, height + padding);
    }
}

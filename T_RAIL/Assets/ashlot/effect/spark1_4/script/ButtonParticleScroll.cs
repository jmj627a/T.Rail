using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System;
using UnityEngine.UI;


public class ButtonParticleScroll : MonoBehaviour
{

    private MyGameObject canvas = new MyGameObject();
    private MyGameObject particle = new MyGameObject();    
    private MyGameObject contens_right;
    private MyGameObject scrollview_left;
    private MyGameObject scrollview_right;
    private MyGameObject button_left;
    private MyGameObject button_right;
    private MyGameObject effects = new MyGameObject();
    private MyGameObject page;

    
    private int PagesForEffect = 1;
    private int PagesMaxForEffect;

    private MyGameObject now_scrollview;

    void Start()
    {

        particle.selfobject = GameObject.Find("Particle System");

        
        canvas.selfobject = GameObject.Find("Canvas");

        
        canvas.saiki();

        effects.selfobject = GameObject.Find("Effects");
        effects.saiki();

        
        contens_right = canvas.getChildsDic("ScrollViews/ScrollViewRight/Contents");

        scrollview_left = canvas.getChildsDic("ScrollViews/ScrollViewLeft");
        scrollview_right = canvas.getChildsDic("ScrollViews/ScrollViewRight");
        now_scrollview = scrollview_left;

        button_left = canvas.getChildsDic("Button_Left");
        button_right = canvas.getChildsDic("Button_Right");

        page = canvas.getChildsDic("Page");

        button_left.selfobject.GetComponent<Button>().onClick.AddListener(onClick_left);
        button_right.selfobject.GetComponent<Button>().onClick.AddListener(onClick_right);

        getPageMaxForEffect();
        changeTextOfPage();

        EffectToButton();

    }

    public void onClick_left()
    {

        int temp_PagesForEffect = PagesForEffect;

        changePagesForEffect(-1);

        if (temp_PagesForEffect == PagesForEffect)
        {
            return;
        }

        onClick();
    }

    public void onClick_right()
    {

        int temp_PagesForEffect = PagesForEffect;

        changePagesForEffect(1);

        if (temp_PagesForEffect == PagesForEffect)
        {
            return;
        }

        onClick();
    }

    void onClick()
    {

        if (now_scrollview != scrollview_left)
        {

            scrollview_left.selfobject.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, scrollview_left.selfobject.transform.GetComponent<RectTransform>().anchoredPosition.y);

            scrollview_right.selfobject.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-700, scrollview_right.selfobject.transform.GetComponent<RectTransform>().anchoredPosition.y);
            scrollview_right.selfobject.SetActive(false);

            now_scrollview = scrollview_left;


        }
        else if (now_scrollview != scrollview_right)
        {

            scrollview_right.selfobject.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, scrollview_right.selfobject.transform.GetComponent<RectTransform>().anchoredPosition.y);

            scrollview_left.selfobject.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-700, scrollview_left.selfobject.transform.GetComponent<RectTransform>().anchoredPosition.y);
            scrollview_left.selfobject.SetActive(false);

            now_scrollview = scrollview_right;

        }

        now_scrollview.selfobject.SetActive(true);

        EffectToButton();

    }

    
    void getPageMaxForEffect()
    {

        int temp_sho;
        int temp_amari;

        temp_sho = effects.childobjects.Count / contens_right.childobjects.Count;
        temp_amari = effects.childobjects.Count % contens_right.childobjects.Count;


        if (temp_amari != 0)
        {
            PagesMaxForEffect = temp_sho + 1;
        }
    }

    
    void changeTextOfPage()
    {
        page.getChildDic("Text").selfobject.GetComponent<Text>().text = PagesForEffect + "/" + PagesMaxForEffect;
    }


    
    void changePagesForEffect(int updown)
    {
        if (PagesMaxForEffect <= 1)
        {
            PagesForEffect = 1;
            return;
        }


        if (updown < 0)
        {
            PagesForEffect = PagesForEffect - 1;
            if (PagesForEffect < 1)
            {
                PagesForEffect = 1;
            }

        }
        else
        {

            PagesForEffect = PagesForEffect + 1;

            if (PagesForEffect > PagesMaxForEffect)
            {
                PagesForEffect = PagesMaxForEffect;
            }

        }

        changeTextOfPage();

    }

    
    void EffectToButton()
    {
        
        int tempPagesForEffect = PagesForEffect - 1;
        
        int temp_i = tempPagesForEffect * now_scrollview.getChildDic("Contents").childobjects.Count;
    

        foreach (MyGameObject temp_button in now_scrollview.getChildDic("Contents").childobjects)
        {
            temp_button.selfobject.GetComponent<Button>().onClick.RemoveAllListeners();
            temp_button.getChildDic("Text").selfobject.GetComponent<Text>().text = "";

            if (effects.childobjects.Count > temp_i)
            {
                temp_button.getChildDic("Text").selfobject.GetComponent<Text>().text = effects.childobjects[temp_i].selfobject.name;
                
                ParticleSystem temp_particle;

                temp_particle = effects.childobjects[temp_i].selfobject.GetComponent<ParticleSystem>();


                temp_button.selfobject.GetComponent<Button>().onClick.AddListener(() =>
                {                   

                    if (temp_particle.loop && temp_particle.isPlaying)
                        temp_particle.Stop();
                    else
                        temp_particle.Play();
                });


                temp_i = temp_i + 1;
            }

        }
    }


}

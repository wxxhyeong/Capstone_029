﻿//카드 회전 담당 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rotation1 : MonoBehaviour
{
    private SpriteRenderer rend; //이미지를 표현하는 속성을 가진 객체
    CardInfo thisCard;

    [SerializeField] //private 변수를 inspector에서 접근가능하게 해줌
    private Sprite backSprite;
    [SerializeField]
    int f_c;

    private bool coroutineAllowed, facedUp;
    GameObject director;
    




    void Start()
    {

        rend = GetComponent<SpriteRenderer>();
        thisCard = GetComponent<CardInfo>();
        director = GameObject.Find("GameDirector");


        rend.sprite = backSprite;
        coroutineAllowed = true;
        facedUp = false;


    }

    void Update()
    {
        f_c = director.GetComponent<GameDirector>().f_c;
    }



    private void OnMouseDown()
    {

        f_c = director.GetComponent<GameDirector>().f_c;
        if (coroutineAllowed && f_c < 2 && facedUp == false ) //flip count 뒤집은 횟수가 2가 되면 클릭해도 뒤집기 금지
                                                                     //if (coroutineAllowed)
        {

            StartCoroutine(RotateCard_Front());
            director.GetComponent<GameDirector>().selected_Card(thisCard.wordImage);

        }
    }

    private IEnumerator RotateCard_Front()
    {
        coroutineAllowed = false;
        for (float i = 0f; i <= 180f; i += 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {
                rend.sprite = thisCard.wordImage;
                //thisCard.cardStatus = 1;

                director.GetComponent<GameDirector>().Flip_Count(); //뒤집어지면서 flip count를 증가시킴


            }
            yield return new WaitForSeconds(0.01f);
        }
        coroutineAllowed = true;
        facedUp = !facedUp;
    }

    public IEnumerator RotateCard_back() 
    {
        coroutineAllowed = false;
        yield return new WaitForSeconds(3.0f);
        for (float i = 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = backSprite;
                    //thisCard.cardStatus = 0;

                }
                yield return new WaitForSeconds(0.01f);
            }
        
        coroutineAllowed = true;

        facedUp = !facedUp;
    }

    /*private IEnumerator RotateCard()
    {
        coroutineAllowed = false;
        if (!facedUp)
        {
            for (float i = 0f; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = thisCard.wordImage;
                    //thisCard.cardStatus = 1;

                    director.GetComponent<GameDirector>().Flip_Count(); //뒤집어지면서 flip count를 증가시킴


                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        else if (facedUp)
        {
            for (float i = 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = backSprite;
                    //thisCard.cardStatus = 0;
                    
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        coroutineAllowed = true;

        facedUp = !facedUp;
    }

    // Update is called once per frame*/

}

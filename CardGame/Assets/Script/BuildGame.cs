﻿//게임 시작시 프리팹을 사용한 게임 빌드 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BuildGame : MonoBehaviour
{
    public static Sprite[] Spr;
    public GameObject canvas;
  
    int number = 3;
    float px = -3.0f; //프리팹 놓을 좌표 x
    float py = 2.7f;  //프리팹 놓을 좌표 y

    public GameObject cardPrefab;

    // Start is called before the first frame update  
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        buildgame(8); //게임 난이도에 따라 매개변수 넘기는 값 변경할 예정
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private int[] randomCard(int x, int y)// x개 중에 무작위로 y개 뽑는 함수
    {
        int[] list = new int[y];
        
        for(int i = 0; i<y; i++)
        {
            
            list[i]  = Random.Range(0, x);
            for (int j = 0; j < i; j++)
            {
                if (list[i] == list[j])
                {
                    i -= 1;
                    break;
                }                               
            }
        }
        return list;
    }

    private List<GameObject> prefab_generator(int level)
    {
        List<GameObject> Prefabs = new List<GameObject>();

        Spr = Resources.LoadAll<Sprite>("audi"); //이미지 폴더 내 파일 로드

        int[] rand_image = randomCard(Spr.Length, level/2);
        int[] rand_number = randomCard(8, level);

        for (int i = 0; i<level; i++) //카드 프리팹 생성
        {
            GameObject card = Instantiate(cardPrefab) as GameObject;
            card.transform.SetParent(canvas.transform);
            Prefabs.Add(card);
            
        }

        for(int i = 0; i<level; i++) // 각 카드 프리팹.name에 랜덤 번호 부여
        {
            Prefabs[i].name = rand_number[i].ToString();
        }


        int j = 0;
        for (int i =0; i < level; i+=2)
        {
            Prefabs[rand_number[i]].GetComponent<CardInfo>().wordImage = Spr[rand_image[j]];
            Prefabs[rand_number[i+1]].GetComponent<CardInfo>().wordImage = Spr[rand_image[j]];
            j++;
        }


        return Prefabs;
    }

    private void buildgame(int level) //프리팹 생성하고, 배치하는 함수
    {

        List<GameObject> Prefabs = prefab_generator(level);

        float px1 = px;

        int p = 0;
        for (int j = 0; j < 2; j++)
        {
            float py1 = py;

            for (int i = 0; i < number; i++)
            {
                //GameObject card = Instantiate(cardPrefab) as GameObject; //프리팹 생성
                Prefabs[p].transform.position = new Vector3(px1, py1, 0); //프리팹 배치하기
                
                
                py1 -= 2.7f;
                p += 1;
            }
            px1 += 4.6f;
        }

        float py2 = 1.35f;
        float px2 = px + 2.3f;
        for (int i = 0; i < number - 1; i++)
        {
            //GameObject card = Instantiate(cardPrefab) as GameObject;
            Prefabs[p].transform.position = new Vector3(px2, py2, 0);
            
            py2 -= 2.7f;
            p += 1;
        }
    }
}
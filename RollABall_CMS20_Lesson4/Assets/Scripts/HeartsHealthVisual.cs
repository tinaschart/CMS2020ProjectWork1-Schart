using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsHealthVisual : MonoBehaviour
{
   [SerializeField] private Sprite heart0Sprite;
   [SerializeField] private Sprite heart1Sprite;

   private List<HeartImage> heartImageList;

   private void Awake()
   {
      heartImageList = new List<HeartImage>();
   }

   private void Start()
   {
      CreateHeartImage(new Vector2(0, 0));
      CreateHeartImage(new Vector2(30, 0));
   }

   private Image CreateHeartImage(Vector2 anchoredPosition)
   {
      GameObject heartGameObject = new GameObject("Heart", typeof(Image));
      heartGameObject.transform.parent = transform;
      heartGameObject.transform.localPosition = Vector3.zero;

      heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
      heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);

      Image heartImageUI = heartGameObject.GetComponent<Image>();
      heartImageUI.sprite = heart0Sprite;

      HeartImage heartImage = new HeartImage(heartImageUI);
      heartImageList.Add(heartImage);
      return heartImage;
   }

   public class HeartImage
   {
      private Image heartImage;
      private HeartsHealthVisual heartsHealthVisual;

      public HeartImage(HeartsHealthVisual heartsHealthVisual, Image heartImage)
      {
         this.heartsHealthVisual = heartsHealthVisual;
         this.heartImage = heartImage;
      }

      public void SetHeartFragments(int fragments)
      {
         switch (fragments)
         {
            case 0: heartImage.sprite = heart0Sprite;
               break;
            case 1: heartImage.sprite = heart1Sprite;
               break;
         }
      }
   }
   }
}
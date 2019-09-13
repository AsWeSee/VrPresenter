using System.Collections;
using System.Collections.Generic;
using OVR.OpenVR;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPresentationController : MonoBehaviour
{
    public Sprite[] sprites;
    public Image screen;
    public SpriteRenderer screen2;
    public int currentSlide = 0;
    
    void Start()
    {
        loadSlides();
    }

    void loadSlides()
    {
        currentSlide = 0;
        //TODO: брать все файлы из конкретной папки
        sprites = new Sprite[5];
        //TODO: рассмотреть возможность загрузки pdf
        sprites[0] = Resources.Load<Sprite>("Presentation/1/diff");
        sprites[1] = Resources.Load<Sprite>("Presentation/1/1");
        sprites[2] = Resources.Load<Sprite>("Presentation/1/2");
        sprites[3] = Resources.Load<Sprite>("Presentation/1/3");
        sprites[4] = Resources.Load<Sprite>("Presentation/1/4");
        updateSlide();
    }

    void updateSlide()
    {
        //Если пошли дальше размера презентации, то остаемся в конце
        if (currentSlide >= sprites.Length)
            currentSlide = sprites.Length - 1;
//            currentSlide = 0; //Как альтернатива, вернуться на первый слайд
        
        //Если мы попытались пойти назад на первом слайде, то слайд не меняется.
        if (currentSlide < 0)
            currentSlide = 0;
//            currentSlide = sprites.Length - 1; //либо загружаем последний слайд
        
        
        screen.sprite = sprites[currentSlide];
        screen2.sprite = sprites[currentSlide];
    }

    // Update is called once per frame
    void Update()
    {
        //Переключение слайдов вперёд и назад
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            currentSlide += 1;
            updateSlide();
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            currentSlide -= 1;
            updateSlide();
        }
        
        //Возврат к началу презентации и обновление слайдов из папки
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            loadSlides();
        }
        
        //Проигрывание аплодисментов
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            
        }
    }
}

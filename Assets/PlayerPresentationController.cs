using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OVR.OpenVR;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPresentationController : MonoBehaviour
{
    
//    public List<Sprite> sprites;
//    public Image screen;
//    public SpriteRenderer screen2;
    
    public List<Texture2D> slides;
    public RawImage rawScreen;
    public int currentSlide = 0;

    public AudioSource applauseSound;
    
    private const string folder = @"C:\UnityProjects\VrPresenter\Assets\Resources\Presentation\2\";
    
    void Start()
    {
        applauseSound.Play();
        loadSlides();
        
    }

    void loadSlides()
    {
//        sprites = new List<Sprite>();
        slides = new List<Texture2D>();
        Debug.Log("GetCurrentDirectory " + Directory.GetCurrentDirectory());
        
        //TODO: рендерить pdf в png т.к. презентации в pdf встречаются намного чаще чем в png
        foreach (string file in Directory.EnumerateFiles(folder, "*.png"))
        {
            slides.Add(LoadImage(file));
        }
//        sprites.AddRange(Resources.LoadAll<Sprite>("Presentation/2/"));
        currentSlide = 0;
        updateSlide();
    }

    Texture2D LoadImage(string filename)
    {
        byte[] byteArray = File.ReadAllBytes(filename);
        Texture2D sampleTexture = new Texture2D(2,2);
        bool isLoaded = sampleTexture.LoadImage(byteArray);
        return isLoaded ? sampleTexture : null;
    }

    void updateSlide()
    {
//        Если пошли дальше размера презентации, то остаемся в конце
        if (currentSlide >= slides.Count)
            currentSlide = slides.Count - 1;
//            currentSlide = 0; //Как альтернатива, вернуться на первый слайд
        
//        Если мы попытались пойти назад на первом слайде, то слайд не меняется.
        if (currentSlide < 0)
            currentSlide = 0;
//            currentSlide = sprites.Length - 1; //либо загружаем последний слайд
        
        
//        screen.sprite = sprites[currentSlide];
//        screen2.sprite = sprites[currentSlide];
        if (slides[currentSlide] != null)
            rawScreen.texture = slides[currentSlide];

    }

    private void Update()
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
            applauseSound.Play();
        }
    }
}

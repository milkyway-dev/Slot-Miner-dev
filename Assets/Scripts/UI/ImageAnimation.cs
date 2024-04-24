using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class ImageAnimation : MonoBehaviour
{
    public enum ImageState
    {
        NONE,
        PLAYING,
        PAUSED
    }

    public static ImageAnimation Instance;

    public List<Sprite> textureArray;

    public Image rendererDelegate;

    public bool useSharedMaterial = true;

    public bool doLoopAnimation = true;

    [HideInInspector]
    public ImageState currentAnimationState;

    private int indexOfTexture;

    private float idealFrameRate = 0.0416666679f;

    private float delayBetweenAnimation;

    public float AnimationSpeed = 5f;

    public float delayBetweenLoop;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        //rendererDelegate.sprite = textureArray[0];
        StopAnimation();
    }

    private void AnimationProcess()
    {

        //StartCoroutine(BlinkAnimation());
        this.transform.localScale = new Vector3(1, 1, 1);

        this.transform.DOScale(new Vector2(1.1f, 1.1f), 1f).SetLoops(-1, LoopType.Yoyo);
        //SetTextureOfIndex();
        //indexOfTexture++;
        //if (indexOfTexture == textureArray.Count)
        //{
        //	indexOfTexture = 0;
        //	if (doLoopAnimation)
        //	{
        //		Invoke("AnimationProcess", delayBetweenAnimation + delayBetweenLoop);
        //	}
        //}
        //else
        //{
        //	Invoke("AnimationProcess", delayBetweenAnimation);
        //}
    }

    public void StartAnimation()
    {
        indexOfTexture = 0;
        if (currentAnimationState == ImageState.NONE)
        {
            RevertToInitialState();
            delayBetweenAnimation = idealFrameRate * (float)textureArray.Count / AnimationSpeed;
            currentAnimationState = ImageState.PLAYING;
            //Invoke("AnimationProcess");
            AnimationProcess();
        }
    }


    IEnumerator BlinkAnimation()
    {

        //while (currentAnimationState == ImageState.PLAYING)
        //{

        //}

        this.transform.DOScale(new Vector2(1.2f, 1.2f), 1f).SetLoops(-1, LoopType.Yoyo);
        yield return null;


    }

    public void PauseAnimation()
    {
        if (currentAnimationState == ImageState.PLAYING)
        {
            //StopCoroutine(BlinkAnimation());
            //Anim.Pause();
            DOTween.Pause(this.transform);

            currentAnimationState = ImageState.PAUSED;
        }
    }

    public void ResumeAnimation()
    {
        if (currentAnimationState == ImageState.PAUSED && !IsInvoking("AnimationProcess"))
        {

            DOTween.Play(this.transform);
            currentAnimationState = ImageState.PLAYING;

        }
    }

    public void StopAnimation()
    {
        //if (currentAnimationState != 0)
        //{
        //rendererDelegate.sprite = textureArray[0];
        //CancelInvoke("AnimationProcess");
        //}

        //StopCoroutine(BlinkAnimation());

        //this.Anim.Kill();
        DOTween.Kill(this.transform);
        this.transform.localScale = new Vector3(1, 1, 1);

        //this.transform.DOScale(new Vector2(1f, 1f), 0.2f).SetEase(Ease.Linear);
        currentAnimationState = ImageState.NONE;

    }

    public void RevertToInitialState()
    {
        indexOfTexture = 0;

        //SetTextureOfIndex();
        this.transform.localScale = new Vector3(1, 1, 1);

        //this.transform.DOScale(new Vector2(1f, 1f), 0.2f).SetEase(Ease.Linear);
    }

    private void SetTextureOfIndex()
    {
        if (useSharedMaterial)
        {
            rendererDelegate.sprite = textureArray[indexOfTexture];
        }
        else
        {
            rendererDelegate.sprite = textureArray[indexOfTexture];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;


public class UIManager : MonoBehaviour
{

    [Header("Menu UI")]
    [SerializeField]
    private Button Menu_Button;
    [SerializeField]
    private GameObject Menu_Object;
    [SerializeField]
    private RectTransform Menu_RT;

    [SerializeField]
    private Button About_Button;
    [SerializeField]
    private GameObject About_Object;
    [SerializeField]
    private RectTransform About_RT;


    // [SerializeField]
    // private Button Exit_Button;
    [SerializeField]
    private GameObject Exit_Object;
    [SerializeField]
    private RectTransform Exit_RT;

    [SerializeField]
    private Button Paytable_Button;
    [SerializeField]
    private GameObject Paytable_Object;
    [SerializeField]
    private RectTransform Paytable_RT;

    [SerializeField] private TMP_Text[] SymbolsText;
    [SerializeField] private TMP_Text Jackpot_Text;
    // [SerializeField] private Button exitButton;

    [Header("Popus UI")]
    [SerializeField]
    private GameObject MainPopup_Object;

    [Header("About Popup")]
    [SerializeField]
    private GameObject AboutPopup_Object;
    [SerializeField]
    private Button AboutExit_Button;

    [Header("Paytable Popup")]
    [SerializeField]
    private GameObject PaytablePopup_Object;
    [SerializeField]
    private Button PaytableExit_Button;

    [Header("menu popup")]
    [SerializeField] private Button Menu_button;
    [SerializeField] private Transform Menu_button_grp;
    [SerializeField] private Sprite MenuOpenSprite;
    [SerializeField] private Sprite MenuCloseSprite;

    [Header("Settings popup")]
    [SerializeField] private Button Setting_button;
    [SerializeField] private GameObject settingObject;
    [SerializeField] private Slider SoundSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Button Setting_exit_button;

    [Header("Quit popup")]
    [SerializeField] private GameObject QuitPopupObject;
    [SerializeField] private Button yes_button;
    [SerializeField] private Button no_button;
    [SerializeField] private Button cancel_button;
    [SerializeField] private Button Quit_button;

    [Header("LowBalance Popup")]
    [SerializeField]
    private Button LBExit_Button;
    [SerializeField]
    private GameObject LBPopup_Object;

    [Header("Disconnection Popup")]
    [SerializeField] private Button CloseDisconnect_Button;
    [SerializeField] private GameObject DisconnectPopup_Object;

    [Header("Scripts")]
    [SerializeField] private AudioController audioController;
    [SerializeField] private SlotBehaviour slotBehaviour;
    [SerializeField] private SocketIOManager socketManager;

    [Header("All wins popup")]
    [SerializeField] private GameObject WinPopup_Object;
    [SerializeField] private GameObject jackpot_Object;
    [SerializeField] private TMP_Text Win_Text;
    [SerializeField] private TMP_Text jackpot_Text;
    [SerializeField] private Image Win_Image;
    [SerializeField] private Sprite HugeWin_Sprite;
    [SerializeField] private Sprite BigWin_Sprite;
    [SerializeField] private Sprite MegaWin_Sprite;

    [Header("Splash Screen")]
    [SerializeField] private GameObject spalsh_screen;
    [SerializeField] private Image progressbar;
    [SerializeField] private RectTransform progressbarHandle;
    [SerializeField] private TMP_Text loadingText;

    [Header("AnotherDevice Popup")]
    [SerializeField] private Button CloseAD_Button;
    [SerializeField] private GameObject ADPopup_Object;


    private bool isOpen;
    private bool isMusic = true;
    private bool isSound = true;
    private bool isExit = false;

    private void Awake()
    {
        if (spalsh_screen) spalsh_screen.SetActive(true);
        StartCoroutine(LoadingRoutine());
    }

    private IEnumerator LoadingRoutine()
    {
        StartCoroutine(LoadingTextAnimate());
        float fillAmount = 0.7f;
        progressbar.DOFillAmount(fillAmount, 2f).SetEase(Ease.Linear);
        progressbarHandle.DOAnchorPosX(20 + (fillAmount * (510 - 20)), 2f, true).SetEase(Ease.Linear);
        yield return new WaitForSecondsRealtime(2f);
        yield return new WaitUntil(() => !socketManager.isLoading);
        progressbar.DOFillAmount(1, 1f).SetEase(Ease.Linear);
        progressbarHandle.DOAnchorPosX(510, 1f, true).SetEase(Ease.Linear);
        yield return new WaitForSecondsRealtime(1f);
        if (spalsh_screen) spalsh_screen.SetActive(false);
        StopCoroutine(LoadingTextAnimate());
    }

    private IEnumerator LoadingTextAnimate()
    {
        while (true)
        {
            if (loadingText) loadingText.text = "Loading.";
            yield return new WaitForSeconds(0.5f);
            if (loadingText) loadingText.text = "Loading..";
            yield return new WaitForSeconds(0.5f);
            if (loadingText) loadingText.text = "Loading...";
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void Start()
    {


        // if (Menu_Button) Menu_Button.onClick.RemoveAllListeners();
        // if (Menu_Button) Menu_Button.onClick.AddListener(OpenMenu);

        // if (Exit_Button) Exit_Button.onClick.RemoveAllListeners();
        // if (Exit_Button) Exit_Button.onClick.AddListener(CloseMenu);

        // if (About_Button) About_Button.onClick.RemoveAllListeners();
        // if (About_Button) About_Button.onClick.AddListener(delegate { OpenPopup(AboutPopup_Object); });

        // if (AboutExit_Button) AboutExit_Button.onClick.RemoveAllListeners();
        // if (AboutExit_Button) AboutExit_Button.onClick.AddListener(delegate { ClosePopup(AboutPopup_Object); });

        if (Quit_button) Quit_button.onClick.RemoveAllListeners();
        if (Quit_button) Quit_button.onClick.AddListener(delegate { OpenPopup(QuitPopupObject); });

        if (no_button) no_button.onClick.RemoveAllListeners();
        if (no_button) no_button.onClick.AddListener(delegate { ClosePopup(QuitPopupObject); });

        if (cancel_button) cancel_button.onClick.RemoveAllListeners();
        if (cancel_button) cancel_button.onClick.AddListener(delegate { ClosePopup(QuitPopupObject); });

        if (yes_button) yes_button.onClick.RemoveAllListeners();
        if (yes_button) yes_button.onClick.AddListener(CallOnExitFunction);

        if (Paytable_Button) Paytable_Button.onClick.RemoveAllListeners();
        if (Paytable_Button) Paytable_Button.onClick.AddListener(delegate { OpenPopup(PaytablePopup_Object); });

        if (PaytableExit_Button) PaytableExit_Button.onClick.RemoveAllListeners();
        if (PaytableExit_Button) PaytableExit_Button.onClick.AddListener(delegate { ClosePopup(PaytablePopup_Object); });

        if (Setting_button) Setting_button.onClick.RemoveAllListeners();
        if (Setting_button) Setting_button.onClick.AddListener(delegate { OpenPopup(settingObject); });

        if (LBExit_Button) LBExit_Button.onClick.RemoveAllListeners();
        if (LBExit_Button) LBExit_Button.onClick.AddListener(delegate { ClosePopup(LBPopup_Object); });

        if (Setting_exit_button) Setting_exit_button.onClick.RemoveAllListeners();
        if (Setting_exit_button) Setting_exit_button.onClick.AddListener(delegate { ClosePopup(settingObject); });

        if (CloseDisconnect_Button) CloseDisconnect_Button.onClick.RemoveAllListeners();
        if (CloseDisconnect_Button) CloseDisconnect_Button.onClick.AddListener(CallOnExitFunction);

        // if (audioController) audioController.ToggleMute(false);

        isMusic = true;
        isSound = true;

        if (Menu_button) Menu_button.onClick.RemoveAllListeners();
        if (Menu_button) Menu_button.onClick.AddListener(OnMenuClick);


        if (SoundSlider) SoundSlider.onValueChanged.AddListener(delegate { ToggleSound(); });
        if (MusicSlider) MusicSlider.onValueChanged.AddListener(delegate { ToggleMusic(); });

    }
    // private void OpenMenu()
    // {
    //     audioController.PlayButtonAudio();
    //     if (Menu_Object) Menu_Object.SetActive(false);
    //     if (Exit_Object) Exit_Object.SetActive(true);
    //     if (About_Object) About_Object.SetActive(true);
    //     if (Paytable_Object) Paytable_Object.SetActive(true);
    //     if (Settings_Object) Settings_Object.SetActive(true);

    //     DOTween.To(() => About_RT.anchoredPosition, (val) => About_RT.anchoredPosition = val, new Vector2(About_RT.anchoredPosition.x, About_RT.anchoredPosition.y + 150), 0.1f).OnUpdate(() =>
    //     {
    //         LayoutRebuilder.ForceRebuildLayoutImmediate(About_RT);
    //     });

    //     DOTween.To(() => Paytable_RT.anchoredPosition, (val) => Paytable_RT.anchoredPosition = val, new Vector2(Paytable_RT.anchoredPosition.x, Paytable_RT.anchoredPosition.y + 300), 0.1f).OnUpdate(() =>
    //     {
    //         LayoutRebuilder.ForceRebuildLayoutImmediate(Paytable_RT);
    //     });

    //     DOTween.To(() => Settings_RT.anchoredPosition, (val) => Settings_RT.anchoredPosition = val, new Vector2(Settings_RT.anchoredPosition.x, Settings_RT.anchoredPosition.y + 450), 0.1f).OnUpdate(() =>
    //     {
    //         LayoutRebuilder.ForceRebuildLayoutImmediate(Settings_RT);
    //     });
    // }

    // private void CloseMenu()
    // {

    //     DOTween.To(() => About_RT.anchoredPosition, (val) => About_RT.anchoredPosition = val, new Vector2(About_RT.anchoredPosition.x, About_RT.anchoredPosition.y - 150), 0.1f).OnUpdate(() =>
    //     {
    //         LayoutRebuilder.ForceRebuildLayoutImmediate(About_RT);
    //     });

    //     DOTween.To(() => Paytable_RT.anchoredPosition, (val) => Paytable_RT.anchoredPosition = val, new Vector2(Paytable_RT.anchoredPosition.x, Paytable_RT.anchoredPosition.y - 300), 0.1f).OnUpdate(() =>
    //     {
    //         LayoutRebuilder.ForceRebuildLayoutImmediate(Paytable_RT);
    //     });

    //     DOTween.To(() => Settings_RT.anchoredPosition, (val) => Settings_RT.anchoredPosition = val, new Vector2(Settings_RT.anchoredPosition.x, Settings_RT.anchoredPosition.y - 450), 0.1f).OnUpdate(() =>
    //     {
    //         LayoutRebuilder.ForceRebuildLayoutImmediate(Settings_RT);
    //     });

    //     DOVirtual.DelayedCall(0.1f, () =>
    //      {
    //          if (Menu_Object) Menu_Object.SetActive(true);
    //          if (Exit_Object) Exit_Object.SetActive(false);
    //          if (About_Object) About_Object.SetActive(false);
    //          if (Paytable_Object) Paytable_Object.SetActive(false);
    //          if (Settings_Object) Settings_Object.SetActive(false);
    //      });
    // }

    internal void LowBalPopup()
    {
        OpenPopup(LBPopup_Object);
    }

    internal void InitialiseUIData(string SupportUrl, string AbtImgUrl, string TermsUrl, string PrivacyUrl, Paylines symbolsText)
    {
        //if (Support_Button) Support_Button.onClick.RemoveAllListeners();
        //if (Support_Button) Support_Button.onClick.AddListener(delegate { UrlButtons(SupportUrl); });

        //if (Terms_Button) Terms_Button.onClick.RemoveAllListeners();
        //if (Terms_Button) Terms_Button.onClick.AddListener(delegate { UrlButtons(TermsUrl); });

        //if (Privacy_Button) Privacy_Button.onClick.RemoveAllListeners();
        //if (Privacy_Button) Privacy_Button.onClick.AddListener(delegate { UrlButtons(PrivacyUrl); });

        //StartCoroutine(DownloadImage(AbtImgUrl));

        PopulateSymbolsPayout(symbolsText);
    }

    private void PopulateSymbolsPayout(Paylines paylines)
    {
        for (int i = 0; i < SymbolsText.Length - 2; i++)
        {
            string text = null;

            if (paylines.symbols[i].Multiplier[0][0] != 0)
            {
                text += "5x - " + paylines.symbols[i].Multiplier[0][0];
            }
            if (paylines.symbols[i].Multiplier[1][0] != 0)
            {
                text += "\n4x - " + paylines.symbols[i].Multiplier[1][0];
            }
            if (paylines.symbols[i].Multiplier[2][0] != 0)
            {
                text += "\n3x - " + paylines.symbols[i].Multiplier[2][0];
            }


            if (SymbolsText[i]) SymbolsText[i].text = text;
        }

        for (int i = 0; i < paylines.symbols.Count; i++)
        {
            if (paylines.symbols[i].Name.ToUpper() == "JACKPOT")
            {
                if (Jackpot_Text) Jackpot_Text.text = "Jackpot: Mega win triggered by 5 Jackpot symbols on a pay line.\nPayout: <color=yellow>" + paylines.symbols[i].defaultAmount;
                continue;
            }

                string text = null;
            if (paylines.symbols[i].ID == 10)
            {


                if (paylines.symbols[i].Multiplier[0][0] != 0)
                {
                    text += "5x - " + paylines.symbols[i].Multiplier[0][0];
                }
                if (paylines.symbols[i].Multiplier[1][0] != 0)
                {
                    text += "\n4x - " + paylines.symbols[i].Multiplier[1][0];
                }
                if (paylines.symbols[i].Multiplier[2][0] != 0)
                {
                    text += "\n3x - " + paylines.symbols[i].Multiplier[2][0];
                }


                if (SymbolsText[7]) SymbolsText[7].text = text;
            }
            if (paylines.symbols[i].ID == 11)
            {


                if (paylines.symbols[i].Multiplier[0][0] != 0)
                {
                    text += "5x - " + paylines.symbols[i].Multiplier[0][0];
                }
                if (paylines.symbols[i].Multiplier[1][0] != 0)
                {
                    text += "\n4x - " + paylines.symbols[i].Multiplier[1][0];
                }
                if (paylines.symbols[i].Multiplier[2][0] != 0)
                {
                    text += "\n3x - " + paylines.symbols[i].Multiplier[2][0];
                }


                if (SymbolsText[8]) SymbolsText[8].text = text;
            }


        }
    }

    internal void DisconnectionPopup(bool isReconnection)
    {
        //if (isReconnection)
        //{
        //    OpenPopup(ReconnectPopup_Object);
        //}
        //else
        //{
        //ClosePopup(ReconnectPopup_Object);
        if (!isExit)
        {
            OpenPopup(DisconnectPopup_Object);
        }
        //}
    }

    internal void ADfunction()
    {
        OpenPopup(ADPopup_Object);
    }

    private void CallOnExitFunction()
    {
        isExit = true;
        print("close");
        slotBehaviour.CallCloseSocket();
        Application.ExternalCall("window.parent.postMessage", "onExit", "*");
    }

    internal void PopulateWin(int value, double amount)
    {
        switch (value)
        {
            case 1:
                if (Win_Image) Win_Image.sprite = BigWin_Sprite;
                break;
            case 2:
                if (Win_Image) Win_Image.sprite = HugeWin_Sprite;
                break;
            case 3:
                if (Win_Image) Win_Image.sprite = MegaWin_Sprite;
                break;

        }
        if (value == 4)
            StartPopupAnim(amount, true);
        else
            StartPopupAnim(amount, false);

    }

    private void StartPopupAnim(double amount, bool jackpot = false)
    {
        int initAmount = 0;
        if (jackpot)
        {
            if (jackpot_Object) jackpot_Object.SetActive(true);
        }
        else
        {
            if (WinPopup_Object) WinPopup_Object.SetActive(true);

        }

        if (MainPopup_Object) MainPopup_Object.SetActive(true);

        DOTween.To(() => initAmount, (val) => initAmount = val, (int)amount, 5f).OnUpdate(() =>
        {
            if (jackpot)
            {
                if (jackpot_Text) jackpot_Text.text = initAmount.ToString();

            }
            else
            {

                if (Win_Text) Win_Text.text = initAmount.ToString();

            }
        });

        DOVirtual.DelayedCall(6f, () =>
        {
            if (jackpot)
            {
                // if (jackpot_Object) jackpot_Object.SetActive(true);
                ClosePopup(jackpot_Object);
            }
            else
            {
                // if (WinPopup_Object) WinPopup_Object.SetActive(false);
                ClosePopup(WinPopup_Object);

            }
            // if (MainPopup_Object) MainPopup_Object.SetActive(false);
            slotBehaviour.CheckPopups = false;
        });
    }
    void OnMenuClick()
    {
        if (audioController) audioController.PlayButtonAudio();
        isOpen = !isOpen;

        if (isOpen)
        {
            if (Menu_button) Menu_button.image.sprite = MenuCloseSprite;
            for (int i = 0; i < Menu_button_grp.childCount - 1; i++)
            {
                Menu_button_grp.GetChild(i).DOLocalMoveY(-200 * (i + 1), 0.1f * (i + 1));
            }
        }
        else
        {

            if (Menu_button) Menu_button.image.sprite = MenuOpenSprite;

            for (int i = 0; i < Menu_button_grp.childCount - 1; i++)
            {
                Menu_button_grp.GetChild(i).DOLocalMoveY(0, 0.1f * (i + 1));
            }
        }


    }
    private void OpenPopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
        if (Popup) Popup.SetActive(true);
    }

    private void ClosePopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(false);
        if(!DisconnectPopup_Object.activeSelf)
        if (MainPopup_Object) MainPopup_Object.SetActive(false);
    }

    private void ToggleMusic()
    {
        float value = MusicSlider.value;
        audioController.ToggleMute(value, "bg");

    }

    private void ToggleSound()
    {

        float value = SoundSlider.value;
        if (audioController) audioController.ToggleMute(value, "button");
        if (audioController) audioController.ToggleMute(value, "wl");


    }
}

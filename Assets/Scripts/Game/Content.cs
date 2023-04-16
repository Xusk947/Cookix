using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class Content
{
    private static Content _CONTENT;
    public static Content Instance
    {
        get
        {
            if (_CONTENT != null) return _CONTENT;
            _CONTENT = new Content();
            _CONTENT.Load();
            return _CONTENT;
        }
        private set
        {
            _CONTENT = value;
        }
    }
    // Materials
    public Material Burnt;
    // GUI for FoodItems
    // Food Zone
    [HideInInspector]
    public GameObject FoodItemUI;
    [HideInInspector]
    public GameObject FoodItemGridUI;
    [HideInInspector]
    public FoodTaskImage FoodTaskUI;
    [HideInInspector]
    public GameObject Icon;
    // Blocks UI
    [HideInInspector]
    public GameObject CookProgressBar;
    // Icons 
    public IconHide IconCancel, IconAccept;
    public Animator WarningIcon;
    // Sprites
    [HideInInspector]
    public Sprite Pixel1x1;
    // Person Prefab
    public ClientController Person;
    // Game Settings
    public Settings UISettings;
    // VFX 
    public AudioMixer AudioMixerMain, AudioMixerFX;
    public AudioClip VFX_UISelect;
    public List<AudioClip> VFX_Slice;
    public List<AudioClip> VFX_PlayerSelect;
    public AudioClip VFX_Fry;
    public AudioClip VFX_ReceptionBell;
    public AudioClip VFX_TimesUp;
    public AudioClip VFX_Cashsound;

    public AudioClip MUSIC_LevelFinished;
    public void Load()
    {
        Burnt = Resources.Load<Material>("Models/Items/Food/Material/Burnt");

        FoodItemUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemUI");
        FoodItemGridUI = Resources.Load<GameObject>("Models/Prefabs/UI/FoodItemGridUI");
        FoodTaskUI = Resources.Load<FoodTaskImage>("Models/Prefabs/UI/FoodTaskUI");
        Icon = Resources.Load<GameObject>("Models/Prefabs/UI/IconImage");

        CookProgressBar = Resources.Load<GameObject>("Models/Prefabs/UI/CookProgressBar");

        Pixel1x1 = Resources.Load<Sprite>("Sprites/UI/pixel1x1");

        IconCancel = Resources.Load<IconHide>("Models/Prefabs/UI/CancelIcon");
        IconAccept = Resources.Load<IconHide>("Models/Prefabs/UI/AcceptIcon");
        WarningIcon = Resources.Load<Animator>("Models/Prefabs/UI/WarningIcon");

        Person = Resources.Load<ClientController>("Models/Prefabs/Persons/person-walker");
        /// VFX
        AudioMixerMain = Resources.Load<AudioMixer>("VFX/AudioMixers/Main");
        AudioMixerFX = Resources.Load<AudioMixer>("VFX/AudioMixers/FX");

        VFX_UISelect = Resources.Load<AudioClip>("VFX/select-ui");
        VFX_Fry = Resources.Load<AudioClip>("VFX/fry");
        VFX_ReceptionBell = Resources.Load<AudioClip>("VFX/reception-bell");
        VFX_Cashsound = Resources.Load<AudioClip>("VFX/cash-sound");

        VFX_Slice = new List<AudioClip>()
        {
            Resources.Load<AudioClip>("VFX/slice-1"),
            Resources.Load<AudioClip>("VFX/slice-2"),
            Resources.Load<AudioClip>("VFX/slice-3")
        };

        VFX_PlayerSelect = new List<AudioClip>() {
            Resources.Load<AudioClip>("VFX/item-select-1"),
            Resources.Load<AudioClip>("VFX/item-select-2"),
            Resources.Load<AudioClip>("VFX/item-select-3"),
            Resources.Load<AudioClip>("VFX/item-select-4")
        };

        MUSIC_LevelFinished = Resources.Load<AudioClip>("Music/ragtime-logo-standard-version");
    }
}

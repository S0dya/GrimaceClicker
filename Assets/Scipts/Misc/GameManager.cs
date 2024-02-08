using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static LeanTween;

public class GameManager : SingletonMonobehaviour<GameManager>, ISaveable
{
    public Color bonusColor;
    [SerializeField] Color standardColor;

    TextMeshProUGUI scoreText;
    TextMeshProUGUI multiplayerText;
    
    GameObject bonusPanel;
    CanvasGroup bonusCanvasGroup;
    TextMeshProUGUI bonusCoctailTextBonusX2;
    TextMeshProUGUI bonusCoctailTextTimer;

    GameObject updgradeTab;
    GameObject statsTab;
    GameObject settingsTab;
    GameObject newGameTab;

    Coroutine bonusCor;

    bool inGame;
    bool inUpgrade;
    bool inPassiveUpgrade;
    bool inStats;
    bool inSettings;
    bool inNewGame;

    GameObjectSave _gameObjectSave;
    public GameObjectSave GameObjectSave { get { return _gameObjectSave; } set { _gameObjectSave = value; } }

    protected override void Awake()
    {
        base.Awake();
        GameObjectSave = new GameObjectSave();

        scoreText = GetActiveObj("ScoreText").GetComponent<TextMeshProUGUI>();
        multiplayerText = GetActiveObj("MultiplayerText").GetComponent<TextMeshProUGUI>();
        bonusPanel = GetActiveObj("BonusText");
        bonusCanvasGroup = bonusPanel.GetComponent<CanvasGroup>();
        bonusCoctailTextBonusX2 = bonusPanel.GetComponent<TextMeshProUGUI>();
        bonusCoctailTextTimer = bonusPanel.GetComponentInChildren<TextMeshProUGUI>();

        updgradeTab = GetActiveObj("UpgradeTab");
        statsTab = GetActiveObj("StatsTab");
        settingsTab = GetActiveObj("SettingsTab");
        newGameTab = GetActiveObj("NewGameTab");

        bonusCoctailTextTimer.color = bonusColor;
        bonusCoctailTextBonusX2.color = bonusColor;

        bonusPanel.SetActive(false);

        updgradeTab.SetActive(false);
        statsTab.SetActive(false);
        settingsTab.SetActive(false);
        newGameTab.SetActive(false);
    }
    GameObject GetActiveObj(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

        foreach (var obj in objs) if (obj.activeSelf) return obj;
        return null;
    }


    void Start()
    {
        SaveManager.I.SaveDataToFile();
        SaveManager.I.LoadDataFromFile();



        if (Settings.upgradeMultiplayerPerSec != 0)
        {
            UpdateMultiplayer();
        }
        ToggleNewGame(false);
        //ToggleBonus(true);
        inGame = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inNewGame)
            {
                ToggleNewGame(false);
            }
            else if (inGame)
            {
                Application.Quit();
            }
            else
            {
                ClosePrevTabs();
            }
        }
    }

    void FixedUpdate()
    {
        Settings.scoreVal += Settings.upgradeMultiplayer;
        Settings.totalAmount += Settings.upgradeMultiplayer;
        UpdateScore();
    }

    //buttons
    public void onGameTabButton()
    {
        ClosePrevTabs();
        inGame = true;
    }

    public void onUpgradeTabButton()
    {
        UpgradePanel.I.UpdateUpgrade();
        ClosePrevTabs();
        inUpgrade = true;
        updgradeTab.SetActive(true);
    }

    public void onStatsTabButton()
    {
        ClosePrevTabs();
        StatsPanel.I.UpdatePassiveUps();
        inStats = true;
        statsTab.SetActive(true);
    }

    public void onSettingsTabButton()
    {
        ClosePrevTabs();
        inSettings = true;
        settingsTab.SetActive(true);
    }

    public void OnButtonPress()
    {
        AudioManager.I.PlayOneShot("ButtonPress");
    }
    public void OnButtonPressDownBar()
    {
        AudioManager.I.PlayOneShot("ButtonPressDownBar");
    }

    //newGameButtons
    public void OnNewGameConfirmButton()
    {
        Settings.Clear();
        UpgradePanel.I.CLear();
        SettingsPanel.I.Start();
        SaveManager.I.SaveDataToFile();
        multiplayerText.text = "";

        ToggleNewGame(false);
    }
    public void OnNewGameCloseButton()
    {
        ToggleNewGame(false);
    }


    //gameMethods
    public void UpdateScore()
    {
        scoreText.text = Settings.Format(Settings.scoreVal);

        if (inUpgrade)
        {
            UpgradePanel.I.UpdateUpgrade();
        }
        else if (inStats)
        {
            StatsPanel.I.UpdateStats();
        }
    }
    public void UpdateMultiplayer()
    {
        multiplayerText.text = Settings.FormatForMultiplayer(Settings.upgradeMultiplayerPerSec) + "/s";
    }

    void ClosePrevTabs()
    {
        if (inUpgrade)
        {
            InfoPanel.I.ToggleInfo(false);
            updgradeTab.SetActive(false);
            inUpgrade = false;
            InfoPanelPassive.I.ToggleInfo(false);
        }
        else if (inStats)
        {
            statsTab.SetActive(false);
            inStats = false;
            StatsPanel.I.TogglePassiveTab(false);
        }
        else if (inSettings)
        {
            settingsTab.SetActive(false);
            inSettings = false;
        }
        inGame = false;
    }

    public void ToggleBonus(bool val)
    {
        Settings.isBonusOn = val;
        Settings.clickMultiplayer *= (val ? 2 : 0.5f);
        Settings.upgradeMultiplayer *= (val ? 2 : 0.5f);
        Settings.upgradeMultiplayerPerSec *= (val ? 2 : 0.5f);
        UpdateMultiplayer();

        if (bonusCor != null)
        {
            StopCoroutine(bonusCor);
        }
        
        if (val)
        {
            scoreText.color = bonusColor;
            multiplayerText.color = bonusColor;
            StartBlinking();
            bonusCor = StartCoroutine(BonusCor());
        }
        else
        {
            scoreText.color = standardColor;
            multiplayerText.color = standardColor;
            StopBlinking();
            BonusButton.I.SetNextBonus();
        }
        
        bonusPanel.SetActive(val);
    }
    IEnumerator BonusCor()
    {
        int duration = Random.Range(65, 90);
        bonusCoctailTextTimer.text = FormatToTimer(duration);
        while (duration > 0)
        {
            yield return new WaitForSeconds(0.25f);

            duration--;
            bonusCoctailTextTimer.text = FormatToTimer(duration);
        }

        ToggleBonus(false);
    }
    string FormatToTimer(int duration)
    {
        int sec = duration % 60;
        return  string.Format($"{duration / 60}:{(sec < 10 ? "0" : "")}{sec}");
    }
    void StartBlinking()
    {
        LeanTween.value(bonusPanel, 0.1f, 1f, 0.2f).setLoopPingPong().setOnUpdate(UpdateAlpha);
    }
    void UpdateAlpha(float alpha)
    {
        bonusCanvasGroup.alpha = alpha;
    }
    void StopBlinking()
    {
        LeanTween.cancel(bonusPanel);
        bonusCanvasGroup.alpha = 1f;
    }


    //newGameMethods
    public void ToggleNewGame(bool val)
    {
        newGameTab.SetActive(val);
        inNewGame = val;
        if (val)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 0.25f;
        }
    }


    //gameManagerMethods
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveManager.I.SaveDataToFile();
        }
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Settings.lastExitTime = System.DateTimeOffset.Now.ToUnixTimeSeconds();
            SaveManager.I.SaveDataToFile();
        }
        else
        {
            long last = Settings.lastExitTime;
            if (last != 0)
            {
                long cur = System.DateTimeOffset.Now.ToUnixTimeSeconds();
                Settings.scoreVal += (cur - last) * Settings.upgradeMultiplayerPerSec;
            }
        }
    }
    void OnApplicationQuit()
    {
        SaveManager.I.SaveDataToFile();
    }




    //save
    void OnEnable()
    {
        ISaveableRegister();
    }
    void OnDisable()
    {
        ISaveableDeregister();
    }

    public void ISaveableRegister()
    {
        SaveManager.I.iSaveableObjectList.Add(this);
    }

    public void ISaveableDeregister()
    {
        SaveManager.I.iSaveableObjectList.Remove(this);
    }

    public GameObjectSave ISaveableSave()
    {
        GameObjectSave.sceneData.Remove(Settings.GameScene);

        SceneSave sceneSave = new SceneSave();

        sceneSave.floatDictionary = new Dictionary<string, float>();
        sceneSave.floatArrayDictionary = new Dictionary<string, float[]>();
        sceneSave.intDictionary = new Dictionary<string, int>();
        sceneSave.boolArrayDictionary = new Dictionary<string, bool[]>();
        sceneSave.boolDictionary = new Dictionary<string, bool>();
        sceneSave.longDictionary = new Dictionary<string, long>();

        //game
        sceneSave.floatDictionary.Add("scoreVal", Settings.scoreVal);
        sceneSave.floatDictionary.Add("clickMultiplayer", Settings.clickMultiplayer);
        sceneSave.floatDictionary.Add("upgradeMultiplayer", Settings.upgradeMultiplayer);
        sceneSave.floatDictionary.Add("upgradeMultiplayerPerSec", Settings.upgradeMultiplayerPerSec);

        //ups/info
        sceneSave.floatArrayDictionary.Add("upgradeCost", Settings.upgradeCost);
        sceneSave.floatArrayDictionary.Add("upgradePerSecond", Settings.upgradePerSecond);

        sceneSave.floatArrayDictionary.Add("upgradeInfoAmount", Settings.upgradeInfoAmount);
        sceneSave.floatArrayDictionary.Add("upgradeInfoSoFar", Settings.upgradeInfoSoFar);

        sceneSave.floatArrayDictionary.Add("upgradesMultiplayers", Settings.upgradesMultiplayers);

        //Stats
        sceneSave.floatDictionary.Add("totalAmount", Settings.totalAmount);
        sceneSave.intDictionary.Add("totalClicks", Settings.totalClicks);
        sceneSave.floatDictionary.Add("totalAmountOfActiveUpgrades", Settings.totalAmountOfActiveUpgrades);
        sceneSave.floatDictionary.Add("totalAmountOfPassiveUpgrades", Settings.totalAmountOfPassiveUpgrades);
        sceneSave.floatDictionary.Add("totalAmountOfWatchedAdx10", Settings.totalAmountOfWatchedAdx10);
        sceneSave.floatDictionary.Add("totalAmountOfWatchedAdx2", Settings.totalAmountOfWatchedAdx2);

        sceneSave.boolArrayDictionary.Add("passiveUpsUnlocked", Settings.passiveUpsUnlocked);
        sceneSave.boolArrayDictionary.Add("openedActiveUps", Settings.openedActiveUps);
        sceneSave.boolArrayDictionary.Add("openedPassiveUps", Settings.openedPassiveUps);
        sceneSave.boolArrayDictionary.Add("boughtPassiveUps", Settings.boughtPassiveUps);

        //settings
        sceneSave.boolDictionary.Add("isMusicOn", Settings.isMusicOn);
        sceneSave.boolDictionary.Add("isParticlesOn", Settings.isParticlesOn);

        sceneSave.longDictionary.Add("lastExitTime", Settings.lastExitTime);

        GameObjectSave.sceneData.Add(Settings.GameScene, sceneSave);
        return GameObjectSave;
    }


    public void ISaveableLoad(GameObjectSave gameObjectSave)
    {
        if (gameObjectSave.sceneData.TryGetValue(Settings.GameScene, out SceneSave sceneSave))
        {
            if (sceneSave.floatDictionary != null)
            {
                if (sceneSave.floatDictionary.TryGetValue("scoreVal", out float scoreVal))
                {
                    Settings.scoreVal = scoreVal;
                }
                if (sceneSave.floatDictionary.TryGetValue("clickMultiplayer", out float clickMultiplayer))
                {
                    Settings.clickMultiplayer = clickMultiplayer;
                }
                if (sceneSave.floatDictionary.TryGetValue("upgradeMultiplayer", out float upgradeMultiplayer))
                {
                    Settings.upgradeMultiplayer = upgradeMultiplayer;
                }
                if (sceneSave.floatDictionary.TryGetValue("upgradeMultiplayerPerSec", out float upgradeMultiplayerPerSec))
                {
                    Settings.upgradeMultiplayerPerSec = upgradeMultiplayerPerSec;
                }

                //stats
                if (sceneSave.floatDictionary.TryGetValue("totalAmount", out float totalAmount))
                {
                    Settings.totalAmount = totalAmount;
                }
                if (sceneSave.floatDictionary.TryGetValue("totalAmountOfActiveUpgrades", out float totalAmountOfActiveUpgrades))
                {
                    Settings.totalAmountOfActiveUpgrades = totalAmountOfActiveUpgrades;
                }
                if (sceneSave.floatDictionary.TryGetValue("totalAmountOfPassiveUpgrades", out float totalAmountOfPassiveUpgrades))
                {
                    Settings.totalAmountOfPassiveUpgrades = totalAmountOfPassiveUpgrades;
                }
                if (sceneSave.floatDictionary.TryGetValue("totalAmountOfWatchedAdx10", out float totalAmountOfWatchedAdx10))
                {
                    Settings.totalAmountOfWatchedAdx10 = totalAmountOfWatchedAdx10;
                }
                if (sceneSave.floatDictionary.TryGetValue("totalAmountOfWatchedAdx2", out float totalAmountOfWatchedAdx2))
                {
                    Settings.totalAmountOfWatchedAdx2 = totalAmountOfWatchedAdx2;
                }
            }
            if (sceneSave.floatArrayDictionary != null)
            {
                if (sceneSave.floatArrayDictionary.TryGetValue("upgradeCost", out float[] upgradeCost))
                {
                    Settings.upgradeCost = upgradeCost;
                }
                if (sceneSave.floatArrayDictionary.TryGetValue("upgradePerSecond", out float[] upgradePerSecond))
                {
                    Settings.upgradePerSecond = upgradePerSecond;
                }
                if (sceneSave.floatArrayDictionary.TryGetValue("upgradeInfoAmount", out float[] upgradeInfoAmount))
                {
                    Settings.upgradeInfoAmount = upgradeInfoAmount;
                }
                if (sceneSave.floatArrayDictionary.TryGetValue("upgradeInfoSoFar", out float[] upgradeInfoSoFar))
                {
                    Settings.upgradeInfoSoFar = upgradeInfoSoFar;
                }
                if (sceneSave.floatArrayDictionary.TryGetValue("upgradesMultiplayers", out float[] upgradesMultiplayers))
                {
                    Settings.upgradesMultiplayers = upgradesMultiplayers;
                }
            }
            if (sceneSave.intDictionary != null) 
            {
                if (sceneSave.intDictionary.TryGetValue("totalClicks", out int totalClicks))
                {
                    Settings.totalClicks = totalClicks;
                }
            }
            if (sceneSave.boolArrayDictionary != null)
            {
                if (sceneSave.boolArrayDictionary.TryGetValue("passiveUpsUnlocked", out bool[] passiveUpsUnlocked))
                {
                    Settings.passiveUpsUnlocked = passiveUpsUnlocked;
                }
                if (sceneSave.boolArrayDictionary.TryGetValue("openedActiveUps", out bool[] openedActiveUps))
                {
                    Settings.openedActiveUps = openedActiveUps;
                }
                if (sceneSave.boolArrayDictionary.TryGetValue("openedPassiveUps", out bool[] openedPassiveUps))
                {
                    Settings.openedPassiveUps = openedPassiveUps;
                }
                if (sceneSave.boolArrayDictionary.TryGetValue("boughtPassiveUps", out bool[] boughtPassiveUps))
                {
                    Settings.boughtPassiveUps = boughtPassiveUps;
                }

            }
            if (sceneSave.boolDictionary != null)
            {
                if (sceneSave.boolDictionary.TryGetValue("isMusicOn", out bool isMusicOn))
                {
                    Settings.isMusicOn = isMusicOn;
                }
                if (sceneSave.boolDictionary.TryGetValue("isParticlesOn", out bool isParticlesOn))
                {
                    Settings.isParticlesOn = isParticlesOn;
                }
            }
            if (sceneSave.longDictionary != null)
            {
                if (sceneSave.longDictionary.TryGetValue("lastExitTime", out long lastExitTime))
                {
                    Settings.lastExitTime = lastExitTime;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    [SerializeField]
    private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    [SerializeField]
    private UI_LevelPackList _levelPackList = null;

    void Awake() {
        if (!_playerProgress.MuatProgress()) {
            _playerProgress.SimpanProgress();
        }
    }
    
    void Start()
    {
        _levelPackList.LoadLevelPack(_levelPacks, _playerProgress.progressData);

        _tempatKoin.text = $"{_playerProgress.progressData.koin}";

        // Play BGM Menu
        AudioManager.instance.PlayBGM(0);
    }

}

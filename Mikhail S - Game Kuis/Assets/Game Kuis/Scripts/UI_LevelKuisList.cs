using System;
using UnityEngine;

public class UI_LevelKuisList : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private UI_OpsiLevelKuis _tombolLevelKuis;

    [SerializeField]
    private RectTransform _content = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _gameplayScene = null;

    private void Start() {
        // if (_levelPack != null) {
        //     UnloadLevelPack(_levelPack);
        // }

        // Subscribe Event
        UI_OpsiLevelKuis.EventSaatKlik += UI_OpsiLevelKuis_EventSaatKlik;
    }

    private void OnDestroy() {
        // Unsubscribe Event
        UI_OpsiLevelKuis.EventSaatKlik -= UI_OpsiLevelKuis_EventSaatKlik;
    }

    private void UI_OpsiLevelKuis_EventSaatKlik(int index)
    {
        _inisialData.levelIndex = index;
        _gameSceneManager.BukaScene(_gameplayScene);
    }

    public void UnloadLevelPack(LevelPackKuis levelPack) {
        HapusIsiKonten();

        _levelPack = levelPack;

        for (int i = 0; i < levelPack.BanyakLevel; i++) {
            // Membuat copy object dari prefab tombol Level Pack
            var lp = Instantiate(_tombolLevelKuis);

            lp.SetLevelKuis(levelPack.AmbilLevelKe(i), i);

            // Masukkan object tombol sebagai child dari object "content"
            lp.transform.SetParent(_content);

            lp.transform.localScale = Vector3.one;

        }
    }

    private void HapusIsiKonten() {
        var cc = _content.childCount;

        for (int i = 0; i < cc; i++) {
            Destroy(_content.GetChild(i).gameObject);
        }
    }

}

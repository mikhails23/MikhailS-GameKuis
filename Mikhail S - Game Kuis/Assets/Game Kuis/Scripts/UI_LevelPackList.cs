using System;
using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private UI_LevelKuisList _levelList = null;

    [SerializeField]
    private UI_OpsiLevelPack _tombolLevelPack;

    [SerializeField]
    private RectTransform _content = null;

    [Space, SerializeField]
    private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    // Start is called before the first frame update
    void Start()
    {
        LoadLevelPack();

        if (_inisialData.SaatKalah) {
            UI_OpsiLevelPack_EventSaatKlik(_inisialData.levelPack);
        }

        // Subscribe Event
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    private void OnDestroy() {
        // Unsubscribe Event
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }

    private void UI_OpsiLevelPack_EventSaatKlik(LevelPackKuis levelPack)
    {
        // Buka Menu Levels
        _levelList.gameObject.SetActive(true);
        _levelList.UnloadLevelPack(levelPack);

        // Tutup menu Level Packs
        gameObject.SetActive(false);

        _inisialData.levelPack = levelPack;
    }

    private void LoadLevelPack() {
        foreach (var lp in _levelPacks) {
            // Membuat copy object dari prefab tombol Level Pack
            var tlp = Instantiate(_tombolLevelPack);

            tlp.SetLevelPack(lp);

            // Masukkan object tombol sebagai child dari object "content"
            tlp.transform.SetParent(_content);

            tlp.transform.localScale = Vector3.one;

        }
    }

}

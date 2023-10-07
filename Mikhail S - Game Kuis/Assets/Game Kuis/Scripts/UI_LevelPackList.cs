using System;
using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private UI_LevelKuisList _levelList = null;

    [SerializeField]
    private UI_OpsiLevelPack _tombolLevelPack;

    [SerializeField]
    private RectTransform _content = null;

    // Start is called before the first frame update
    void Start()
    {
        // LoadLevelPack();

        if (_inisialData.SaatKalah) {
            UI_OpsiLevelPack_EventSaatKlik(null, _inisialData.levelPack, false);
        }

        // Subscribe Event
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    private void OnDestroy() {
        // Unsubscribe Event
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }

    private void UI_OpsiLevelPack_EventSaatKlik(UI_OpsiLevelPack tombolLevelPack, LevelPackKuis levelPack, bool terkunci)
    {
        // Jika terkunci jangan pindah ke menu Levels
        if (terkunci) {
            return;
        }

        // Buka Menu Levels
        // _levelList.gameObject.SetActive(true);
        _levelList.UnloadLevelPack(levelPack);

        // Tutup menu Level Packs
        // gameObject.SetActive(false);

        _inisialData.levelPack = levelPack;

        _animator.SetTrigger("KeLevels");
    }

    public void LoadLevelPack(LevelPackKuis[] _levelPacks, PlayerProgress.MainData playerData) {
        foreach (var lp in _levelPacks) {
            // Membuat copy object dari prefab tombol Level Pack
            var tlp = Instantiate(_tombolLevelPack);

            tlp.SetLevelPack(lp);

            // Masukkan object tombol sebagai child dari object "content"
            tlp.transform.SetParent(_content);

            tlp.transform.localScale = Vector3.one;

            // Apakah Level Pack terdaftar di Dictionary progress pemain
            if (!playerData.progressLevel.ContainsKey(lp.name)) {
                // Jika tidak terdaftar maka Level Pack terkunci
                tlp.KunciLevelPack();
            }

        }
    }

}

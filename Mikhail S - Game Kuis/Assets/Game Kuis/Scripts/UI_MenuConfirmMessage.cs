using TMPro;
using UnityEngine;

public class UI_MenuConfirmMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private GameObject _pesanCukupKoin = null;

    [SerializeField]
    private GameObject _pesanTakCukupKoin = null;

    private UI_OpsiLevelPack _tombolLevelPack = null; 
    private LevelPackKuis _levelPackKuis = null; 

    void Start()
    {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }

        // Subscribe Event
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    private void OnDestroy() {
        // Unsubscribe Event
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }

    private void UI_OpsiLevelPack_EventSaatKlik(UI_OpsiLevelPack tombolLevelPack, 
                                                LevelPackKuis levelPackKuis, 
                                                bool terkunci) {
        // Jika sudah tidak terkunci maka abaikan
        if (!terkunci) {
            return;
        }

        // Aktifkan Menu Konfirmasi
        gameObject.SetActive(true);

        // Cek apakah Koin cukup untuk membeli Level Pack
        // Jika Koin tidak cukup
        if (_playerProgress.progressData.koin < levelPackKuis.Harga) {
            // Tampilkan Pesan Tidak Cukup dan keluar
            _pesanCukupKoin.SetActive(false);
            _pesanTakCukupKoin.SetActive(true);
            return;
        }

        // Jika Koin cukup, tampilkan pesan konfirmasi
        _pesanCukupKoin.SetActive(true);
        _pesanTakCukupKoin.SetActive(false);

        _tombolLevelPack = tombolLevelPack;
        _levelPackKuis = levelPackKuis;

    }

    public void BukaLevel() {
        _playerProgress.progressData.koin -= _levelPackKuis.Harga;
        _playerProgress.progressData.progressLevel[_levelPackKuis.name] = 1;
        _playerProgress.SimpanProgress();

        _tempatKoin.text = $"{_playerProgress.progressData.koin}";

        _tombolLevelPack.BukaLevelPack();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    public static event System.Action EventWaktuHabis;

    // [SerializeField]
    // private UI_PesanLevel _tempatPesan = null;
    [SerializeField]
    private Slider _timeBar = null;
    [SerializeField]
    private float _waktuJawab = 30f;

    private float _sisaWaktu = 0f; // Data Sementara
    private bool _waktuBerjalan = true;

    public bool WaktuBerjalan {
        get => _waktuBerjalan;
        set => _waktuBerjalan = value;
    }

    public void UlangWaktu() {
        _sisaWaktu = _waktuJawab;
    }
    // Start is called before the first frame update
    void Start()
    {
        UlangWaktu();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_waktuBerjalan) {
            return;
        }

        _sisaWaktu -= Time.deltaTime;
        _timeBar.value = _sisaWaktu / _waktuJawab;

        if (_sisaWaktu <= 0f) {
            // _tempatPesan.Pesan = "Waktu sudah habis";
            // _tempatPesan.gameObject.SetActive(true);

            EventWaktuHabis?.Invoke();

            // Debug.Log("Waktu Habis");
            _waktuBerjalan = false;
            return;
        }

        // Debug.Log("Sisa waktu: " + _sisaWaktu);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class UI_PesanLevel : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private GameObject _opsiMenang = null;

    [SerializeField]
    private GameObject _opsiKalah = null;

    [SerializeField]
    private TextMeshProUGUI _tempatPesan;

    public string Pesan {
        get => _tempatPesan.text;
        set => _tempatPesan.text = value;
    }

    private void Awake() {

        // Mematikan halaman pesan ketika game dimulai
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }

        // Subscribe Event
        UI_Timer.EventWaktuHabis += UI_Timer_EventWaktuHabis;
        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;
    }

    private void OnDestroy() {
        // Unsubscribe Event
        UI_Timer.EventWaktuHabis -= UI_Timer_EventWaktuHabis;
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }

    private void UI_PoinJawaban_EventJawabSoal(string jawabanTeks, bool adalahBenar)
    {
        Pesan = $"Jawaban Anda {adalahBenar} (Jawab: {jawabanTeks})";
        gameObject.SetActive(true);

        if (adalahBenar) {
            _opsiMenang.SetActive(true);
            _opsiKalah.SetActive(false);
        } else {
            _opsiMenang.SetActive(false);
            _opsiKalah.SetActive(true);
        }

        _animator.SetBool("Menang", adalahBenar);

    }

    private void UI_Timer_EventWaktuHabis()
    {
        Pesan = "Waktu sudah habis!";
        gameObject.SetActive(true);
        
        _opsiMenang.SetActive(false);
        _opsiKalah.SetActive(true);

        _animator.SetBool("Menang", false);
    }
}

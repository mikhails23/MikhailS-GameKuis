using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class UI_PesanLevel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatPesan;

    public string Pesan {
        get => _tempatPesan.text;
        set => _tempatPesan.text = value;
    }

    void Awake() {

        // Mematikan halaman pesan ketika game dimulai
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{


    [System.Serializable]
    public struct DataSoal {
        public string pertanyaan;
        public Sprite petunjukJawaban;

        public string[] pilihanJawaban;
        public bool[] adalahBenar;

    }

    [SerializeField]
    private DataSoal[] _soalSoal = new DataSoal[0];

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;

    [SerializeField]
    private UI_PoinJawaban[] _pilihanJawaban = new UI_PoinJawaban[0];
    private int _indexSoal = -1;

    public void NextLevel() {
        // Soal index selanjutnya
        _indexSoal++;

        // Jika index lebih dari soal terakhir, reset ke awal
        if (_indexSoal >= _soalSoal.Length) {
            _indexSoal = 0;
        }

        // Get Soal menurut index
        DataSoal soal = _soalSoal[_indexSoal];

        // Set informasi pertanyaan
        _pertanyaan.SetPertanyaan($"Soal {_indexSoal + 1}", soal.pertanyaan, soal.petunjukJawaban);

        // Set informasi jawaban
        for (int i = 0; i < _pilihanJawaban.Length; i++) {
            UI_PoinJawaban poinJawaban = _pilihanJawaban[i];
            poinJawaban.SetJawaban(soal.pilihanJawaban[i], soal.adalahBenar[i]);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        NextLevel();
    }

}

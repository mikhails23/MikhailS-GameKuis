using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private LevelPackKuis _soalSoal = null;

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;

    [SerializeField]
    private UI_PoinJawaban[] _pilihanJawaban = new UI_PoinJawaban[0];
    private int _indexSoal = -1;

    public void NextLevel() {
        // Soal index selanjutnya
        _indexSoal++;

        // Jika index lebih dari soal terakhir, reset ke awal
        if (_indexSoal >= _soalSoal.BanyakLevel) {
            _indexSoal = 0;
        }

        // Get Soal menurut index
        LevelSoalKuis soal = _soalSoal.AmbilLevelKe(_indexSoal);

        // Set informasi pertanyaan
        _pertanyaan.SetPertanyaan($"Soal {_indexSoal + 1}", soal.pertanyaan, soal.petunjukJawaban);

        // Set informasi jawaban
        for (int i = 0; i < _pilihanJawaban.Length; i++) {
            UI_PoinJawaban poinJawaban = _pilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            poinJawaban.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if (!_playerProgress.MuatProgress()) {
            _playerProgress.SimpanProgress();
        }

        NextLevel();
    }

}

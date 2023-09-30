using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpsiLevelPack : MonoBehaviour
{
    public static event System.Action<LevelPackKuis> EventSaatKlik;

    [SerializeField]
    private TextMeshProUGUI _packName = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

    [SerializeField]
    private Button _tombol = null;

    // Start is called before the first frame update
    void Start()
    {
        if (_levelPack != null) {
            SetLevelPack(_levelPack);
        }

        // Subscribe Event
        _tombol.onClick.AddListener(SaatKlik);
    }

    private void OnDestroy() {
        // Unsubscribe Event
        _tombol.onClick.RemoveListener(SaatKlik);
    }
 
    public void SetLevelPack(LevelPackKuis levelPack) {
        _packName.text = levelPack.name;
        _levelPack = levelPack;
    }

    private void SaatKlik() {
        // Debug.Log("KLIK!");
        EventSaatKlik?.Invoke(_levelPack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

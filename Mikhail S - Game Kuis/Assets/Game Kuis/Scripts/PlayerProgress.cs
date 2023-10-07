using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[CreateAssetMenu(
    fileName = "Player Progress",
    menuName = "Game Kuis/Player Progress"
)]
public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData {
        public int koin;
        public Dictionary<string, int> progressLevel;
    }

    [SerializeField]
    private string _fileName = "contoh.txt";

    [SerializeField]
    private string _startingLevelPackName = string.Empty;

    public MainData progressData = new MainData();

    public void SimpanProgress() {
        // Contoh data
        // progressData.koin = 200;
        // if (progressData.progressLevel == null) {
        //     progressData.progressLevel = new();
        // }
        // progressData.progressLevel.Add("Level Pack 1", 3);
        // progressData.progressLevel.Add("Level Pack 3", 5);

        if (progressData.progressLevel == null) {
            progressData.progressLevel = new();
            progressData.koin = 0;
            progressData.progressLevel.Add(_startingLevelPackName, 1);
        }

        // Info penyimpanan data
        #if UNITY_EDITOR
        string directory = Application.dataPath + "/Temporary";
        #elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.persistentDataPath + "/ProgressLokal";
        #endif

        var path = directory + "/" + _fileName;

        // Membuat Directory baru
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory created: " + directory);
        }

        // Membuat File baru
        if (!File.Exists(path)) {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        // var konten = $"{progressData.koin}\n";

        // Simpan data ke file dengan Binary Formatter
        var fileStream = File.Open(path, FileMode.Open);
        // var formatter = new BinaryFormatter();

        fileStream.Flush();
        // formatter.Serialize(fileStream, progressData);

        // Simpan data dalam bentuk Binary
        var writer = new BinaryWriter(fileStream);

        writer.Write(progressData.koin);
        foreach (var i in progressData.progressLevel) {
            writer.Write(i.Key);
            writer.Write(i.Value);
        }

        // // Putuskan aliran memori dengan File
        // writer.Dispose();
        fileStream.Dispose();


        // foreach (var i in progressData.progressLevel) {
        //     konten += $"{i.Key} {i.Value}\n";
        // }

        // File.WriteAllText(path, konten);

        Debug.Log($"{_fileName} Berhasil Disimpan");
    }

    public bool MuatProgress() {
        // Info penyimpanan data
        #if UNITY_EDITOR
        string directory = Application.dataPath + "/Temporary";
        #elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.persistentDataPath + "/ProgressLokal";
        #endif

        var path = directory + "/" + _fileName;

        // Membuat Directory baru
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory created: " + directory);
        }

        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try {
            var reader = new BinaryReader(fileStream);

            try {
                progressData.koin = reader.ReadInt32();
                if (progressData.progressLevel == null) {
                    progressData.progressLevel = new();
                }

                while (reader.PeekChar() != -1) {
                    var namaLevelPack = reader.ReadString();
                    var levelKe = reader.ReadInt32();
                    progressData.progressLevel.Add(namaLevelPack, levelKe);
                    Debug.Log($"Load {namaLevelPack}, Level ke-{levelKe}");
                }

                // Putus aliran memori dengan File
                reader.Dispose();
            } catch (System.Exception e) {
                // Putus aliran memori dengan File
                reader.Dispose();
                fileStream.Dispose();

                Debug.Log($"ERROR: Terjadi kesalahan ketika memuat progress\n{e.Message}");
            }

            // // Muat data ke file dengan Binary Formatter           
            // var formatter = new BinaryFormatter();

            // progressData = (MainData)formatter.Deserialize(fileStream);

            // Putus aliran memori dengan File
            fileStream.Dispose();

            Debug.Log($"Load progress Koin: {progressData.koin}, Progress level: {progressData.progressLevel.Count}");

            return true;
        } catch (System.Exception e) {
            // Putus aliran memori dengan File
            fileStream.Dispose();

            Debug.Log($"ERROR: Terjadi kesalahan ketika memuat progress\n{e.Message}");

            return false;
        }

    }
}

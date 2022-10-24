using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class Configuration {

    public string optionsType = "Type 0";
}

public class OptionsMenu : MonoBehaviour {

    private Configuration configurationData;
    public TMP_Text optionsType;

    // Start is called before the first frame update
    void Start() {
        // optionsType.text = "Type 0";
        configurationData = LoadConfiguration();
        optionsType.text = configurationData.optionsType;
    }

    // Update is called once per frame
    void Update() {


    }

    public static void saveConfiguration(Configuration configuration) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/configuration.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, configuration);
        stream.Close();
    }

    public static Configuration LoadConfiguration() {
        string path = Application.persistentDataPath + "/configuration.fun";
        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(path)) {
            Debug.Log("Successfully load file found in " + path);
            
            FileStream stream = new FileStream(path, FileMode.Open);

            Configuration data = formatter.Deserialize(stream) as Configuration;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Configuration file not found in " + path);
            Debug.Log("Create Configuration.fun");

            FileStream stream = new FileStream(path, FileMode.Create);
            Configuration configuration = new Configuration();

            formatter.Serialize(stream, configuration);
            stream.Close();
            return configuration;
        }
    }

    public void TypeToggle() {

        switch (optionsType.text) {
            case "Type 0" :
                optionsType.text = "Type 1";
                break;
            case "Type 1" :
                optionsType.text = "Type 2";
                break;
            case "Type 2" :
                optionsType.text = "Type 0";
                break;
        }
    }

    public void Back() {
        configurationData.optionsType = optionsType.text;
        saveConfiguration(configurationData);
    }
}

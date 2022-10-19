using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public static class SaveSystem
{
    private static string directory = "/Data/";
    private static bool consoleLog = false;

    public static void InitializeDirectory()
    {
        string path = Application.persistentDataPath + directory;

        /* Create Save Directory if it does not already exist */
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            if (consoleLog)
            {
                Debug.Log("Creating directory: " + path);
            }
        }
    }

    public static bool SaveGenericData(object data, string fileName)
    {
        InitializeDirectory();
        string path = Application.persistentDataPath + directory;

        /* Create Data File */
        try
        {
            using (FileStream file = new FileStream(path + fileName, FileMode.Create))
            {
                if (consoleLog)
                {
                    Debug.Log("SaveSystem: Saving data: '" + fileName + "' at '" + path + "'");
                }

                BinaryFormatter bf = new BinaryFormatter();

                /* Serialization Successful */
                try
                {
                    bf.Serialize(file, data);

                    if (consoleLog)
                    {
                        Debug.Log("SaveSystem: Successfully saved data: '" + fileName + "' at '" + path + "'");
                    }

                    return true;
                }
                /* Serialization Failed */
                catch
                {
                    Debug.LogWarning("SaveSystem: File serialization failed.");
                    return false;
                }
            }
        }
        /* File Creation Exception */
        catch
        {
            Debug.LogWarning("SaveSystem: Failed to create file: '" + fileName + "' at '" + path + "'");
            return false;
        }
    }

    public static object LoadGenericData(string fileName, object defaultData)
    {
        InitializeDirectory();
        string path = Application.persistentDataPath + directory;

        /* If File Exists */
        if (File.Exists(path + fileName))
        {
            /* Load Data File */
            try
            {
                using (FileStream file = new FileStream(path + fileName, FileMode.Open))
                {
                    if (consoleLog)
                    {
                        Debug.Log("SaveSystem: Loading data: '" + fileName + "' at '" + path + "'");
                    }

                    BinaryFormatter bf = new BinaryFormatter();
                    object data;

                    /* Deserialization Successful */
                    try
                    {
                        data = bf.Deserialize(file);

                        if (consoleLog)
                        {
                            Debug.Log("SaveSystem: Successfully Loaded data: '" + fileName + "' at '" + path + "'");
                        }

                        return data;
                    }
                    /* Deserialization Failed */
                    catch
                    {
                        Debug.LogWarning("SaveSystem: File Deserialization failed.");
                        return null;
                    }
                }
            }
            /* File Load Exception */
            catch
            {
                Debug.LogWarning("SaveSystem: Failed to load file: '" + fileName + "' at '" + path + "'");
                return null;
            }
        }
        /* Data file missing */
        else
        {
            if (consoleLog)
            {
                Debug.Log("SaveSystem: Data file missing: '" + fileName + "' at '" + path + "'");
            }

            return CreateAndLoadDefaultGenericData(fileName, defaultData);
        }
    }

    public static object CreateAndLoadDefaultGenericData(string fileName, object defaultData)
    {
        InitializeDirectory();
        string path = Application.persistentDataPath + directory;

        /* Attempt to create and load Default data file */
        try
        {
            using (FileStream file = new FileStream(path + fileName, FileMode.Create))
            {
                if (consoleLog)
                {
                    Debug.Log("SaveSystem: Saving data: '" + fileName + "' at '" + path + "'");
                }

                BinaryFormatter bf = new BinaryFormatter();

                /* Serialization Successful */
                try
                {
                    bf.Serialize(file, defaultData);

                    if (consoleLog)
                    {
                        Debug.Log("SaveSystem: Successfully saved data: '" + fileName + "' at '" + path + "'");
                    }

                    return defaultData;
                }
                /* Serialization Failed */
                catch
                {
                    Debug.LogWarning("SaveSystem: File serialization failed.");
                    return null;
                }
            }
        }
        /* File Creation Exception */
        catch
        {
            Debug.LogWarning("SaveSystem: Failed to create file.");
            return null;
        }
    }
}
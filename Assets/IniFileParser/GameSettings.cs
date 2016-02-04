﻿using IniParser;
using IniParser.Model;
using System.IO;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static class Meshing
    {
        public static int meshingThreads = 2;
        public static int queueLimit = 4;
    }

    public static class Rendering
    {
        public static int drawRangeSide = 4;
        public static int drawRangeUp = 1;
        public static int drawRangeDown = 5;
    }

    public static class Units
    {
        public static bool drawUnits = true;
        public static bool scaleUnits = true;
    }

    static void DeserializeIni(string filename)
    {
        if (!File.Exists(filename))
            return;
        FileIniDataParser parser = new FileIniDataParser();
        IniData data = parser.ReadFile(filename);
        if(data.Sections.ContainsSection("Meshing"))
        {
            var meshingThreadsString = data["Meshing"]["meshingThreads"];
            if (meshingThreadsString != null)
                Meshing.meshingThreads = int.Parse(meshingThreadsString);
            var queueLimitString = data["Meshing"]["queueLimit"];
            if (queueLimitString != null)
                Meshing.queueLimit = int.Parse(queueLimitString);
        }
        if(data.Sections.ContainsSection("Rendering"))
        {
            string drawRangeSideString = data["Rendering"]["drawRangeSide"];
            if (drawRangeSideString != null)
                Rendering.drawRangeSide = int.Parse(drawRangeSideString);
            string drawRangeUpString = data["Rendering"]["drawRangeUp"];
            if (drawRangeUpString != null)
                Rendering.drawRangeUp = int.Parse(drawRangeUpString);
            string drawRangeDownString = data["Rendering"]["drawRangeDown"];
            if (drawRangeSideString != null)
                Rendering.drawRangeDown = int.Parse(drawRangeDownString);
        }
        if(data.Sections.ContainsSection("Units"))
        {
            string drawUnitsString = data["Units"]["drawUnits"];
            if (drawUnitsString != null)
                Units.drawUnits = bool.Parse(drawUnitsString);
            string scaleUnitsString = data["Units"]["scaleUnits"];
            if (scaleUnitsString != null)
                Units.scaleUnits = bool.Parse(scaleUnitsString);
        }
    }

    static void SerializeIni(string filename)
    {
        FileIniDataParser parser = new FileIniDataParser();
        IniData data = new IniData();
        if (File.Exists(filename))
        {
            data = parser.ReadFile(filename);
            File.Delete(filename);
        }

        data.Sections.AddSection("Meshing");
        data["Meshing"]["meshingThreads"] = Meshing.meshingThreads.ToString();
        data["Meshing"]["queueLimit"] = Meshing.queueLimit.ToString();

        data.Sections.AddSection("Rendering");
        data["Rendering"]["drawRangeSide"] = Rendering.drawRangeSide.ToString();
        data["Rendering"]["drawRangeUp"] = Rendering.drawRangeUp.ToString();
        data["Rendering"]["drawRangeDown"] = Rendering.drawRangeDown.ToString();

        data.Sections.AddSection("Units");
        data["Units"]["drawUnits"] = Units.drawUnits.ToString();
        data["Units"]["scaleUnits"] = Units.drawUnits.ToString();

        parser.WriteFile(filename, data);
    }

    // This function is called when the MonoBehaviour will be destroyed
    public void OnDestroy()
    {
        SerializeIni("Config.ini");
    }

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        DeserializeIni("Config.ini");
    }
}
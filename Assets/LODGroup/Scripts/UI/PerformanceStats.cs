using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;

public class PerformanceStats : MonoBehaviour
{
    private readonly Queue<float> _samples = new Queue<float>();
    private int _totalSamples = 60;
    private readonly StringBuilder _stringBuilder = new StringBuilder();
    private readonly string[] _qualitys = {"Low", "Middle", "High"};
    private bool _openOption;

    public Action ShowDebuggerAction;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        _stringBuilder.Clear();

        _samples.Enqueue(Time.deltaTime);

        if (_samples.Count > _totalSamples)
        {
            _samples.Dequeue();
        }

        UpdateFrameTime();
        UpdateMemory();
        UpdateScreen();
        UpdateQuality();
    }

    void UpdateFrameTime()
    {
        float frameTime = 0f;
        float sampleDivision = 1f / _samples.Count;
        foreach (var sample in _samples)
        {
            frameTime += sample * sampleDivision;
        }

        _stringBuilder.AppendFormat("Total time:{0:F2}ms fps:{1:F2}\n", frameTime * 1000, 1f / frameTime);
    }

    private void UpdateMemory()
    {
        long totalMem = Profiler.GetTotalAllocatedMemoryLong();
        _stringBuilder.AppendFormat("Total Memory:{0:F2}Mbs\n", (float) totalMem / 1000000);
        long gpuMem = Profiler.GetAllocatedMemoryForGraphicsDriver();
        _stringBuilder.AppendFormat("GPU Memory:{0:F2}Mbs\n", (float) gpuMem / 1000000);
    }

    private void UpdateScreen()
    {
        _stringBuilder.AppendFormat("ScreenSize X:{0}, Y:{1}\n", Screen.width, Screen.height);
    }

    private void UpdateQuality()
    {
        _stringBuilder.AppendFormat("QualityLevel: {0}\n", _qualitys[QualitySettings.GetQualityLevel()]);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 90), _stringBuilder.ToString());

        if (_openOption == false && GUI.Button(new Rect(0, 90, 100, 20), "Open Option"))
        {
            _openOption = true;
        }

        if (_openOption && GUI.Button(new Rect(0, 180, 100, 20), "Close Option"))
        {
            _openOption = false;
        }

        if (_openOption)
        {
            var qualityLevel = QualitySettings.GetQualityLevel();
            var index = GUI.Toolbar(new Rect(0, 90, 200, 20), qualityLevel, _qualitys);
            if (index != qualityLevel)
            {
                QualitySettings.SetQualityLevel(index);
            }

            if (GUI.Button(new Rect(0, 120, 200, 20), "Unlock Framerate"))
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 200;
            }

            if (GUI.Button(new Rect(0, 150, 200, 20), "ShowDebugger"))
            {
                ShowDebuggerAction?.Invoke();
                _openOption = false;
            }
        }
    }
}
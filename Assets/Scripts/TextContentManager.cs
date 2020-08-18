﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class TextContentManager : MonoBehaviour
{
    public TextAsset textFile;
    public TextMeshProUGUI textMeshPro;
    public TextWriter writer;
    public GameObject DisableOnComplete;

    private string[] text;
    private int textIter;

    private void Awake()
    {
        text = textFile.text.Split('\n');
        textMeshPro.text = text[0]; // Assign the first line to begin with.
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeTicker.Instance.SetTimeScale(0); // Pause the game while we are doing this
    }

    // Update is called once per frame
    void Update()
    {
        // If we press space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (writer.IsWorking()) // If working, skip the working text
                writer.Skip();
            else                    // Otherwise we go to the next text chunk
            {
                textIter++;
                if (textIter < text.Length) // If we're within our bounds still, then write!
                {
                    writer.SetTextToWrite(text[textIter]); // Set the next line to write
                    //if(!writer.AutoStartWriting) // Start writing if the writer isn't set to auto
                        //writer.StartWriting();
                }
                else                        // Otherwise we should close this UI
                {
                    TimeTicker.Instance.SetTimeScale(1); // Set to regular time scale
                    DisableOnComplete.SetActive(false);
                }
            }
        }
    }

}

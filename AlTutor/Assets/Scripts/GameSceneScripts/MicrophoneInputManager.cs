using UnityEngine;

public class MicrophoneInputManager : MonoBehaviour
{
    [SerializeField] private AudioSource microphoneAudioSource;
    private bool isRecording = false;
    string defaultMicrophone;
    private string WavFileSavePath;

    public void HandleRecording()
    {
        if (!isRecording && InputManager.GetRecordKeyPressed())
        {
            isRecording = true;
            microphoneAudioSource.clip = Microphone.Start(defaultMicrophone, false, 30, AudioSettings.outputSampleRate);
        }

        else if (isRecording && InputManager.GetRecordKeyPressed())
        {
            isRecording = false;
            Microphone.End(defaultMicrophone);
            SaveRecord(microphoneAudioSource.clip);
        }
    }

    private int recordNumberCount = 0;
    private void SaveRecord(AudioClip audioClip)
    {
        string filePath = WavFileSavePath + @$"\Record_{recordNumberCount}.wav";
        SavWav.Save(filePath, audioClip, true);
        recordNumberCount++;
    }


    private void Start()
    {
        defaultMicrophone = Microphone.devices[0];
        WavFileSavePath = Application.dataPath + @"\DataTransfer\PlayerInput";
    }

    private void Update()
    {
        HandleRecording();
    }

}

using System;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;

public static class ReadWav
{
    private static bool ReadWavFile(string filePath, out byte[] header, out byte[] data)
    {
        if (!(new Regex(@"\.wav$").IsMatch(filePath)))
        {
            header = new byte[0];
            data = new byte[0];
            Debug.Log("This is not a .wav file");
            return false;
        }

        int fileSizeInByte = 0;
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) fileSizeInByte = (int)fileStream.Length;


        byte[] byteArray = new byte[fileSizeInByte];
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) fileStream.Read(byteArray, 0, fileSizeInByte);


        header = new byte[44];
        data = new byte[fileSizeInByte - 44];
        Array.Copy(byteArray, 0, header, 0, 44);
        Array.Copy(byteArray, 44, data, 0, fileSizeInByte - 44);

        return true;

    }

    private static AudioClip CreateEmptyAudioCilp(byte[] header, byte[] data)
    {
        int numChannels = header[22] + header[23] * 256;

        int bitPerSample = header[34] + header[35] * 256;
        int sampleSizeInBytes = (bitPerSample * numChannels / 8);

        int numOfSample = data.Length / sampleSizeInBytes;

        int sampleRate = header[24] + header[25] * 256 + header[26] * 256 * 256 + header[27] * 256 * 256 * 256;

        return AudioClip.Create("newClip", numOfSample, numChannels, sampleRate, false);
    }

    private static void SetAudioCilp(AudioClip audioClip, byte[] data)
    {
        //from int16 to float
        float rescaleFactor = 32767f;
        float[] floatArray = new float[data.Length / 2];

        //can only convert a mono sound file (with only one channel)
        for (int i = 0; i < data.Length; i += 2)
        {
            try
            {
                short temp = BitConverter.ToInt16(data, i);
                floatArray[i / 2] = temp / rescaleFactor;
            }
            catch (Exception e)
            {
                Debug.Log("ERROR AT INDEX:  " + i);
                Debug.Log(e);
                break;
            }
        }

        audioClip.SetData(floatArray, 0);
    }

    public static AudioClip ReadWavToAudioClip(string filePath)
    {
        ReadWavFile(filePath, out byte[] header, out byte[] data);
        AudioClip audioClip = CreateEmptyAudioCilp(header, data);
        SetAudioCilp(audioClip, data);
        return audioClip;
    }

}

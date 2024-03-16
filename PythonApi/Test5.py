from pydub import utils, AudioSegment

def Mp3ToWav(mp3FileName, outputFileIndex):
    Mp3Path = "D:\\AITutor_onUnity\\AITutor\\AlTutor\\Assets\\DataTransfer\\TempMp3ToWav"
    OutputPath = "D:\\AITutor_onUnity\\AITutor\\AlTutor\\Assets\\DataTransfer\\ServerOutput_Sound"

    def get_prober_name():
        return "bin/ffprobe.exe"
            
    AudioSegment.converter = "bin/ffmpeg.exe"
    utils.get_prober_name = get_prober_name

    print(OutputPath + f"\\output{outputFileIndex}.wav")

    sound = AudioSegment.from_mp3(Mp3Path+"\\"+ mp3FileName)
    sound.export(OutputPath + f"\\output{outputFileIndex}.wav", format="wav")
    #os.remove(Mp3Path + "\\" + mp3FileName)


Mp3ToWav("output0.mp3", 0)
from pydub import utils, AudioSegment

# files                                                                         
src = "D:\\AITutor_onUnity\\AITutor\\AlTutor\Assets\\DataTransfer\T\empMp3ToWav\\outputTest3.mp3"
dst = "outputTest3.wav"

def get_prober_name():
    return "bin/ffprobe.exe"

# convert wav to mp3              
AudioSegment.converter = "bin/ffmpeg"
utils.get_prober_name = get_prober_name

sound = AudioSegment.from_mp3("outputTest3.mp3")
sound.export(dst, format="wav")
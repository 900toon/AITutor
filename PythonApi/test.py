from openai import OpenAI
import os
import json
import time
from langchain.chains import ConversationChain
from langchain.chains.conversation.memory import ConversationEntityMemory
from langchain.chains.conversation.prompt import ENTITY_MEMORY_CONVERSATION_TEMPLATE
from langchain_openai import ChatOpenAI
import requests
from pydub import utils, AudioSegment
import pathlib


common_path = pathlib.Path().absolute().parents[0].as_posix() + "/AlTutor_Data/DataTransfer"
common_path_prompts = pathlib.Path().absolute().as_posix() + "/Prompts"


voiceId_AUS_Female = "GyGUOL7iuKmX4jvChjiF"
# voiceId_AUS_Male = "" (reach maximum api usage)
voiceId_BRI_Female = "GYCXkDNMg7joTQitC0WM"
voiceId_BRI_Male = "5LzOtVrtuhZCXs4eC39B"

voiceId_CHI = "" 
voiceID_JP="6XNSYkDqZ1blajSVtPok"
voiceId = ""

key_ass="b0752bd6cbbfd443185b4b0506eaf12c"
key_sumi="b3ef70ec97f85750c696c9d50b706a0b"
key_kong="6b82a83d8182992771626505c13526a7"
elevenlab_api_key = key_kong
#Api key與voiceID掛勾，每個人都不一樣

def TxtToMp3_Elevenlab(voiceId,elevenlab_api_key, txtFile, fileNumber):

    Mp3Path = f"{common_path}\\TempMp3ToWav"


    CHUNK_SIZE = 1024
    url = "https://api.elevenlabs.io/v1/text-to-speech/"+ voiceId

    headers = {
    "Accept": "audio/mpeg",
    "Content-Type": "application/json",
    "xi-api-key": elevenlab_api_key
    }

    data = {
    "text": txtFile,
    "model_id": "eleven_monolingual_v1",
    "voice_settings": {
        "stability": 0.5,
        "similarity_boost": 0.5
    }
    }

    response = requests.post(url, json=data, headers=headers)
    with open(Mp3Path + f"\\output{fileNumber}.mp3", 'wb') as f:
        for chunk in response.iter_content(chunk_size=CHUNK_SIZE):
            if chunk:
                f.write(chunk)

    print("mp3 convert succeed")
    return f"output{fileNumber}.mp3"
    

def TxtToMp3_OpenAiTts(txtFile, fileNumber, voiceId):
    response = client.audio.speech.create(
        model="tts-1",
        voice= voiceId,
        input= txtFile
    )

    Mp3Path = f"{common_path}\\TempMp3ToWav"
    with open(Mp3Path  + f"\\output{fileNumber}.mp3", 'wb') as f:
        f.write(response.content)
    
    print("mp3 convert succeed")
    return f"output{fileNumber}.mp3"


def Mp3ToWav(mp3FileName, outputFileIndex):
    Mp3Path = f"{common_path}\\TempMp3ToWav"
    OutputPath = f"{common_path}\\ServerOutput_Sound"

    def get_prober_name():
        return "bin/ffprobe.exe"
            
    AudioSegment.converter = "bin/ffmpeg.exe"
    utils.get_prober_name = get_prober_name

    print(OutputPath + f"\\output{outputFileIndex}.wav")

    sound = AudioSegment.from_mp3(Mp3Path+"\\"+ mp3FileName)
    sound.export(OutputPath + f"\\output{outputFileIndex}.wav", format="wav")
    os.remove(Mp3Path + "\\" + mp3FileName)



def fetch_wav_files(directory):
    wav_files = []
    if os.path.exists(directory) and os.path.isdir(directory):
        for file in os.listdir(directory):
            if file.endswith(".wav"):
                # wav_files.append(os.path.join(directory, file))
                wav_files.append(file)
    return wav_files


#pre clean the folder
def Clean_the_files():
    folderArray = [f"{common_path}\\PlayerInput",
                   f"{common_path}\\ServerOutput_Text",
                   f"{common_path}\\ServerOutput_Sound",
                   f"{common_path}\\TempMp3ToWav"
                   ]
    num = 0

    for folder in folderArray:
        if (os.path.exists(folder)):
            for file in os.listdir(folder):
                os.remove(folderArray[num] + "\\" + file)
                print("File deleted:  " + folderArray[num] + "\\" + file)
        num+=1

#main funciton start from here:


#Get setup file before running:
initFilePath = f"{common_path}/Initialization.txt"
Txt_To_Mp3_Api_mode = 0
prompt_string = ""
init_settings = {"accentMode": 0, "loverMode" : 0}
init_string = ""

print("initfilepath: "+ initFilePath)
while(True):
    print("0")
    try:     
        with open(initFilePath, "r") as f:
            init_string = f.read()
            
        
        if (len(init_string) > 1):
            Clean_the_files()  
            break
        
    except:
        pass

print("1")
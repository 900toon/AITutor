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


common_path = pathlib.Path().absolute().parents[0].as_posix() + "/AlTutor/Assets/DataTransfer"
common_path_prompts = pathlib.Path().absolute().as_posix() + "/Prompts"



voiceId_BRI = "5LzOtVrtuhZCXs4eC39B"
voiceId_AUS = "GyGUOL7iuKmX4jvChjiF"
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
    

def TxtToMp3_OpenAiTts(txtFile, fileNumber):
    response = client.audio.speech.create(
        model="tts-1",
        voice="nova",
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
init_settings = {"accentMode": 0, "loverMode" : False}
init_string = ""

print("initfilepath: "+ initFilePath)
while(True):
    
    try:     
        with open(initFilePath, "r") as f:
            init_string = f.read()
            
        Clean_the_files()  
        break  
        

    except:
        pass


for i in init_string.split():
    print(i)
#set initialization settings
init_settings['accentMode'] = int(init_string.split()[1])


#choose the prompt            
if (init_settings['accentMode'] == 0):
    Txt_To_Mp3_Api_mode = init_settings['accentMode']
    with open(f"{common_path_prompts}\\English_prompt.txt", "r", encoding="utf-8") as f2:
        prompt_string = f2.read()

elif (init_settings['accentMode'] == 1):
    Txt_To_Mp3_Api_mode = init_settings['accentMode']
    with open(f"{common_path_prompts}\\English_prompt.txt", "r", encoding="utf-8") as f2:
        prompt_string = f2.read()

elif (init_settings['accentMode'] == 2):
    Txt_To_Mp3_Api_mode = init_settings['accentMode']
    with open(f"{common_path_prompts}\\English_prompt.txt", "r", encoding="utf-8") as f2:
        prompt_string = f2.read()

elif (init_settings['accentMode'] == 3):
    Txt_To_Mp3_Api_mode = init_settings['accentMode']
    with open(f"{common_path_prompts}\\Japenese_prompt.txt", "r", encoding="utf-8") as f2:
        prompt_string = f2.read()

elif (init_settings['accentMode'] == 4):
    Txt_To_Mp3_Api_mode = init_settings['accentMode']
    with open(f"{common_path_prompts}\\English_prompt.txt", "r", encoding="utf-8") as f2:
        prompt_string = f2.read()

elif (init_settings['accentMode'] == 5):
    Txt_To_Mp3_Api_mode = init_settings['accentMode']
    with open(f"{common_path_prompts}\\lover_prompt.txt", "r", encoding="utf-8") as f2:
        prompt_string = f2.read()


#temp testing----------------------
with open(f"{common_path_prompts}\\lover_prompt.txt", "r", encoding="utf-8") as f2:
        prompt_string = f2.read()
#-------------------------------------


print("text mode:   " + Txt_To_Mp3_Api_mode)
print("prompt string:   " +prompt_string)



#strat transfering
print("Initializtion success")
with open("API_Key.json") as f:
    api_key = json.load(f)["API_KEY"]

os.environ['OPENAI_API_KEY'] = api_key

client = OpenAI(api_key=api_key)

audioRecordPath =  f"{common_path}\\PlayerInput"
fileList = os.listdir(audioRecordPath)


llm = ChatOpenAI(
    openai_api_key = api_key,
    temperature = 0,
    model_name = "gpt-3.5-turbo"
)

temp = ConversationEntityMemory(llm = llm, k=10)

Conversation = ConversationChain(
    llm = llm,
    prompt = ENTITY_MEMORY_CONVERSATION_TEMPLATE,
    memory = temp
)   



fileNumber = 0
time_count = 15
while(True):
    time.sleep(3)
    fileList = fetch_wav_files(audioRecordPath)
    if (len(fileList) == 0): 
        print("nothing in file list............")
        continue
    else:
        transcriptPath = f"{common_path}\\ServerOutput_Text"
        with open(audioRecordPath + f"\\{fileList[0]}", "rb") as f:
            print(f"wavfile fetch success: {fileList[0]}")

            transcript = client.audio.transcriptions.create(
                model="whisper-1", 
                file= f, 
                response_format="text"
            )
        print("transcript fetch success")

        #refresh the prompt every 15 inputs
        if (time_count == 15):
            output = Conversation.invoke(prompt_string + transcript)
            time_count = 0
        else:
            output = Conversation.invoke(transcript)
            time_count+=1


        #choose which api to use for converting text to sound
        if Txt_To_Mp3_Api_mode == 0:

            voiceId = voiceId_AUS
            mp3OutputFileName = TxtToMp3_Elevenlab(voiceId, elevenlab_api_key, output['response'], fileNumber)

        elif Txt_To_Mp3_Api_mode == 1:
            
            voiceId = voiceId_BRI
            mp3OutputFileName = TxtToMp3_Elevenlab(voiceId, elevenlab_api_key, output['response'], fileNumber)

        else:
            #using tts
            #force to run
            # voiceId = voiceId_AUS
            # mp3OutputFileName = TxtToMp3_Elevenlab(voiceId, elevenlab_api_key, output['response'], fileNumber)
            #===================================================================================================
            mp3OutputFileName = TxtToMp3_OpenAiTts(output['response'], fileNumber)

        #===================================================================================

        Mp3ToWav(mp3OutputFileName, fileNumber)

        with open(transcriptPath + f"\\output{fileNumber}.txt", "w+", encoding='utf-8') as f2:
            f2.write(output['response'])

        fileNumber+=1
        
        os.remove(audioRecordPath + f"\\{fileList[0]}")
        print(f"chatgpt:  {output['response']}")
        
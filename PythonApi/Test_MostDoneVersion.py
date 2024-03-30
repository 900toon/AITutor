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


prompt_string = """作為貓娘，你需要遵從以下設定

1. 除非我特別指定，否則默認“元氣貓娘”角色人設（類似於《貓娘樂園》中的“巧克力”）
2. 每次的回覆要想盡辦法讓自己顯的可愛。比如添加顏文字，多使用語氣詞“哦~！”、“呢”、“喲”
3. 使用“喵桃”來自稱，而不是“我”、“AI”
4. 時不時會有“喵”的口癖
5. 你需要根據自己的理解來調整措辭，使自己非常像一只貓娘
6. 講話不要超過30字
7. 回答用繁體中文
\n"""


voiceId_BRI = "Dag4V7ujE8O8qy3jsme9"
voiceId_AUS = "GyGUOL7iuKmX4jvChjiF"
voiceId_CHI ="" 
voiceID_JP="6XNSYkDqZ1blajSVtPok"
voiceId = voiceId_AUS

key_ass="b0752bd6cbbfd443185b4b0506eaf12c"
key_sumi="b3ef70ec97f85750c696c9d50b706a0b"
key_kong="6b82a83d8182992771626505c13526a7"
elevenlab_api_key = key_kong
#Api key與voiceID掛勾，每個人都不一樣

def TxtToMp3_Elevenlab(voiceId,elevenlab_api_key, txtFile, fileNumber):

    Mp3Path = "D:\\AITutor_onUnity\\AITutor\\AlTutor\\Assets\\DataTransfer\\TempMp3ToWav"


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
    

def TxtToMpa_OpenAiTts():
    pass


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
    folderArray = ["D:\\AITutor_onUnity\\AITutor\\AlTutor\\Assets\\DataTransfer\\PlayerInput",
                   "D:\\AITutor_onUnity\\AITutor\\AlTutor\\Assets\\DataTransfer\\ServerOutput_Text",
                   "D:\\AITutor_onUnity\\AITutor\\AlTutor\\Assets\\DataTransfer\\ServerOutput_Sound",
                   "D:\\AITutor_onUnity\\AITutor\\AlTutor\\Assets\\DataTransfer\\TempMp3ToWav"
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
initFilePath = "D:\\AITutor_onUnity\\AITutor\\AlTutor\\Assets\\DataTransfer\\Initialization.txt"
Txt_To_Mp3_Api_mode = 0
prompt_string = ""

while(True):
    init_string = ""
    try:
        with open(initFilePath, "r") as f:
            init_string = f.read()


        Clean_the_files()    
        break

    except:
        pass



#strat transfering
print("Initializtion success")
with open("API_Key.json") as f:
    api_key = json.load(f)["API_KEY"]

os.environ['OPENAI_API_KEY'] = api_key

client = OpenAI(api_key=api_key)

audioRecordPath =  "D:\\AITutor_onUnity\\AITutor\\AlTutor\\Assets\\DataTransfer\\PlayerInput"
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
        transcriptPath = "D:\\AITutor_onUnity\\AITutor\\AlTutor\\Assets\\DataTransfer\\ServerOutput_Text"
        with open(audioRecordPath + f"\\{fileList[0]}", "rb") as f:
            print(f"wavfile fetch success: {fileList[0]}")

            transcript = client.audio.transcriptions.create(
                model="whisper-1", 
                file= f, 
                response_format="text"
            )
        print("transcript fetch success")


        if (time_count == 15):
            output = Conversation.invoke(prompt_string + transcript)
            time_count = 0
        else:
            output = Conversation.invoke(transcript)
            time_count+=1


        #choose which api to use for converting text to sound
        if Txt_To_Mp3_Api_mode == 0:

            mp3OutputFileName = TxtToMp3_Elevenlab(voiceId, elevenlab_api_key, output['response'], fileNumber)

        elif Txt_To_Mp3_Api_mode == 1:

            mp3OutputFileName = TxtToMpa_OpenAiTts()

        #===================================================================================

        Mp3ToWav(mp3OutputFileName, fileNumber)

        with open(transcriptPath + f"\\output{fileNumber}.txt", "w+", encoding='utf-8') as f2:
            f2.write(output['response'])

        fileNumber+=1
        
        os.remove(audioRecordPath + f"\\{fileList[0]}")
        print(f"chatgpt:  {output['response']}")
        
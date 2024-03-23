import os
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
                print(folderArray[num] + "\\" + file)
        num+=1

Clean_the_files()

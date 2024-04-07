import pathlib
path = pathlib.Path()


common_path = pathlib.Path().absolute().parents[0].as_posix()

# initFilePath = "D:/AITutor_onUnity/AITutor/AlTutor/Assets/DataTransfer/Initialization.txt"
initFilePath = common_path + "/AlTutor/Assets/DataTransfer/Initialization.txt"

while(True):
    
 
    with open(initFilePath, "r") as f:
        init_string = f.read()
          
    break  
        

print(1)
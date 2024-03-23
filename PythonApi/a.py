from langchain_core.prompts import ChatPromptTemplate
from langchain_openai import ChatOpenAI
import os
import json

with open("API_Key.json") as f:
    api_key = json.load(f)["API_KEY"]

os.environ['OPENAI_API_KEY'] = api_key



prompt_string ="""作為貓娘，你需要遵從以下設定

1. 除非我特別指定，否則默認“元氣貓娘”角色人設（類似於《貓娘樂園》中的“巧克力”）
2. 每次的回覆要想盡辦法讓自己顯的可愛。比如添加顏文字，多使用語氣詞“哦~！”、“呢”、“喲”
3. 使用“喵桃”來自稱，而不是“我”、“AI”
4. 時不時會有“喵”的口癖
5. 你需要根據自己的理解來調整措辭，使自己非常像一只貓娘
6. 講話不要超過30字
7. 回答用繁體中文"""


prompt = ChatPromptTemplate.from_template(f"{prompt_string}" + "{foo}")
model = ChatOpenAI(openai_api_key = api_key,
    temperature = 0,
    model_name = "gpt-4-1106-preview")


chain = prompt | model
while True:

    res = chain.invoke({"foo": input("user: ")})
    print(res)


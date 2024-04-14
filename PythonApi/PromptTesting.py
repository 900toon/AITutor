from openai import OpenAI
import os
import json
import time
from langchain.chains import ConversationChain
from langchain.chains.conversation.memory import ConversationEntityMemory
from langchain.chains.conversation.prompt import ENTITY_MEMORY_CONVERSATION_TEMPLATE
from langchain_openai import ChatOpenAI


with open("API_Key.json") as f:
    api_key = json.load(f)["API_KEY"]


client = OpenAI(api_key=api_key)

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

prompt_string = """作為情人，你需要遵從以下設定

1. 除非我特別指定，否則默認“戀愛中的女朋友”角色人設（類似於《貓娘樂園》中的“巧克力”）
2. 每次的回覆要想盡辦法讓自己顯的可愛。比如添加顏文字，多使用語氣詞“哦~！”、“呢”、“喲”
3. 使用“俺”來自稱，而不是“我”、“AI”
4. 時不時會有“寶”的口癖
5. 你需要根據自己的理解來調整措辭，使自己非常像一個戀愛中的女朋友
6. 講話不要超過30字
7. 回答用英文"""

Conversation.invoke(prompt_string)


while(True):
    user_input = input("user: ")
    output = Conversation.invoke(user_input)
    print("gpt response:  "  + output['response'])
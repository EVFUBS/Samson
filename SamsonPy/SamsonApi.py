from fastapi import FastAPI
from pydantic import BaseModel

app = FastAPI()

class Transcript(BaseModel):
    content: str

class ResponseMessage(BaseModel):
    message: str

@app.get("/", response_model=ResponseMessage)
async def root():
    return {"message": "Hello World"}

@app.get("/action", response_model=ResponseMessage)
async def GetSamsonAction(transcipt: Transcript):
    return {"message": "Hello World"}

@app.get("/wake", response_model=ResponseMessage)
async def GetSamsonWake(transcipt: Transcript):
    return {"message": "Hello World"}
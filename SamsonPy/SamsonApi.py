from fastapi import FastAPI, UploadFile
from pydantic import BaseModel
import tensorflow as tf
import numpy as np
import os
from Training.Wake.ProcessData import extract_features
from enum import Enum

app = FastAPI()

wakeModelPath = r"Training\Wake\WakeModel"
actionModelPath = r""
tempPath = os.getcwd() + r"\temp"

class SamsonCatergories(Enum):
    General = 0,
    Spotify = 2,
    DidNotUnderstand = 100000

# seperated by thousands to allow for more actions without risk of losing space
class SamsonActions(Enum):
    Greet = 0,
    Question = 1,

    WebBrowserOpenWebBrowserToUrl = 1000,
    WebBrowserOpenGoogleBrowser = 1001,

    SpotifyAvailableDevices = 2000,
    SpotifyPlayOrResumePlayback = 2001,
    SpotifyPausePlayback = 2002,

    DoNotUnderstand = 100000

class ResponseMessage(BaseModel):
    message: str
    
class SamsonWakeResponse(BaseModel):
    wake: bool

class SamsonActionRequest(BaseModel):
    summary: str

class SamsonActionResponse(BaseModel):
    action: SamsonActions
    catergory: SamsonCatergories
    parameters: list[str]

class SamsonQuestionResponse(BaseModel):
    summary: str

class SamsonQuestionRequest(BaseModel):
    question: str

@app.get("/api/", response_model=ResponseMessage)
async def root():
    return {"message": "Hello World"}

@app.get("/api/action", response_model=SamsonActionResponse)
async def GetSamsonAction(request: SamsonActionRequest):
    # Look into named entity recognition for extracting paramerters out of the prompts seems to be the way to go
    # looks for named labels, could be used to look for "play" for example then the sequence after it?
    return {"message": "Hello World"}

@app.get("/api/question", response_model=SamsonQuestionResponse)
async def GetSamsonQuestion(request: SamsonQuestionRequest):
    return {"message": "Hello World"}

@app.post("/api/wake", response_model=SamsonWakeResponse)
async def GetSamsonWake(file: UploadFile): 
    file_path = os.path.join(os.getcwd() + r"\temp", "wake.wav")
    with open(file_path, "wb") as temp_file:
        temp_file.write(file.file.read())
        
    input_data = []
    mfccs = extract_features(file_path)
    input_data.append(mfccs)
    input_data = np.array(input_data)
    
    model = tf.keras.models.load_model(wakeModelPath)
    prediction = model.predict(input_data)
    predicted_class = True if prediction[0, 0] > 0.5 else False
    return SamsonWakeResponse(wake=predicted_class)

def use_route_names_as_operation_ids(app: FastAPI) -> None:
    for route in app.routes:
        route.operation_id = route.name

use_route_names_as_operation_ids(app)
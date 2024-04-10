from typing import Union
from fastapi import FastAPI, UploadFile
from pydantic import BaseModel
import tensorflow as tf
import numpy as np
import os
from Training.Wake.ProcessData import extract_features
from enum import Enum
import json
from Training.Action.ProcessData import process_action_data
import spacy
import pandas as pd

app = FastAPI()

wakeModelPath = r"WakeModel"
actionModelPath = r"Training\Action\ActionModel"
nerModelPath = r""
tempPath = os.getcwd() + r"\temp"

class Catergories(Enum):
    General = 0,
    Spotify = 2,
    DidNotUnderstand = 100000

# seperated by thousands to allow for more actions without risk of losing space
class Actions(Enum):
    Greet = 0,
    Question = 1,

    WebBrowserOpenWebBrowserToUrl = 1000,
    WebBrowserOpenGoogleBrowser = 1001,

    SpotifyAvailableDevices = 2000,
    SpotifyPlayOrResumePlayback = 2001,
    SpotifyPausePlayback = 2002,
    SpotifyStartPlaylist = 2003,

    DoNotUnderstand = 100000

class ResponseMessage(BaseModel):
    message: str
    

class WakeResponse(BaseModel):
    wake: bool

class WordsEntity(BaseModel):
    Word: str
    Entity: str

class ActionParameters(BaseModel):
    WordsEntityPairing: list[WordsEntity]

class ActionRequest(BaseModel):
    summary: str

class ActionResponse(BaseModel):
    action: Actions
    catergory: Catergories
    parameters: ActionParameters


class QuestionResponse(BaseModel):
    summary: str

class QuestionRequest(BaseModel):
    question: str


@app.get("/api/", response_model=ResponseMessage)
async def root():
    return {"message": "Hello World"}

# This endpoint is going to use a mix of multi-class classification and 
# named entity recognition to recognise what action the user is asking for
@app.get("/api/action", response_model=ActionResponse)
async def GetSamsonAction(request: ActionRequest):
    data = pd.read_csv(r'Data\SamsonActions.csv')
    classes = data['class_enc']

    # multiclassifcation model - used to identify the action
    input_data = process_action_data(request.summary)
    model = tf.keras.models.load_model(actionModelPath)
    prediction = classes[np.argmax(model.predict(input_data))]

    # depending on the prediction direct to the corresponding ner model

    # ner model - used to extract data from that extraction
    nlp = spacy.load(nerModelPath)
    doc = nlp(request.summary)
    #idk what to do with these till model is up

    return ActionResponse(action=Actions.SpotifyPlayOrResumePlayback, 
                                catergory=Catergories.Spotify, 
                                parameters=ActionParameters(WordsEntityPairing=[WordsEntity(Word="test", Entity="test")]))

@app.get("/api/question", response_model=QuestionResponse)
async def GetSamsonQuestion(request: QuestionRequest):
    return {"message": "Hello World"}

@app.post("/api/wake", response_model=WakeResponse)
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
    return WakeResponse(wake=predicted_class)



def use_route_names_as_operation_ids(app: FastAPI) -> None:
    for route in app.routes:
        route.operation_id = route.name

use_route_names_as_operation_ids(app)
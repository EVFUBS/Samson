from fastapi import FastAPI, UploadFile
from pydantic import BaseModel
import tensorflow as tf
import librosa
from sklearn.preprocessing import StandardScaler
import numpy as np
import os
import tempfile
import wave
from Training.Wake.ProcessData import extract_features

app = FastAPI()

wakeModelPath = r"Training\Wake\WakeModel"
actionModelPath = r""
tempPath = os.getcwd() + r"\temp"

class Transcript(BaseModel):
    content: str

class ResponseMessage(BaseModel):
    message: str
    
class SamsonWakeResponse(BaseModel):
    wake: bool

@app.get("/api/", response_model=ResponseMessage)
async def root():
    return {"message": "Hello World"}

@app.get("/api/action", response_model=ResponseMessage)
async def GetSamsonAction(transcipt: Transcript):
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
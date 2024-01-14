from fastapi import FastAPI, UploadFile
from pydantic import BaseModel
import tensorflow as tf
import librosa
from sklearn.preprocessing import StandardScaler
import numpy as np

app = FastAPI()

wakeModelPath = r"Training\Wake\WakeModel"
actionModelPath = r""

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
    
    async def preprocess_wake_data(file: UploadFile):
        async def extract_features(file: UploadFile, mfcc_max_len=100):
            audio_data, sample_rate = librosa.load(await file.read(), sr=None)
            mfccs = librosa.feature.mfcc(y=audio_data, sr=sample_rate, n_mfcc=13, hop_length=512)
            scaler = StandardScaler()
            mfccs = scaler.fit_transform(mfccs)
            if mfccs.shape[1] < mfcc_max_len:
                mfccs = np.pad(mfccs, ((0, 0), (0, mfcc_max_len - mfccs.shape[1])), mode='constant')
            else:
                mfccs = mfccs[:, :mfcc_max_len]

            return mfccs
        
        input_data = []
        mfccs = await extract_features(file)
        input_data.append(mfccs)
        return input_data
    
    model = tf.keras.models.load_model(wakeModelPath)
    input_data = await preprocess_wake_data(file)
    prediction = model.predict(input_data)
    predicted_class = True if prediction[0, 0] > 0.5 else False
    return SamsonWakeResponse(wake=predicted_class)
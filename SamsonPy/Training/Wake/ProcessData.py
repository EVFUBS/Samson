import os
import librosa
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler
  
# Function to extract MFCC features from an audio file
def extract_features(file_path, mfcc_max_len=100):
    audio_data, sample_rate = librosa.load(file_path, sr=None)
    
    # Extract MFCC features
    mfccs = librosa.feature.mfcc(y=audio_data, sr=sample_rate, n_mfcc=13, hop_length=512)

    # Z-score normalization
    scaler = StandardScaler()
    mfccs = scaler.fit_transform(mfccs)

    # Pad or truncate the MFCCs to a fixed length
    if mfccs.shape[1] < mfcc_max_len:
        mfccs = np.pad(mfccs, ((0, 0), (0, mfcc_max_len - mfccs.shape[1])), mode='constant')
    else:
        mfccs = mfccs[:, :mfcc_max_len]

    return mfccs
    
# Directory paths for positive and negative examples
positive_dir = r'../../Data\Wake\negative'
negative_dir = r'../../Data\Wake\positive'

# List to store features and labels
features = []
labels = []

# Process positive examples
for filename in os.listdir(positive_dir):
    if filename.endswith(".wav"):
        file_path = os.path.join(positive_dir, filename)
        mfccs = extract_features(file_path)
        features.append(mfccs)
        labels.append(1)  # Positive label

# Process negative examples
for filename in os.listdir(negative_dir):
    if filename.endswith(".wav"):
        file_path = os.path.join(negative_dir, filename)
        mfccs = extract_features(file_path)
        features.append(mfccs)
        labels.append(0)  # Negative label

# Convert lists to NumPy arrays
features = np.array(features)
labels = np.array(labels)

# Split the data into training and validation sets
X_train, X_val, y_train, y_val = train_test_split(features, labels, test_size=0.2, random_state=42)

# Save the processed data
np.save("TrainingData\X_train.npy", X_train)
np.save("TrainingData\X_val.npy", X_val)
np.save("TrainingData\y_train.npy", y_train)
np.save("TrainingData\y_val.npy", y_val)
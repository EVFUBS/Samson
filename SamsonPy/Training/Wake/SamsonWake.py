import tensorflow as tf
import numpy as np
from tensorflow.keras import layers

# Load the processed data
X_train = np.load("TrainingData\X_train.npy")
X_val = np.load("TrainingData\X_val.npy")
y_train = np.load("TrainingData\y_train.npy")
y_val = np.load("TrainingData\y_val.npy")

# Define your neural network architecture
model = tf.keras.models.Sequential([
    layers.Input(shape=(X_train.shape[1], X_train.shape[2])),
    layers.Flatten(),
    layers.Dense(64, activation='relu'),
    layers.Dense(1, activation='sigmoid')  # Binary classification, wake word is positive
])

# Compile the model
model.compile(optimizer='adam', loss='binary_crossentropy', metrics=['accuracy'])

# Train the model
model.fit(X_train, y_train, epochs=10, validation_data=(X_val, y_val))

# Save the trained model
model.save("WakeModel")

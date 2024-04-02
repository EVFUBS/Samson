from keras.preprocessing.text import Tokenizer
from keras.preprocessing.sequence import pad_sequences
from numpy.random import RandomState
from sklearn.model_selection import train_test_split
from keras.models import Sequential
from keras.layers import Dense, LSTM, Dropout
import sklearn
import pandas as pd
import json
import io
import tensorflow as tf

df = pd.read_csv(r'C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonPy\Data\SamsonActions.csv')
df['class'] = df['class'].astype('category')
df['class_enc'] = df['class'].cat.codes
df.to_csv(r'C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonPy\Data\SamsonActions.csv')

rng = RandomState()

x_train, x_test, y_train, y_test = train_test_split(df['text'], df['class_enc'], test_size=.2, stratify=df['class'], random_state=42)

tokenizer = Tokenizer(num_words=50000, oov_token='<oov>')
tokenizer.fit_on_texts(x_train)
word_index = tokenizer.word_index
x_seq = tokenizer.texts_to_sequences(x_train)

maxlen = 50
num_classes = df['class'].nunique()
num_of_words = len(tokenizer.word_index) + 1

train_padded = pad_sequences(x_seq, padding='post', maxlen=maxlen)
test_padded = pad_sequences(tokenizer.texts_to_sequences(x_test), padding='post', maxlen=maxlen)

y_train = tf.keras.utils.to_categorical(y_train, num_classes=num_classes)

tokenizer_json = tokenizer.to_json()
with io.open(r'Models\SamsonActions\tokenizer.json', 'w', encoding='utf-8') as f:
    f.write(json.dumps(tokenizer_json, ensure_ascii=False))



# will need to determine the structure of the model with testing prob need some LSTM or RNN
model = Sequential([
    LSTM(128, input_shape=(maxlen, num_of_words), return_sequences=True, activation="relu"),
    Dropout(0.2),
    LSTM(64, activation='relu'),
    Dropout(0.2),
    Dense(num_classes, activation='softmax')
])

model.compile(loss='categorical_crossentropy', optimizer='adam', metrics=['accuracy'])
history = model.fit(train_padded, y_train, epochs=10, validation_data=(x_test, y_test), verbose=2)

model.save("ActionModel", overwrite=True)
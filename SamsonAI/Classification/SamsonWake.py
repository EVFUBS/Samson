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

model_save_path = 'C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonAI\Models\SamsonWake'

df = pd.read_csv('C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonAI\Data\SamsonWake.csv')
rng = RandomState()

x_train, x_test, y_train, y_test = train_test_split(df['text'], df['class'], test_size=.2, stratify=df['class'], random_state=42)
df['encoded_class'] = sklearn.LabelEncoder().fit_transform(df['class'])

maxlen = 20000
tokenizer = Tokenizer(num_words=50000, oov_token='<oov>')
tokenizer.fit_on_texts(x_train)
word_index = tokenizer.word_index
x_seq = tokenizer.texts_to_sequences(x_train)
train_padded = pad_sequences(x_seq, padding='post', maxlen=maxlen)
test_padded = pad_sequences(tokenizer.texts_to_sequences(x_test), padding='post', maxlen=maxlen)

tokenizer_json = tokenizer.to_json()
with io.open('../Models/tokenizer.json', 'w', encoding='utf-8') as f:
    f.write(json.dumps(tokenizer_json, ensure_ascii=False))

model = Sequential([
    Dense(48, x_train.shape, kernel_initializer='he_uniform', activation='relu'),
    Dropout(0.2),
    Dense(24, activation='relu'),
    Dense(df['class'].nunique(), activation='softmax')
])

model.compile(loss='sparse_categorical_crossentropy', optimizer='adam', metrics=['accuracy'])
history = model.fit(x_train, y_train, epochs=10, validation_data=(x_test, y_test), verbose=2)

model.save(model_save_path, overwrite=True)

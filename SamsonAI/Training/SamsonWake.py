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

model_save_path = r'C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonAI\Models\SamsonWake\\'

df = pd.read_csv(r'C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonAI\Data\SamsonWake.csv')
rng = RandomState()

df['encoded_class'] = sklearn.preprocessing.LabelEncoder().fit_transform(df['class'])
x_train, x_test, y_train, y_test = train_test_split(df['text'], df['encoded_class'], test_size=.1, stratify=df['class'], random_state=42)

maxlen = 10
tokenizer = Tokenizer(num_words=50000, oov_token='<oov>')
tokenizer.fit_on_texts(x_train)
word_index = tokenizer.word_index
x_seq = tokenizer.texts_to_sequences(x_train)
train_padded = pad_sequences(x_seq, padding='post', maxlen=maxlen)
test_padded = pad_sequences(tokenizer.texts_to_sequences(x_test), padding='post', maxlen=maxlen)

tokenizer_json = tokenizer.to_json()
with io.open(r'C:\Users\lssmith\Documents\pdrepos\Samson\SamsonConsoleApp\SamsonAI\Models\SamsonWake\tokenizer.json', 'w', encoding='utf-8') as f:
    f.write(json.dumps(tokenizer_json, ensure_ascii=False))

model = Sequential([
    Dense(48, input_shape=[1], kernel_initializer='he_uniform', activation='relu'),
    Dense(df['encoded_class'].nunique(), activation='softmax')
])

model.compile(loss='sparse_categorical_crossentropy', optimizer='adam', metrics=['accuracy'])

model.summary()

train_data = tf.data.Dataset.from_tensor_slices((train_padded, y_train))
valid_data = tf.data.Dataset.from_tensor_slices((test_padded, y_test))
history = model.fit(train_data, epochs=10, validation_data=valid_data, verbose=2)

model.save(model_save_path, overwrite=True)

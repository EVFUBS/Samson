from keras.preprocessing.text import tokenizer_from_json
from keras.preprocessing.sequence import pad_sequences

maxlen = 20000

def process_action_data(input: str):
    actionTokenizer = r"Models/SamsonActions/tokenizer.json"
    with open(actionTokenizer) as f:
        tokenizer = tokenizer_from_json(f.read())
    
    input_data = tokenizer.texts_to_sequences(input)
    padded_input_data = pad_sequences(input_data, padding='post', maxlen=maxlen)

    return padded_input_data
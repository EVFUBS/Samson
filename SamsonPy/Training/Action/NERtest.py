# import spacy 
  
# nlp = spacy.load('en_core_web_sm') 
  
# sentences = ["Samson play a song by John Mayer",
#              "could you resume that Samson"]

# for sentence in sentences: 
#     doc = nlp(sentence) 
    
#     for ent in doc.ents: 
#         print(ent.text, ent.start_char, ent.end_char, ent.label_) 

import spacy

nlp = spacy.load("en_core_web_sm")
doc = nlp("Samson play By The Sea by Linkin Park")

# for token in doc:
#     print(token.text, token.lemma_, token.pos_, token.tag_, token.dep_,
#             token.shape_, token.is_alpha, token.is_stop)
    
for token in doc:
    print(token.text, token.pos_)
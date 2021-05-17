import urllib.request
from nudenet import NudeClassifier
import os

import spacy


import tempfile
from spacy.tokens import Token
from profanity_check import predict, predict_prob

def hello_world(request):
    file_name = "image.jpg"
    tmp_path = os.path.join(tempfile.gettempdir(), file_name)

    with urllib.request.urlopen("https://www.feliway.com/var/storage/images/feliway-2017/de-ce-feliway/cum-pute-i-ajuta-pisicile-sa-nu-se-mai-ascunda/2663712-203-rum-RO/PISICA-DVS.-SE-ASCUNDE.jpg") as url:
        output = open(tmp_path,"wb")
        output.write(url.read())
        output.close()

    classifier = NudeClassifier()
    result = classifier.classify(tmp_path)
    os.remove(tmp_path)
    print(result)
    result = predict_prob(['predict_prob() takes an array and returns the probability each string is offensive'])
    print(result)
    
    result = predict_prob(['go to hell, you scum'])
    print(result)


hello_world(None)
import os
import requests
import string
import random
import urllib.request

import spacy

from nudenet import NudeClassifier

import tempfile
from spacy.tokens import Token
from profanity_check import predict_prob

BASE_URL = 'https://i-can-haz-cheezborger-trends-microservice-es62kxp3ma-nw.a.run.app'
AUTH_TOKEN = 'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImJiZDM1OTUzLWY0NDYtNGJmNy04ZjQwLTRmZTAxMDU0YWE2MiIsInN1YiI6IkludGVncmF0aW9uVGVzdHNVc2VyIiwiZW1haWwiOiJpbnRlZ3VzZXJAdGVzdHMuY29tIiwibmJmIjoxNjIwODU1MTg4LCJleHAiOjE2NTIzOTExODgsImlhdCI6MTYyMDg1NTE4OH0.7DNb986wML_b-a6wT95RiphEr1RBA1ya2y8Fghabo95pMdS7LKixwysvtfmFTInzT8ENZY4TqUQtor2N6mj9Xg'

def scan_image(image_url):
    if image_url is None:
        return None

    file_name = ''.join(random.choices(string.ascii_uppercase + string.digits, k=18))
    tmp_path = os.path.join(tempfile.gettempdir(), file_name)

    with urllib.request.urlopen(image_url) as url:
        output = open(tmp_path,"wb")
        output.write(url.read())
        output.close()

    classifier = NudeClassifier()
    img_scan_result = classifier.classify(tmp_path)
    os.remove(tmp_path)
    return img_scan_result[tmp_path]['unsafe']


def scan_text(text):
    if text is None:
        return None
    return predict_prob([text])[0]


def hello_world(request):
    request_json = request.get_json()

    image_url = request_json['ImageUrl']
    text = request_json['Text']
    callback_url = request_json['CallbackUrl']
    object_id = request_json['ObjectId']

    img_scan_result = scan_image(image_url)
    text_scan_result = scan_text(text)
    
    callback_body = {
        'ObjectId': object_id,
        'ApprovedImage': img_scan_result,
        'ApprovedText': text_scan_result
    }
    callback_complete_url = BASE_URL + '/' + callback_url
    callback_header_token = 'Bearer {}'.format(AUTH_TOKEN)

    print("Callback_body {}".format(callback_body))
    print("Callback_complete_url {}".format(callback_complete_url))
    print("Callback_header_token {}".format(callback_header_token))

    return
    response = requests.post(callback_complete_url, callback_body, headers={"Authentication" : callback_header_token})

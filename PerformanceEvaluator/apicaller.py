import requests
import time

def call_api(index):
    start_time = time.time()
    requests.get('https://localhost:44341/api/v1/trends', verify=False)
    final_time = time.time()
    return final_time - start_time
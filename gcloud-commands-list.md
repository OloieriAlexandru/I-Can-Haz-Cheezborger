
# GCloud setup

## The commands are for Windows CMD

1. Log in

    - gcloud auth login

2. Export env. variables (Windows)

    - setx ACCOUNT_NAME "ci-cd-account"
    - setx PROJECT_ID "i-can-haz-cheezborger"

3. Set the project id

    - gcloud config set project %PROJECT_ID%

4. Enable the necessary services

    - gcloud services enable cloudbuild.googleapis.com
    - gcloud services enable run.googleapis.com
    - gcloud services enable containerregistry.googleapis.com

5. Create a service account

    - gcloud iam service-accounts create %ACCOUNT_NAME% --description="Cloud Run deploy account" --display-name="Cloud-Run-Deploy"

6. Give the service account Cloud Run Admin, Storage Admin, and Service Account User roles

    - gcloud projects add-iam-policy-binding %PROJECT_ID% --member=serviceAccount:%ACCOUNT_NAME%@%PROJECT_ID%.iam.gserviceaccount.com --role=roles/run.admin
    - gcloud projects add-iam-policy-binding %PROJECT_ID% --member=serviceAccount:%ACCOUNT_NAME%@%PROJECT_ID%.iam.gserviceaccount.com --role=roles/storage.admin
    - gcloud projects add-iam-policy-binding %PROJECT_ID% --member=serviceAccount:%ACCOUNT_NAME%@%PROJECT_ID%.iam.gserviceaccount.com --role=roles/iam.serviceAccountUser

7. Generate a **key.json** file with the credentials, so the GitHub workflow can authenticate with Google Cloud

    - gcloud iam service-accounts keys create key.json --iam-account %ACCOUNT_NAME%@%PROJECT_ID%.iam.gserviceaccount.com

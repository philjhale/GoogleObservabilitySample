# Google Cloud Platform observability sample app

The purpose of this repository is to evaluate Stackdriver's ability to observe applications deployed in Kubernetes.

## Prerequisites

All
 * .NET Core SDK 2.1
 * A Google Cloud Platform account

Mac
  
  * Download and install [Docker for Mac](https://docs.docker.com/docker-for-mac/install/)
  * Preferences -> Kubernetes -> Enable Kubernetes

Windows
 * You're on your own. Probably Docker and Minikube
 
 
 ## Steps
 
 See [creating a cluster](https://cloud.google.com/kubernetes-engine/docs/how-to/creating-a-cluster)
 
 Install the [Google Cloud SDK](https://cloud.google.com/sdk/docs/downloads-interactive) and set your default project and region.
 
 Create a cluster. Command reference [here](https://cloud.google.com/sdk/gcloud/reference/container/clusters/create).
 
 ```
 gcloud container clusters create observability-test-cluster --num-nodes 1
 ```
 
 Set the kubectl context to the new cluster.
 
 ```
 gcloud container clusters get-credentials observability-test-cluster
 ```
 
 Deploy!
 
 ```
 ./kubectl-apply.sh
 ```
 
 Wait for the service to obtain a public IP.
 
 ```
 kubectl get service --watch
 ```
 
 Navigate to ```http://publicIP:4000/api/dog``` to test.



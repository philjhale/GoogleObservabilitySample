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
 
 ## Instrumenting Stackdriver tracing and logging
 
 Follow the instructions [here](https://cloud.google.com/dotnet/docs/stackdriver).
 
 ## Creating a cluster
 
 The following instructions are taken mostly from [here](https://cloud.google.com/kubernetes-engine/docs/how-to/creating-a-cluster)
 
 Install the [Google Cloud SDK](https://cloud.google.com/sdk/docs/downloads-interactive) and set your default project and region.
 
 Create a cluster. Command reference [here](https://cloud.google.com/sdk/gcloud/reference/container/clusters/create).
 The ```--scopes``` parameter [enables error reporting](https://cloud.google.com/error-reporting/docs/setup/dotnet).
 
 ```
 gcloud container clusters create observability-test-cluster --num-nodes 1 --scopes https://www.googleapis.com/auth/cloud-platform
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


TODO

https://cloud.google.com/monitoring/kubernetes-engine/installing

Enable error reporting https://cloud.google.com/error-reporting/docs/setup/dotnet
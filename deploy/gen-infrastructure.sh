#!/bin/sh

# Create RG
az group create --name beers-rg --location westeurope

# Create cluster
az aks create \
    --resource-group PlainConcepts-K8SWorkshop-Dev \
    --name mybeersakscluster \
    --node-count 1 \
    --enable-addons monitoring \
    --generate-ssh-keys

# Install k8s cli
az aks install-cli

# Get Credentials
az aks get-credentials --resource-group PlainConcepts-K8SWorkshop-Dev --name mybeersakscluster

# Create ACR
az acr create -n myrepo -g PlainConcepts-K8SWorkshop-Dev --sku Standard --admin-enabled true

# Create secret to connect to ACR
kubectl create secret docker-registry acr-auth --docker-server <acr-login-server> --docker-username <username> --docker-password <password> --docker-email <email-address>
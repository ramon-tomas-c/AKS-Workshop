#!/bin/sh

# Create RG
az group create --name <resource-group> --location westeurope

# Create cluster
az aks create \
    --resource-group <resource-group> \
    --name mybeersakscluster \
    --node-count 1 \
    --enable-addons monitoring \
    --generate-ssh-keys

# Install k8s cli
az aks install-cli

# Get Credentials
az aks get-credentials --resource-group <resource-group> --name mybeersakscluster

# Create ACR
az acr create -n myrepo -g <resource-group> --sku Standard --admin-enabled true

# Create secret to connect to ACR
kubectl create secret docker-registry acr-auth --docker-server <acr-login-server> --docker-username <username> --docker-password <password> --docker-email <email-address>

# Add admin role to dashboard service
kubectl create clusterrolebinding kubernetes-dashboard --clusterrole=cluster-admin --serviceaccount=kube-system:kubernetes-dashboard

# Install Helm
kubectl apply -f tiller-rbac.yaml
helm init --service-account tiller

# Deploy nginx controller
helm install stable/nginx-ingress --name nginx-controller --namespace kube-system --set controller.replicaCount=2
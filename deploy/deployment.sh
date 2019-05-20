#!/bin/sh


pushd "k8s"

# Deploy configmaps & Secrets
kubectl create configmap config-files --from-file=nginx-conf=nginx.conf
kubectl apply -f configmap.yaml
kubectl apply -f secrets.yaml

# Deploy services
kubectl apply -f sql-deployment.yaml
kubectl apply -f sql-service.yaml

kubectl apply -f web-deployment.yaml
kubectl apply -f web-service.yaml

kubectl apply -f webapi-deployment.yaml
kubectl apply -f webapi-service.yaml

kubectl apply -f reverse-proxy.yaml
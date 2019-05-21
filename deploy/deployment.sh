#!/bin/sh


pushd "k8s"

helm install --dry-run --debug db

# Deploy services
#helm install db --name release-db
helm upgrade --recreate-pods --install release-db db
helm upgrade --recreate-pods --install release-webapi webapi
helm upgrade --recreate-pods --install release-webapp webapp

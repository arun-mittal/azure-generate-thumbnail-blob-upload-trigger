# Azure-Generate-Thumbnail-Blob-Upload-Trigger

The Blob storage trigger starts a function when a new or updated blob is detected. The blob contents are provided as input to the function.

The Azure Blob storage trigger requires a general-purpose storage account. Storage V2 accounts with hierarchical namespaces are also supported.

# Deploying Azure Function

This function is designed to be deployed inside an Azure function app. It will take monitor a container called 'rawimages' inside the storage account and process '.jpg', '.png' and '.bmp' format files. The files will be resized to a width of 500px whilst maintaining the aspect ratio.

The easiest way to deploy this function is from Azure Portal by navigating to your existing function app and chosing 'Deployment Center'. From here, the settings should be configured as shown below. The 'repository' URL is the URL of this repository.

![]()

Alternatively, to setup CI/CD, you can clone this repository to your Github account (or another version control tool) and setting up the 'Source' to a 'Continous Deployment (CI/CD)' offering instead. 
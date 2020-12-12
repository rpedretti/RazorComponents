name: Publish SignalR Server

on:
  push:
    branches: 
    - master
    paths:
    - Samples/RPedretti.RazorComponents.Sample.SignalRServer/**
    
env:
    DOTNET_VERSION: '3.1.102'
    AZURE_WEBAPP_NAME: 'razorsignalrserver'

jobs:
  publish:
    runs-on: ubuntu-latest
    name: Build

    steps:
    - uses: actions/checkout@v2
      
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: ${{ env.DOTNET_VERSION }}

    - name: Build with dotnet
      working-directory: ./Samples/RPedretti.RazorComponents.Sample.SignalRServer
      run: | 
        dotnet build --configuration Release
        dotnet publish -c Release -o ../../publish

    - name: 'Run Azure webapp deploy action using publish profile credentials'
      uses: azure/webapps-deploy@v1
      with:
        app-name: ${{ env.AZURE_WEBAPP_NAME }}
        publish-profile: ${{ secrets.AZURE_SIGNALR_PROFILE  }}
        package: ./publish
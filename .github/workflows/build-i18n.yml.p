name: Build I18n

on:
  push:
    branches: 
    - master
    paths:
    - RPedretti.RazorComponents.I18n/**
    
jobs:
  build:
    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v2
      
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 3.1.102
    
    - name: Build with dotnet
      run: dotnet build --configuration Release
      working-directory: ./RPedretti.RazorComponents.I18n
    
    - name: Generate Nuget Version
      uses: emrekas/build-number@v3
      id: buildnumber
      with:
        token: ${{secrets.github_token}}
        prefix: shared
        
    - name: Build Nuget
      run: dotnet pack -c Release -p:Version=1.1.$BUILD_NUMBER -o ../publish
      working-directory: ./RPedretti.RazorComponents.I18n
      
    - name: Upload nuget artifact
      uses: actions/upload-artifact@v1
      with:
        name: nugetFile
        path: ./publish/RPedretti.RazorComponents.I18n.1.1.${{ env.BUILD_NUMBER }}.nupkg

    - name: Upload build number
      uses: actions/upload-artifact@v1
      with:
        name: BUILD_NUMBER
        path: BUILD_NUMBER

  publish:
    runs-on: ubuntu-latest
    needs: build
    
    steps:
      - name: Setup .NET Core
        uses: actions/setup-dotnet@v1
        with:
          dotnet-version: 3.1.102
        
      - name: Download build number
        uses: actions/download-artifact@v1
        with:
          name: BUILD_NUMBER
      
      - name: Restore build number
        id: buildnumber
        uses: einaregilsson/build-number@v2 
        
      - name: Get nuget file
        uses: actions/download-artifact@v1
        with:
          name: nugetFile
          
      - name: Publish Nuget
        env:
          NUGET_TOKEN: ${{ secrets.NUGET_TOKEN }}
        run: dotnet nuget push nugetFile/*.nupkg -k $NUGET_TOKEN -s https://api.nuget.org/v3/index.json

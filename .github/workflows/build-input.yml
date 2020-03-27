name: Build Input

on:
  push:
    branches: 
    - master
    paths:
    - RPedretti.RazorComponents.Input/**
    
jobs:
  build:
    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v2
      
    - name: Cache node modules
      uses: actions/cache@v1
      env:
        cache-name: cache-node-modules
      with:
        path: ~/.npm # npm cache files are stored in `~/.npm` on Linux/macOS
        key: ${{ runner.os }}-build-${{ env.cache-name }}-${{ hashFiles('**/package-lock.json') }}
        restore-keys: |
          ${{ runner.os }}-build-${{ env.cache-name }}-
          ${{ runner.os }}-build-
          ${{ runner.os }}-
      
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 3.1.102
    
    - name: Setup Node.js for use with actions
      uses: actions/setup-node@v1.1.0
    
    - name: Install npm dependencies
      run: npm i
      working-directory: ./RPedretti.RazorComponents.Input
      
    - name: Build with Node
      run: npm run build:prod
      working-directory: ./RPedretti.RazorComponents.Input
            
    - name: Build with dotnet
      run: dotnet build --configuration Release
      working-directory: ./RPedretti.RazorComponents.Input
    
    - name: Generate Nuget Version
      uses: emrekas/build-number@v3
      id: buildnumber
      with:
        token: ${{secrets.github_token}}
        prefix: input
        
    - name: Build Nuget
      run: dotnet pack -c Release -p:Version=1.1.$BUILD_NUMBER -o ../publish
      working-directory: ./RPedretti.RazorComponents.Input
      
    - name: Upload nuget artifact
      uses: actions/upload-artifact@v1
      with:
        name: nugetFile
        path: ./publish/RPedretti.RazorComponents.Input.1.1.${{ env.BUILD_NUMBER }}.nupkg

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

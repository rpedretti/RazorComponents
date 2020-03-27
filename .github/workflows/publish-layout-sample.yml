name: Publish Layout Sample

on:
  push:
    branches: 
    - master
    paths:
    - RPedretti.RazorComponents.I18n/**
    - RPedretti.RazorComponents.Input/**
    - RPedretti.RazorComponents.Layout/**
    - RPedretti.RazorComponents.Sensors/**
    - RPedretti.RazorComponents.Shared/**
    - RPedretti.RazorComponents.Sample.Shared/**
    - Samples/RPedretti.RazorComponents.Wasm.Sample/**
    
jobs:
  publish:
    runs-on: ubuntu-latest
    name: Build

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
    
    - name: Install Layout npm dependencies
      run: npm i
      working-directory: ./RPedretti.RazorComponents.Layout
      
    - name: Build Layout with Node
      run: npm run build:prod
      working-directory: ./RPedretti.RazorComponents.Layout
    
    - name: Install Sensors npm dependencies
      run: npm i
      working-directory: ./RPedretti.RazorComponents.Sensors
      
    - name: Build Sensors with Node
      run: npm run build:prod
      working-directory: ./RPedretti.RazorComponents.Sensors
    
    - name: Install Input npm dependencies
      run: npm i
      working-directory: ./RPedretti.RazorComponents.Input
      
    - name: Build Input with Node
      run: npm run build:prod
      working-directory: ./RPedretti.RazorComponents.Input
    
    - name: Install Shared npm dependencies
      run: npm i
      working-directory: ./Samples/RPedretti.RazorComponents.Sample.Shared
      
    - name: Build Shared with Node
      run: npm run build:prod
      working-directory: ./Samples/RPedretti.RazorComponents.Sample.Shared

    - name: Install LibMan
      run: dotnet tool install -g Microsoft.Web.LibraryManager.Cli
            
    - name: Restore LibMan dependencies
      run: ~/.dotnet/tools/libman restore
      working-directory: ./Samples/RPedretti.RazorComponents.Wasm.Sample

    - name: Build with dotnet
      run: dotnet build --configuration Release
      working-directory: ./Samples/RPedretti.RazorComponents.Wasm.Sample

    - name: Create Sample dist folder
      run: dotnet publish -c Release -o ../../publish
      working-directory: ./Samples/RPedretti.RazorComponents.Wasm.Sample

    - name: List content
      run: ls -laR ./publish
        
    - name: Copy GitHub Pages specific files
      run: cp ./GitHubPages/404.html ./GitHubPages/.nojekyll ./publish/wwwroot

    - name: Add base ref
      run: sed -i 's/<base href=".*" \/>/<base href="https:\/\/rpedretti.github.io\/ClientSideRazorComponents\/" \/>/g' ./publish/wwwroot/index.html
          
    - name: GitHub Pages action
      uses: peaceiris/actions-gh-pages@v3.0.3
      with:
        deploy_key: ${{ secrets.ACTIONS_DEPLOY_KEY }}
        publish_branch: master
        publish_dir: ./publish/wwwroot
        external_repository: rpedretti/ClientSideRazorComponents
        commit_message: 'Updated version'

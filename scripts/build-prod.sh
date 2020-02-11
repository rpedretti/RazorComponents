#!/bin/bash

paths=(
	"./Samples/RPedretti.RazorComponents.Sample.Shared"
	"./RPedretti.RazorComponents.BingMap"
	"./RPedretti.RazorComponents.Input"
	"./RPedretti.RazorComponents.Layout"
	"./RPedretti.RazorComponents.Sensors"
)

initDir=$(pwd);
cd ../

for path in "${paths[@]}"
do
	cd $path
	if [[ -d "$path/node_modules" ]]
	then
		npm i
	fi

	npm run build:prod
	cd -
done

cd $initDir

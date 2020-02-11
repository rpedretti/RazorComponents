#!/bin/bash

paths=(
	"./RPedretti.RazorComponents.BingMap"
	"./RPedretti.RazorComponents.Input"
	"./RPedretti.RazorComponents.Layout"
	"./RPedretti.RazorComponents.Sensors"
)

initDir=$(pwd);
cd ../

for path in "${paths[@]}"
do
	(cd $path
	if [[ ! -d "$path/node_modules" ]]
	then
		npm i
	fi

	npm run build:$1
	)
done

cd $initDir

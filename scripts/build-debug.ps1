[string[]]$Paths = "RPedretti.RazorComponents.Input", "RPedretti.RazorComponents.Layout", "RPedretti.RazorComponents.Sample", "RPedretti.RazorComponents.Wasm.Sample", "RPedretti.RazorComponents.Sensors"

pushd "../"

foreach ($path in $Paths) {
	pushd $path
	npm i
	npm run build:debug
	popd
}

popd

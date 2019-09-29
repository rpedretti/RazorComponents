[string[]]$Paths = "RPedretti.RazorComponents.Input", "RPedretti.RazorComponents.Layout", "RPedretti.RazorComponents.Sample", "RPedretti.RazorComponents.Sensors"

foreach ($path in $Paths) {
	pushd $path
	npm i
	npm run build:prod
	popd
}

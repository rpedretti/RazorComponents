[string[]]$Paths = 
	"Samples/RPedretti.RazorComponents.Sample.Shared",
	"RPedretti.RazorComponents.BingMap",
	"RPedretti.RazorComponents.Input",
	"RPedretti.RazorComponents.Layout", 
	"RPedretti.RazorComponents.Sensors"

pushd "../"

foreach ($path in $Paths) {
	pushd $path
	if(Test-Path -Path $path\node_modules -PathType Container) {
		npm i
	}
	npm run build:prod
	popd
}

popd

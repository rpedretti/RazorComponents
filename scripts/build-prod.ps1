[string[]]$Paths = 
	"RPedretti.RazorComponents.Sample.Shared",
	"RPedretti.RazorComponents.Input",
	"RPedretti.RazorComponents.Layout", 
	"RPedretti.RazorComponents.Sensors"

pushd "../"

foreach ($path in $Paths) {
	pushd $path
	npm i
	npm run build:prod
	popd
}

popd

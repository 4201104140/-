

declare var process;

var folderToTransform = process.argv[2];
var primaryTypeName = process.argv[3].split(",");
var defaultPrimaryType: string | undefined = undefined;
if (process.argv.length > 5) {
  defaultPrimaryType = process.argv[4]
}
var typePropertyName: string | undefined = undefined;
if (process.argv.length > 5) {
	typePropertyName = process.argv[5];
}

console.log(JSON.stringify(transformed, null, 2));
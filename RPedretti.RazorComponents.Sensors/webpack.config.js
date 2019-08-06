const path = require("path");
const webpack = require("webpack");

module.exports = {
    mode: 'development',
    optimization: {
        minimize: false,
        splitChunks: {
            chunks: 'all'
        }
	},
    resolve: {
        extensions: [".ts", ".js"]
    },
    devtool: "inline-source-map",
    module: {
        rules: [
            {
                test: /\.ts?$/,
                loader: "ts-loader"
            }
        ]
    },
    entry: {
        "geolocation": "./ts/Geolocation.ts",
        "lightsensor": "./ts/LightSensor.ts",
        "init-namespaces": "./ts/InitNamespaces.ts"
    },
    output: {
        path: path.join(__dirname, "/wwwroot/js"),
        filename: "[name].js"
    }
};
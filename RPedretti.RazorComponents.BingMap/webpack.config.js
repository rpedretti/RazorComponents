const path = require("path");
const devMode = process.env.NODE_ENV !== "production";

module.exports = {
    mode: devMode ? "development" : "production",
    optimization: {
        minimize: !devMode,
        splitChunks: {
            chunks: 'all'
        }
    },
    resolve: {
        extensions: [".ts", ".js"]
    },
    devtool: devMode ? "inline-source-map" : "",
    module: {
        rules: [
            {
                test: /\.ts?$/,
                loader: "ts-loader"
            }
        ]
    },
    entry: {
        "bing-map_v1": "./ts/BingMap.ts",
        "bing-map-devtool": "./ts/DevTools.ts"
    },
    output: {
        path: path.join(__dirname, "/wwwroot/js"),
        filename: devMode ? "[name].js" : "[name].min.js"
    }
};
const path = require("path");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MonacoWebpackPlugin = require("monaco-editor-webpack-plugin");
const CopyWebpackPlugin = require("copy-webpack-plugin");

module.exports = (env, args) => {
  const mode = args.mode || 'development';
  const devMode = mode === "development";

  console.info('running webpack with mode:', mode);

  return {
    mode: mode,
    entry: {
      "adaptivecards-designer-app": "./src/app.ts",
    },
    output: {
      path: path.resolve(__dirname, "dist"),
      filename: devMode ? "[name].js" : "[name].min.js",
    },
    resolve: {
      extensions: [".ts", ".tsx", ".js"]
    },
    module: {
      
    }
  }
}
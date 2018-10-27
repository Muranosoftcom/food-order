const path = require("path");
const webpack = require("webpack");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = (env = {}, argv = {}) => {
	const isProd = argv.mode === "production";

	const config = {
		mode: argv.mode || "development", // we default to development when no 'mode' arg is passed
		entry: {
			shared: "./src/pages/shared.js",
			home: "./src/pages/home/index.js",
			order: "./src/pages/order/index.js",
		},
		output: {
			filename: "[name].js",
			path: path.resolve(__dirname, "../wwwroot/dist"),
			publicPath: "/dist/",
		},
		resolve: {
			// Add `.ts` and `.tsx` as a resolvable extension.
			extensions: [".ts", ".tsx", ".js"],
		},
		plugins: [
			new webpack.ProvidePlugin({
				$: "jquery",
				jQuery: "jquery",
				"window.jQuery": "jquery",
			}),
			new MiniCssExtractPlugin({
				filename: "styles.css",
			}),
		],
		module: {
			rules: [
				{
					test: /\.(scss)$/,
					use: [
						{
							// Adds CSS to the DOM by injecting a `<style>` tag
							loader: "style-loader",
						},
						{
							// Interprets `@import` and `url()` like `import/require()` and will resolve them
							loader: "css-loader",
						},
						{
							// Loader for webpack to process CSS with PostCSS
							loader: "postcss-loader",
							options: {
								plugins: function() {
									return [require("autoprefixer")];
								},
							},
						},
						{
							// Loads a SASS/SCSS file and compiles it to CSS
							loader: "sass-loader",
						},
					],
				},
				{
					test: /\.css$/,
					use: [isProd ? MiniCssExtractPlugin.loader : "style-loader", "css-loader"],
				},
				{
					test: /\.m?js$/,
					exclude: /(node_modules|bower_components)/,
					use: {
						loader: "babel-loader",
						options: {
							presets: ["@babel/preset-env"],
							plugins: ["@babel/plugin-transform-runtime", "@babel/plugin-proposal-class-properties"],
						},
					},
				},
			],
		},
	};

	if (!isProd) {
		config.devtool = "eval-source-map";
	}

	return config;
};

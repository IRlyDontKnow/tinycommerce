const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = [
    {
        mode: process.env.NODE_ENV,
        entry: path.resolve(__dirname, 'Client/front/main.js'),
        output: {
            path: path.resolve(__dirname, 'wwwroot/front'),
            filename: '[name].js'
        },
        devtool: false,
        module: {
            rules: [
                {
                    test: /\.css$/,
                    use: [
                        {
                            loader: MiniCssExtractPlugin.loader,
                            options: {
                                hmr: process.env.NODE_ENV === "development",
                                reloadAll: true,
                            },
                        },
                        "css-loader",
                        "postcss-loader",
                    ],
                },
                {
                    test: /\.scss$/,
                    use: [
                        {
                            loader: MiniCssExtractPlugin.loader,
                            options: {
                                hmr: process.env.NODE_ENV === "development",
                                reloadAll: true,
                            },
                        },
                        'css-loader',
                        {
                            loader: 'sass-loader',
                            options: {
                                sourceMap: true
                            }
                        }
                    ]
                }
            ]
        },
        plugins: [
            new MiniCssExtractPlugin({
                filename: "[name].css",
                chunkFilename: "[id].css",
            }),
        ],
    }
]
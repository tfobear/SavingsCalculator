module.exports = {
  devServer: {
    proxy: {
      '/': {
        target: 'https://localhost:44359',
        ws: true,
        changeOrigin: true,
        secure: true
      }
    }
  }
};

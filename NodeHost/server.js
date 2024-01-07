const http = require('http');
const fs = require('fs')

const hostname = '192.168.10.121';
const port = 30303;

// const appPath = "../JS/client/index.html";
const appPath = "package.html";

const server = http.createServer(function (req, res) {
  //Open a file on the server and return its content:
  fs.readFile(appPath, function(err, data) {
    res.writeHead(200, {'Content-Type': 'text/html'});
    res.write(data);
    return res.end();   
  });
}).listen(port);

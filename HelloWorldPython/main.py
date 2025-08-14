from http.server import BaseHTTPRequestHandler, HTTPServer

class HelloHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        self.send_response(200)
        self.send_header("Content-type", "application/json")
        self.end_headers()

        message = '{"mensaje": "Hola Mundo, me llamo Areli"}'
        self.wfile.write(message.encode("utf-8"))

if __name__ == "__main__":
    server_address = ("0.0.0.0", 8000)
    httpd = HTTPServer(server_address, HelloHandler)
    print("Servidor escuchando en puerto 8000...")
    httpd.serve_forever()

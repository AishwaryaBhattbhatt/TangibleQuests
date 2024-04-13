const WebSocket = require('ws');
const wss = new WebSocket.Server({port: 8081},()=>{
    console.log('server started');
});

wss.on('connection',(ws)=>{
    ws.on('message',(data)=>{
        const myText = new TextDecoder('utf-8').decode(data);
        console.log('data received %s', data)
        console.log(myText)
        // Broadcast data to all connected clients
        wss.clients.forEach(client => {
            if (client.readyState === WebSocket.OPEN) {
                console.log('sending data: %s', myText); // Log data being sent
                client.send(myText);
            }
        });
    });
});

wss.on('listening',()=>{
    console.log('server is listening on port 8081')
});
const MessageType = {
    greet: 0, 
    update:1, 
    clear: 2
}
const ellipseCordinates = [];

let selectedShape = 0;
const shapes = {
    circle: 0,
    square: 1,
    personal: 2,
}

let connected = false;
let ready = false;

const uiDiv = document.createElement("div");
const leftDiv = document.createElement("div");
const rightDiv = document.createElement("div");
const button = document.createElement("button");
const colorPicker = document.createElement("input");
const IPAddr = document.createElement("input");
const connectButton = document.createElement("button");
const shapeSelect = document.createElement("select");

const sizeSlider = document.createElement("input");
sizeSlider.type = "range";
sizeSlider.min = 5;
sizeSlider.max = 50;
sizeSlider.value = 15;
sizeSlider.style = "width:45px;"

Object.entries(shapes).forEach(shape => {
    let [key, value] = shape;
    let shapeOption = document.createElement("option");
    shapeOption.text = key;
    shapeOption.value = value;
    shapeSelect.append(shapeOption);
})

shapeSelect.addEventListener("change", (e) => {
    selectedShape = Number(e.target.value);
});

let ip = "85.231.47.151:42069";
let ws;

connectButton.addEventListener("click", (e) => {
    ws = new WebSocket("ws://" + IPAddr.value + "/connect");

    ws.addEventListener("message", (e) => {
        let message = JSON.parse(e.data);

        switch(message.type)
        {
            case MessageType.greet: 
                console.log("Server: " + message.message);
                break;
            case MessageType.update: 
                ellipseCordinates.push(message.data)
                break;
            case MessageType.clear: 
                ellipseCordinates.splice(0, ellipseCordinates.length);
                break;
        }
    });

    ws.addEventListener("open", () => {
        console.log("Opened websocket!");
        connected = true;
    });

    ws.addEventListener("close", () => {
        console.log("Websocket closed!")
        connected = false;
        e.target.disabled = true;
    });

    ws.addEventListener("error", () => {
        console.log("Websocket died!");
        connected = false;
        e.target.disabled = true;
    });

    e.target.disabled = true;
});

connectButton.textContent = "Connect"
connectButton.style = "font-size:large;";

IPAddr.type = "text";
IPAddr.value = ip;
IPAddr.style = "font-size:large;";

colorPicker.type = "color";
colorPicker.style = "font-size:large;";


button.addEventListener("click", () => {
    if(!connected) return;
    
    let message = {type: MessageType.clear};
    ws.send(JSON.stringify(message));
})

button.className = "btn";
button.textContent = "Clear";
button.style = "font-size:large;";

rightDiv.style = "margin-left:5vw;";
shapeSelect.style = "margin-left:5vw;";

leftDiv.append(IPAddr);
leftDiv.append(connectButton);
rightDiv.append(colorPicker);
rightDiv.append(button);

uiDiv.style = "display:flex;";
uiDiv.append(leftDiv);
uiDiv.append(rightDiv);
uiDiv.append(shapeSelect);
uiDiv.append(sizeSlider);

document.body.append(uiDiv);

function setup() {
    createCanvas(800, 800);
    background(100);
    fill(255, 0, 0);   
    rectMode(CENTER)
    noStroke();
}

function draw() {
    background(150)
    ellipseCordinates.forEach(point => {
        fill(point.col)

        switch(point.shape) {
            case shapes.circle:
                ellipse(point.x, point.y, 10, 10);
                break;
            case shapes.square:
                rect(point.x, point.y, 10, 10);
                break;
            case shapes.personal:
                ellipse(point.x, point.y, point.size, point.size);
                break;
        }
    });
}

function mouseDragged() {
    if(!connected) return;

    let newPos = {x: mouseX, y: mouseY, col: colorPicker.value, shape:0}
    switch(selectedShape) {
        case shapes.circle:
            newPos.shape = 0;
            break;
        case shapes.square:
            newPos.shape = 1;
            break;
        case shapes.personal:
            newPos.shape = 2;
            newPos.size = Number(sizeSlider.value);
            break;
    }

    const message = {type: MessageType.update, data: newPos};

    ws.send(JSON.stringify(message));
}
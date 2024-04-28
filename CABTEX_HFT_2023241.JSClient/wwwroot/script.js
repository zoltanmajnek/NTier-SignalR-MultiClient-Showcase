let mydisks = [];
let connection = null;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:53910/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("MyDiskCreated", (user, message) => {
        getdata();
    });

    connection.on("MyDiskDeleted", (user, message) => {
        getdata();
    });

    connection.on("MyDiskUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:53910/mydisk')
        .then(x => x.json())
        .then(y => {
            mydisks = y;
            //console.log(mydisks);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    mydisks.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td style=\"width:5%\">" + t.id + "</td><td>"
            + t.label + "</td><td style=\"width:30%\">"
            + `<button type="button" onclick="select(${t.id})">Select</button> `
        + `<button type="button" onclick="directory(${t.id})">Directory</button> `
            + `<button type="button" onclick="remove(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}
function select(id) {
    let selected = mydisks.find(x => x.id == id);
    document.getElementById('diskLabel').value = selected.label;
    document.getElementById('dMessage').innerHTML = "Nála van:" + selected.friend.name;
}

function remove(id) {
    fetch('http://localhost:53910/mydisk/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let dLabel = document.getElementById('diskLabel').value;
    fetch('http://localhost:53910/mydisk', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Label: dLabel })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function directory(id) {
    document.getElementById('resultarea').innerHTML = "";
    mydisks.forEach(t => {
        let dirFunc = "directory(" + t.id + ")";
        let funcName = "Directory"
        let currLabel = t.label;
        if (t.id == id) {
            funcName = "Close Dir";
            dirFunc = "display()";
            t.softwares.forEach(sw => {
                currLabel += "<br/> &emsp;" + sw.name + "-" + sw.genre + "-" + sw.creator;
            });
        }
        document.getElementById('resultarea').innerHTML +=
            "<tr><td style=\"width:5%\">" + t.id + "</td><td>"
            + currLabel + "</td><td style=\"width:30%\">"
            + `<button type="button" onclick="select(${t.id})">Select</button> `
            + `<button type="button" onclick="${dirFunc}">${funcName}</button> `
            + `<button type="button" onclick="remove(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}
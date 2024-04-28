let mydisks = [];
let connection = null;
let opendirs = [];
let selectedId = -1;
document.getElementById('updateButton').style.visibility = 'hidden';
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
async function getSwdata() {
    await fetch('http://localhost:53910/software')
        .then(x => x.json())
        .then(y => {
            softwares = y;
            //console.log(mydisks);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    mydisks.forEach(t => {
        let dirFunc = "directoryOpen(" + t.id + ")";
        let funcName = "Directory"
        let currLabel = t.label;
        let selectString = "Select"
        if (selectedId == t.id) {
            selectString = "Unselect";
            currLabel = "<p style=\"color:red;\">"+currLabel+"</p>";
        }
        if (opendirs.includes(t.id) && t.softwares != null) {
            funcName = "Close Dir";
            dirFunc = "directoryClose(" + t.id + ")";
            t.softwares.forEach(sw => {
                currLabel += "<br/> &emsp;" + sw.name + "-" + sw.genre + "-" + sw.creator;
            });
        }
        document.getElementById('resultarea').innerHTML +=
            "<tr><td style=\"width:5%\">" + t.id + "</td><td>"
            + currLabel + "</td><td style=\"width:30%\">"
            + `<button type="button" onclick="select(${t.id})">${selectString}</button > `
            + `<button type="button" onclick="${dirFunc}">${funcName}</button> `
            + `<button type="button" onclick="remove(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}
function select(id) {
    if (selectedId != id) {
        let selected = mydisks.find(x => x.id == id);
        selectedId = id;
        document.getElementById('updateButton').style.visibility = 'visible';
        document.getElementById('diskLabel').value = selected.label;
        if (selected.friend != null) {
            document.getElementById('dMessage').innerHTML = "Nála van:" + selected.friend.name;
        }
    } else {
        selectedId = -1;
        document.getElementById('updateButton').style.visibility = 'hidden';
        document.getElementById('dMessage').innerHTML = "";
    }
    display();
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

function update() {
    let dLabel = document.getElementById('diskLabel').value;
    fetch('http://localhost:53910/mydisk', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Label: dLabel, Id: selectedId })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function directoryOpen(id) {
    opendirs.push(id);
    display();
}
function directoryClose(id) {
    opendirs.push(id);
    opendirs = opendirs.filter(x => x != id);
    display();
}
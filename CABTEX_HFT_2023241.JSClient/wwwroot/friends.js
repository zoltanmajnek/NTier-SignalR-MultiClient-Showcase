let friends = [];
let connection = null;
let selectedId = -1;
document.getElementById('updateButton').style.visibility = 'hidden';
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:53910/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("FriendCreated", (user, message) => {
        getdata();
    });

    connection.on("FriendDeleted", (user, message) => {
        getdata();
    });

    connection.on("FriendUpdated", (user, message) => {
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
    await fetch('http://localhost:53910/friend')
        .then(x => x.json())
        .then(y => {
            friends = y;
            display();
        });
}
async function getSwdata() {
    await fetch('http://localhost:53910/software')
        .then(x => x.json())
        .then(y => {
            softwares = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    friends.forEach(t => {
        let selectString = "Select"
        let name = "Név:" + t.name + ". Cím:" + t.address + ". Tel:" + t.phoneNumber;
        if (selectedId == t.id) {
            selectString = "Unselect";
            name = "<p style=\"color:red;\">" + name + "</p>";
        }
        document.getElementById('resultarea').innerHTML +=
            "<tr><td style=\"width:5%\">" + t.id + "</td><td>"
            + name + "</td><td style=\"width:30%\">"
            + `<button type="button" onclick="select(${t.id})">${selectString}</button > `
            + `<button type="button" onclick="remove(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}
function select(id) {
    if (selectedId != id) {
        let selected = friends.find(x => x.id == id);
        selectedId = id;
        document.getElementById('updateButton').style.visibility = 'visible';
        document.getElementById('friendName').value = selected.name;
        document.getElementById('friendAddress').value = selected.address;
        document.getElementById('friendPhone').value = selected.phoneNumber;

    } else {
        selectedId = -1;
        document.getElementById('updateButton').style.visibility = 'hidden';
    }
    display();
}

function remove(id) {
    fetch('http://localhost:53910/friend/' + id, {
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
    let fName = document.getElementById('friendName').value;
    let address = document.getElementById('friendAddress').value;
    let phone = document.getElementById('friendPhone').value;

    fetch('http://localhost:53910/friend', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Name: fName, Address: address, PhoneNumber: phone })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function update() {
    let fName = document.getElementById('friendName').value;
    let address = document.getElementById('friendAddress').value;
    let phone = document.getElementById('friendPhone').value;
    fetch('http://localhost:53910/friend', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Name: fName, Address: address, PhoneNumber: phone, Id: selectedId })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

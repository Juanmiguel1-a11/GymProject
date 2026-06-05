const API_URL = "https://localhost:7170/api";

document
.getElementById("memberForm")
.addEventListener("submit", createMember);

async function createMember(e){

    e.preventDefault();

    const member = {

        firstName:
            document.getElementById("firstName").value,

        lastName:
            document.getElementById("lastName").value,

        email:
            document.getElementById("email").value,

        phone:
            document.getElementById("phone").value,

        dateOfBirth:
            document.getElementById("dateOfBirth").value,

        membershipType:
            parseInt(
                document.getElementById("membershipType").value
            )
    };

    const response = await fetch(
        `${API_URL}/Members`,
        {
            method:"POST",
            headers:{
                "Content-Type":"application/json"
            },
            body:JSON.stringify(member)
        }
    );

    if(response.ok){

        alert("Miembro registrado");

        loadMembers();

        document
        .getElementById("memberForm")
        .reset();
    }
    else{
        alert("Error al registrar");
    }
}

async function loadMembers(){

    const response = await fetch(
        `${API_URL}/Members`
    );

    const members = await response.json();

    const table =
        document.getElementById("membersTable");

    table.innerHTML = "";

    members.forEach(member => {

        table.innerHTML += `
            <tr>
                <td>${member.id}</td>
                <td>${member.firstName} ${member.lastName}</td>
                <td>${member.email}</td>
                <td>${member.membershipType}</td>
            </tr>
        `;
    });
}
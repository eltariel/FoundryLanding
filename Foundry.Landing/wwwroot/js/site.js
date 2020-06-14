// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function join_game(gameHost, playerId, password) {
    const url = gameHost + "/join"
    const formData = new FormData()
    formData.append("userid", playerId)
    formData.append("password", password)

    fetch(url, {
        body: formData,
        method: "POST"
    })
        .then(data => {
            console.log(data)
            return data.json()
        })
        .then(res => {
            console.log(res)
            if ( res.status === "success" ) {
                window.location.href = gameHost + res.redirect
            }
            else {
                console.log("Failure?")
            }
        })
        .catch(error => { console.log(error) })
}

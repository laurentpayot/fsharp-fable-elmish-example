// import { startApp } from "./Main.js"
// startApp({ count: 42 })


function sendApp(event, payload) {
    return document.dispatchEvent(new CustomEvent(event, { detail: JSON.stringify(payload) }))
}

setInterval(() => {
    sendApp("time", {
        time: new Date().toISOString(),
        foo: "bar"
    })
}, 1000)

// document.addEventListener("time", ({detail}) => console.log(JSON.parse(detail)), false)

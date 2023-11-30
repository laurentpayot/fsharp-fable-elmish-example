export function multiply(a, b) {
    return a * b
}

export async function catBase64(text, fontSize) {
    const response = await fetch(`https://cataas.com/cat/says/${
        encodeURIComponent(text)
    }?filter=mono&fontColor=red&fontSize=${
        fontSize
    }&type=square`)
    const array = await response.body?.getReader().read()
    return btoa(String.fromCharCode.apply(null, array?.value))
}

const API_BASE = "http://localhost:5113/api";

function showMessage(elementId, message, type = "success") {
    const element = document.getElementById(elementId);
    if (!element) return;

    element.textContent = message;
    element.className = type === "success" ? "message success" : "message error";

    setTimeout(() => {
        element.textContent = "";
        element.className = "message";
    }, 4000);
}

function getValue(id) {
    return document.getElementById(id).value.trim();
}

function clearInputs(ids) {
    ids.forEach(id => {
        const input = document.getElementById(id);
        if (input) input.value = "";
    });
}

async function request(url, options = {}) {
    const config = {
        method: options.method || "GET",
        headers: {
            "Content-Type": "application/json"
        }
    };

    if (options.body) {
        config.body = options.body;
    }

    const response = await fetch(`${API_BASE}${url}`, config);

    if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText || "Ocurrió un error en la solicitud.");
    }

    const text = await response.text();

    if (!text) return null;

    try {
        return JSON.parse(text);
    } catch {
        return text;
    }
}